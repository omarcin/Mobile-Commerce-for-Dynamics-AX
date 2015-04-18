using mAxCommerce.WindowsPhone.Services;
using mAxCommerce.WindowsPhone.Tests.Helpers;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace mAxCommerce.WindowsPhone.Tests.Tests
{
    [TestClass]
    public class LongRunningJobTests
    {
        private Mock<IProgressIndicatorService> progressIndicatorService;
        private Mock<IMessageService> messageService;
        private string progressMessage;
        private LongRunningJob longRunningJob;

        private int jobRunsCount;
        private bool throwOnFirstRun;

        [TestInitialize]
        public void TestInitialize()
        {
            this.progressIndicatorService = new Mock<IProgressIndicatorService>();
            this.messageService = new Mock<IMessageService>();
            this.progressMessage = Guid.NewGuid().ToString();
            this.throwOnFirstRun = false;
            this.jobRunsCount = 0;

            this.longRunningJob = new LongRunningJob(
                this.progressIndicatorService.Object,
                this.messageService.Object,
                this.Job,
                this.progressMessage);
        }

        [TestMethod]
        public void ProgressIndicatorIsShown()
        {
            // Arrange
            this.progressIndicatorService.SetupMessage(this.progressMessage);

            // Act
            Task.WaitAll(this.longRunningJob.Run());

            // Assert
            this.progressIndicatorService.VerifyAll();
        }

        [TestMethod]
        public void JobIsRunOnce()
        {
            // Act
            Task.WaitAll(this.longRunningJob.Run());

            // Assert
            Assert.AreEqual(1, this.jobRunsCount);
        }

        [TestMethod]
        public void WhenJobFailsMessageBoxIsShown()
        {
            // Arrange
            this.throwOnFirstRun = true;
            this.messageService.SetupMessageBoxResult(MessageBoxResult.Cancel);

            // Act
            Task.WaitAll(this.longRunningJob.Run());

            // Assert
            this.messageService.VerifyAll();
        }

        [TestMethod]
        public void WhenJobFailsAndUserSelectsCancelJobIsNotRerun()
        {
            // Arrange
            this.throwOnFirstRun = true;
            this.messageService.SetupMessageBoxResult(MessageBoxResult.Cancel);

            // Act
            Task.WaitAll(this.longRunningJob.Run());

            // Assert
            Assert.AreEqual(1, this.jobRunsCount);
        }

        [TestMethod]
        public void WhenJobFailsAndUserSelectsOKJobIsRerun()
        {
            // Arrange
            this.throwOnFirstRun = true;
            this.messageService.SetupMessageBoxResult(MessageBoxResult.OK);

            // Act
            Task.WaitAll(this.longRunningJob.Run());

            // Assert
            Assert.AreEqual(2, this.jobRunsCount);
        }

        private Task Job()
        {
            return Task.Run(() =>
                {
                    this.jobRunsCount++;

                    if (this.throwOnFirstRun && this.jobRunsCount == 1)
                    {
                        throw new Exception();
                    }
                });
        }
    }
}
