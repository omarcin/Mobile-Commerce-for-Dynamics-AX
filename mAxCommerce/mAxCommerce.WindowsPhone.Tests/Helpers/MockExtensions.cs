using System.Linq.Expressions;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Moq.Language.Flow;

namespace mAxCommerce.WindowsPhone.Tests.Helpers
{
    public static class MockExtensions
    {
        public static IReturnsResult<TMock> SetupTask<TMock>(this Mock<TMock> mock, Expression<Func<TMock, Task>> expression) where TMock : class
        {
            return mock.Setup<Task>(expression).Returns(Task.Delay(0));
        }

        public static ISetup<TMock, Task<TResult>> SetupTask<TMock, TResult>(this Mock<TMock> mock, Expression<Func<TMock, Task<TResult>>> expression) where TMock : class
        {
            return mock.Setup<Task<TResult>>(expression);
        }

        public static IReturnsResult<TMock> ReturnsResult<TMock, TResult>(this ISetup<TMock, Task<TResult>> setup, TResult result) where TMock : class
        {
            return setup.Returns(Task.FromResult(result));
        }
    }
}
