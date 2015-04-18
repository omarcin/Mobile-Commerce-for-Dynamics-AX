using System;
using System.Linq;
using Microsoft.Phone.Controls;
using NavigationCoercion;
using mAxCommerce.WindowsPhone.Views;

namespace mAxCommerce.WindowsPhone
{
    public class NavigationSetup
    {
        public void SetupNavigation(PhoneApplicationFrame rootFrame)
        {
            var navigation = new FluentNavigation(rootFrame);

            navigation.WhenNavigatedTo<LoginPage>()
                .ThenTo<AccountCreatePage>()
                .ThenToAnyPage()
                .RemoveEntriesFromBackStack(2);

            navigation.WhenNavigatedTo<LoginPage>()
                .ThenTo<AccountCreatePage>()
                .RemoveEntriesFromBackStack(0);

            navigation.WhenNavigatedTo<LoginPage>()
                .ThenToAnyPage()
                .RemoveEntriesFromBackStack(1);

            navigation.WhenNavigatedTo<AccountCreatePage>()
                .ThenToAnyPage()
                .RemoveEntriesFromBackStack(1);

            navigation.WhenNavigatedTo<OrderConfirmPage>()
                .ThenTo<MainPage>()
                .RemoveEntriesFromBackStack(2);
        }
    }
}
