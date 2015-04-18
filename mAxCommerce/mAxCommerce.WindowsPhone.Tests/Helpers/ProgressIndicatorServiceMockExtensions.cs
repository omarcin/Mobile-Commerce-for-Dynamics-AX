using mAxCommerce.WindowsPhone.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mAxCommerce.WindowsPhone.Tests.Helpers
{
    public static class ProgressIndicatorServiceMockExtensions
    {
        public static void SetupMessage(this Mock<IProgressIndicatorService> mock, string message)
        {
            ProgressIndicatorServiceToken token = new ProgressIndicatorServiceToken();
            mock.Setup(s => s.ShowMessage(message)).Returns(token);
            mock.Setup(s => s.Hide(token));
        }

        public static void SetupMessage(this Mock<IProgressIndicatorService> mock)
        {
            ProgressIndicatorServiceToken token = new ProgressIndicatorServiceToken();
            mock.Setup(s => s.ShowMessage(It.IsAny<string>())).Returns(token);
            mock.Setup(s => s.Hide(token));
        }
    }
}
