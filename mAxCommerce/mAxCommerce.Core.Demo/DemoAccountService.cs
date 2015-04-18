using System;
using System.Collections.Generic;
using System.Linq;

namespace mAxCommerce.Core.Demo
{
    public class DemoAccountService : IAccountService
    {
        private static readonly List<Account> accounts = new List<Account>();
        private static Account currentAccount;

        static DemoAccountService()
        {
            var account = new Account
                            {
                                Email = "placeholder@placeholder.placeholder"
                            };

            var address = new Address
                            {
                                Id = Guid.NewGuid().ToString(),
                                FirstName = "placeholder",
                                LastName = "placeholder",
                                Street = "placeholder",
                                Number = "placeholder",
                                PostalCode = "placeholder",
                                City = "placeholder",
                                Country = "placeholder"
                            };

            account.DeliveryAddresses.Add(address);

            accounts.Add(account);
            currentAccount = account;
        }

        public bool IsLoggedIn()
        {
            return this.CurrentAccount != null;
        }

        public bool Register(Account account)
        {
            accounts.Add(account);
            this.CurrentAccount = account;
            return true;
        }

        public bool LogIn(string email, string password)
        {
            this.CurrentAccount = accounts.FirstOrDefault(a => a.Email.Equals(email, StringComparison.OrdinalIgnoreCase)) ??
                                  accounts.FirstOrDefault();
            return this.CurrentAccount != null;
        }

        public void LogOut()
        {
            this.CurrentAccount = null;
        }

        public Account CurrentAccount
        {
            get 
            {
                if (currentAccount == null)
                {
                    return null;
                }
                else
                {
                    return currentAccount.Clone();
                }
            }
            private set { currentAccount = value; }
        }

        public void UpdateAccount(Account account)
        {
            this.ValidateAccount(account);

            currentAccount.Email = account.Email;
            currentAccount.DeliveryAddresses = account.DeliveryAddresses;

            foreach (var address in currentAccount.DeliveryAddresses.Where(a => a.Id == null))
            {
                address.Id = Guid.NewGuid().ToString();
            }
        }

        private void ValidateAccount(Account account)
        {
            if (!account.Validate())
            {
                throw new InvalidOperationException("Invalid account");
            }

            if (account.DeliveryAddresses.Any(a => !a.Validate()))
            {
                throw new InvalidOperationException("Invalid address");
            }
        }
    }
}