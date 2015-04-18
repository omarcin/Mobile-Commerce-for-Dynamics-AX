using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Linq;

namespace mAxCommerce.WindowsPhone.Services
{
    public class ProgressIndicatorService : IProgressIndicatorService
    {
        private readonly ProgressIndicator progressIndicator;

        private List<KeyValuePair<ProgressIndicatorServiceToken, string>> messageQueue;

        public ProgressIndicatorService()
        {
            this.progressIndicator = new ProgressIndicator();
            this.messageQueue = new List<KeyValuePair<ProgressIndicatorServiceToken, string>>();
        }

        public ProgressIndicatorServiceToken ShowMessage(string message)
        {
            ProgressIndicatorServiceToken token = this.AddToMessageQueue(message);

            this.DisplayFirstMessage();

            return token;
        }

        public void Hide(ProgressIndicatorServiceToken token)
        {
            this.messageQueue.RemoveAll(item => item.Key == token);

            if (!this.messageQueue.Any())
            {
                this.HideProgressIndicator();
            }
            else
            {
                this.DisplayFirstMessage();
            }
        }

        private ProgressIndicatorServiceToken AddToMessageQueue(string message)
        {
            var token = new ProgressIndicatorServiceToken();
            var queueItem = new KeyValuePair<ProgressIndicatorServiceToken, string>(token, message);

            this.messageQueue.Add(queueItem);

            return token;
        }

        private void DisplayFirstMessage()
        {
            SystemTray.ProgressIndicator = this.progressIndicator;
            this.progressIndicator.IsVisible = true;
            this.progressIndicator.IsIndeterminate = true;
            this.progressIndicator.Text = this.messageQueue[0].Value;
        }

        private void HideProgressIndicator()
        {
            this.progressIndicator.IsVisible = false;
        }
    }
}
