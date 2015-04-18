using Microsoft.Phone.Controls;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace mAxCommerce.WindowsPhone.Services
{
    public class MessageService : IMessageService
    {
        public async Task<CustomMessageBoxResult> ShowCustomMessage(string title, string message, string leftButton, string rightButton)
        {
            var messageBox = new CustomMessageBox
            {
                Title = title,
                Message = message,
                LeftButtonContent = leftButton,
                RightButtonContent = rightButton,

            };

            TaskCompletionSource<CustomMessageBoxResult> tcs = new TaskCompletionSource<CustomMessageBoxResult>();
            messageBox.Dismissed += (_, args) => { tcs.TrySetResult(args.Result); };
            messageBox.Show();
            return await tcs.Task;
        }

        public MessageBoxResult ShowMessage(string title, string message)
        {
            return ShowMessage(title, message, MessageBoxButton.OKCancel);
        }

        public MessageBoxResult ShowMessage(string title, string message, MessageBoxButton buttons)
        {
            return MessageBox.Show(message, title, buttons);
        }

    }
}
