using System;
using System.Linq;
using Moq;
using mAxCommerce.Core;

namespace mAxCommerce.WindowsPhone.Tests.Helpers
{
    public static class AccountServiceMockExtensions
    {
        public static void SetupCurrentAccount(this Mock<IAccountService> mock, Account account)
        {
            mock.SetupGet(service => service.CurrentAccount).Returns(() => account.Clone());
        }
    }
}
