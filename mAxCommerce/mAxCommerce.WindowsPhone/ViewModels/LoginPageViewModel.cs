using Caliburn.Micro;
using mAxCommerce.WindowsPhone.Services;
using System;
using System.Linq;

namespace mAxCommerce.WindowsPhone.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private string email;

        private string password;

        public LoginPageViewModel()
        {
        }

        public string Email
        {
            get
            {
                return this.email;
            }
            set
            {
                this.email = value;
                this.NotifyOfPropertyChange(() => Email);
                this.NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
                this.NotifyOfPropertyChange(() => Password);
                this.NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        public string NextPageUriString { get; set; }

        public bool CanLogIn
        {
            get
            {
                return !string.IsNullOrEmpty(this.Email) &&
                       !string.IsNullOrEmpty(this.Password);
            }
        }

        public void LogIn()
        {
            if (this.AccountService.LogIn(this.Email, this.Password))
            {
                this.NavigationService.NavigateToOrGoToMainPage(this.NextPageUriString);
            }
            else
            {
                this.MessageService.ShowMessage("Login failed", "Please check if you have provided a correct email and password");
            }
        }

        public void CreateAccount()
        {
            this.NavigationService.UriFor<AccountCreatePageViewModel>()
                .WithParam(vm => vm.NextPageUriString, this.NextPageUriString)
                .Navigate();
        }
    }
}
