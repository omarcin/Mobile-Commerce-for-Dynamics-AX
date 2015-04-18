using mAxCommerce.Core;
using mAxCommerce.WindowsPhone.Services;
using System;
using System.Linq;

namespace mAxCommerce.WindowsPhone.ViewModels
{
    public class AccountCreatePageViewModel : AccountPageViewModelBase
    {
        private bool acceptsTerms;
        private string password;
        private string passwordRepeat;

        public bool AcceptsTerms
        {
            get
            {
                return this.acceptsTerms;
            }
            set
            {
                this.acceptsTerms = value;
                this.NotifyOfPropertyChange(() => AcceptsTerms);
                this.NotifyOfPropertyChange(() => IsValid);
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
                this.NotifyOfPropertyChange(() => IsValid);
            }
        }


        public string PasswordRepeat
        {
            get
            {
                return this.passwordRepeat;
            }
            set
            {
                this.passwordRepeat = value;
                this.NotifyOfPropertyChange(() => PasswordRepeat);
                this.NotifyOfPropertyChange(() => IsValid);
            }
        }

        public string NextPageUriString { get; set; }

        public void Register()
        {
            if (this.AccountService.Register(this.Account))
            {
                this.NavigationService.NavigateToOrGoToMainPage(this.NextPageUriString);
            }
            else
            {
                this.MessageService.ShowMessage("Registration failed", "Could not register new account");
            }
        }

        public override bool IsValid
        {
            get
            {
                return this.AcceptsTerms && this.Password == this.PasswordRepeat && base.IsValid;
            }
        }

        protected override Account CreateAccount()
        {
            return new Account();
        }
    }
}
