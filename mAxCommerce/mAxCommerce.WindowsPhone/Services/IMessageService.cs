using Microsoft.Phone.Controls;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace mAxCommerce.WindowsPhone.Services
{
    public interface IMessageService
    {
        Task<CustomMessageBoxResult> ShowCustomMessage(string title, string message, string leftButton, string rightButton);
        MessageBoxResult ShowMessage(string title, string message);
        MessageBoxResult ShowMessage(string title, string message, MessageBoxButton buttons);
    }
}
