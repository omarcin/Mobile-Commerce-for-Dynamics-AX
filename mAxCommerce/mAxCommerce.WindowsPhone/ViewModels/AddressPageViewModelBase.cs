using mAxCommerce.Core;
using System;
using System.Linq;

namespace mAxCommerce.WindowsPhone.ViewModels
{
    public class AddressPageViewModelBase : ViewModelBase
    {
        public AddressPageViewModelBase()
        {
            this.Address = new Address();
        }

        protected Address Address { get; set; }

        public string FirstName
        {
            get
            {
                return this.Address.FirstName;
            }
            set
            {
                this.Address.FirstName = value;
                this.NotifyOfPropertyChange(() => FirstName);
                this.NotifyOfPropertyChange(() => IsValid);
            }
        }

        public string LastName
        {
            get
            {
                return this.Address.LastName;
            }
            set
            {
                this.Address.LastName = value;
                this.NotifyOfPropertyChange(() => LastName);
                this.NotifyOfPropertyChange(() => IsValid);
            }
        }

        public string Street
        {
            get
            {
                return this.Address.Street;
            }
            set
            {
                this.Address.Street = value;
                this.NotifyOfPropertyChange(() => Street);
                this.NotifyOfPropertyChange(() => IsValid);
            }
        }

        public string Number
        {
            get
            {
                return this.Address.Number;
            }
            set
            {
                this.Address.Number = value;
                this.NotifyOfPropertyChange(() => Number);
                this.NotifyOfPropertyChange(() => IsValid);
            }
        }

        public string PostalCode
        {
            get
            {
                return this.Address.PostalCode;
            }
            set
            {
                this.Address.PostalCode = value;
                this.NotifyOfPropertyChange(() => PostalCode);
                this.NotifyOfPropertyChange(() => IsValid);
            }
        }

        public string City
        {
            get
            {
                return this.Address.City;
            }
            set
            {
                this.Address.City = value;
                this.NotifyOfPropertyChange(() => City);
                this.NotifyOfPropertyChange(() => IsValid);
            }
        }

        public string Country
        {
            get
            {
                return this.Address.Country;
            }
            set
            {
                this.Address.Country = value;
                this.NotifyOfPropertyChange(() => Country);
                this.NotifyOfPropertyChange(() => IsValid);
            }
        }

        public bool IsValid
        {
            get
            {
                return this.Address.Validate();
            }
        }

    }
}
