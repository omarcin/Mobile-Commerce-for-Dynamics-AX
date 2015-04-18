using System;
using System.Linq;

namespace mAxCommerce.Core
{
    public interface IAccountService
    {
        bool Register(Account account);

        bool LogIn(string email, string password);

        bool IsLoggedIn();

        void LogOut();

        Account CurrentAccount { get; }

        void UpdateAccount(Account account);
    }
}
