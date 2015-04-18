using System;
using System.Linq;
using System.Linq.Expressions;
using Caliburn.Micro;
using Moq;

namespace mAxCommerce.WindowsPhone.Tests.Helpers
{
    public static class NavigationServiceMockExtensions
    {
        public static void SetupNavigateTo<TExpectedViewModel>(this Mock<INavigationService> mock)
        {
            string uri = UriHelper.GetUriFor<TExpectedViewModel>();
            SetupNavigateToUri(mock, uri);
        }

        public static void SetupNavigateToWithParam<TExpectedViewModel>(this Mock<INavigationService> mock, Expression<Func<TExpectedViewModel, string>> property, string value)
        {
            var parameter = new NavigationParameter<TExpectedViewModel>(property, value);
            
            SetupNavigateToWithParams(mock, parameter);
        }

        public static void SetupNavigateToWithParams<TExpectedViewModel>(
            this Mock<INavigationService> mock,
            Expression<Func<TExpectedViewModel, string>> property1, 
            string value1,
            Expression<Func<TExpectedViewModel, string>> property2, 
            string value2)
        {
            var parameter1 = new NavigationParameter<TExpectedViewModel>(property1, value1);
            var parameter2 = new NavigationParameter<TExpectedViewModel>(property2, value2);

            SetupNavigateToWithParams(mock, parameter1, parameter2);
        }

        public static void SetupNavigateToWithParams<TExpectedViewModel>(
            this Mock<INavigationService> mock,
            Expression<Func<TExpectedViewModel, string>> property1,
            string value1,
            Expression<Func<TExpectedViewModel, string>> property2,
            string value2,
            Expression<Func<TExpectedViewModel, string>> property3,
            string value3)
        {
            var parameter1 = new NavigationParameter<TExpectedViewModel>(property1, value1);
            var parameter2 = new NavigationParameter<TExpectedViewModel>(property2, value2);
            var parameter3 = new NavigationParameter<TExpectedViewModel>(property3, value3);

            SetupNavigateToWithParams(mock, parameter1, parameter2, parameter3);
        }

        public static void SetupNavigateToWithParams<TExpectedViewModel>(this Mock<INavigationService> mock, params NavigationParameter<TExpectedViewModel>[] parameters)
        {
            string uri = BuildUriWithParams<TExpectedViewModel>(parameters);

            SetupNavigateToUri(mock, uri);
        }

        private static string BuildUriWithParams<TExpectedViewModel>(NavigationParameter<TExpectedViewModel>[] parameters)
        {
            var uriBuilder = new UriBuilder<TExpectedViewModel>();
            foreach(NavigationParameter<TExpectedViewModel> parameter in parameters)
            {
                uriBuilder.WithParam(parameter.Property, parameter.Value);
            }

            return uriBuilder.BuildUri().OriginalString;
        }

        public static void SetupNavigateToUri(this Mock<INavigationService> mock, string uri)
        {
            mock.Setup(s => s.Navigate(It.Is<Uri>(u => u.OriginalString == uri)));
        }
    }
}
