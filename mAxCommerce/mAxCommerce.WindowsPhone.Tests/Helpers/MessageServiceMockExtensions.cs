using System;
using System.Linq;
using Moq;
using mAxCommerce.WindowsPhone.Services;
using Microsoft.Phone.Controls;
using System.Windows;

namespace mAxCommerce.WindowsPhone.Tests.Helpers
{
    public static class MessageServiceMockExtensions
    {
        public static void SetupCustomMessageBoxResult(this Mock<IMessageService> mock, CustomMessageBoxResult result)
        {
            mock.SetupTask<IMessageService, CustomMessageBoxResult>(s => s.ShowCustomMessage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsResult(result);
        }

        public static void SetupMessageBoxResult(this Mock<IMessageService> mock, MessageBoxResult result)
        {
            mock.Setup(s => s.ShowMessage(It.IsAny<string>(), It.IsAny<string>())).Returns(result);
        }
    }
}
