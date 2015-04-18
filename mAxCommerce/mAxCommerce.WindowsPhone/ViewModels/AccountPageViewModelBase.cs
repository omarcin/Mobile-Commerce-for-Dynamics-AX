using Caliburn.Micro;
using mAxCommerce.Core;
using System;
using System.Linq;

namespace mAxCommerce.WindowsPhone.ViewModels
{
    public abstract class AccountPageViewModelBase : ViewModelBase
    {
        private Account account;

        public AccountPageViewModelBase()
        {
            this.DeliveryAddresses = new BindableCollection<Address>();
        }

        public string Email
        {
            get
            {
                return this.Account.Email;
            }
            set
            {
                this.Account.Email = value;
                this.NotifyOfPropertyChange(() => Email);
                this.NotifyOfPropertyChange(() => IsValid);
            }
        }

        public BindableCollection<Address> DeliveryAddresses
        {
            get;
            private set;
        }

        protected Account Account
        {
            get
            {
                if (this.account == null)
                {
                    this.account = this.CreateAccount();
                }
                return this.account;
            }
            private set
            {
                this.account = value;
            }
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            this.Account = this.RefreshAccount(this.Account);
            this.DeliveryAddresses = new BindableCollection<Address>(this.Account.DeliveryAddresses);
            this.Refresh();
        }

        public virtual bool IsValid
        {
            get
            {
                return this.Account.Validate();
            }
        }

        protected abstract Account CreateAccount();

        protected virtual Account RefreshAccount(Account oldAccount)
        {
            return oldAccount;
        }
    }
}
