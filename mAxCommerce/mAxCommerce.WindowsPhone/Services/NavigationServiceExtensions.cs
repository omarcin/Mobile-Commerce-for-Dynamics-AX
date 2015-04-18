using Caliburn.Micro;
using mAxCommerce.Core;
using mAxCommerce.WindowsPhone.ViewModels;
using System;
using System.Linq;

namespace mAxCommerce.WindowsPhone.Services
{
    public static class NavigationServiceExtensions
    {
        public static void NavigateToOrGoToMainPage(this INavigationService navigationService, string pageUriString)
        {
            Uri uri;
            if (!string.IsNullOrEmpty(pageUriString))
            {
                uri = new Uri(pageUriString, UriKind.RelativeOrAbsolute);
            }
            else
            {
                uri = navigationService.UriFor<MainPageViewModel>().BuildUri();
            }

            navigationService.Navigate(uri);
        }

        public static void GoBackOrDefault(this INavigationService navigationService)
        {
            if (navigationService.CanGoBack)
            {
                navigationService.GoBack();
            }
            else
            {
                navigationService.UriFor<MainPageViewModel>().Navigate();
            }
        }

        public static string UriStringFor<TViewModel>(this INavigationService navigationService)
        {
            return navigationService.UriFor<TViewModel>().BuildUri().ToString();
        }

        public static void NavigateLoggedInTo(this INavigationService navigationService, IAccountService accountService, Uri uri)
        {
            if (accountService.IsLoggedIn())
            {
                navigationService.Navigate(uri);
            }
            else
            {
                navigationService.UriFor<LoginPageViewModel>()
                    .WithParam(vm => vm.NextPageUriString, uri.ToString())
                    .Navigate();
            }
        }

        public static void NavigateLoggedInTo<TNavigationTarget>(this INavigationService navigationService, IAccountService accountService)
        {
            Uri uri = navigationService.UriFor<TNavigationTarget>().BuildUri();
            NavigateLoggedInTo(navigationService, accountService, uri);
        }

        public static void NavigateToProductDetails(this INavigationService navigationService, Product product)
        {
            navigationService.UriFor<ProductDetailsPageViewModel>()
                             .WithParam(vm => vm.ProductId, product.Id)
                             .Navigate();
        }

        public static void NavigateToCategory(this INavigationService navigationService, Category category, Category parentCategory)
        {
            navigationService.UriFor<ChildCategoriesPageViewModel>()
                .WithParam(vm => vm.DisplayCategoryId, category.Id)
                .WithParam(vm => vm.ParentCategoryId, parentCategory.Id)
                .Navigate();
        }
    }
}
