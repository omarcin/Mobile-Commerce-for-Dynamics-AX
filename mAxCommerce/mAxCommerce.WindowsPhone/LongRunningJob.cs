using mAxCommerce.WindowsPhone.Services;
using mAxCommerce.WindowsPhone.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace mAxCommerce.WindowsPhone
{
    public class LongRunningJob
    {
        private const string ErrorMessageTitle = "Connection issue";
        private const string ErrorMessageBody = "We could not communicate with server. Please check your Internet connection. Would you like to retry?";
        
        private IProgressIndicatorService progressIndicatorService;
        private IMessageService messageService;

        private string progressMessage;
        private Func<Task> jobFactory;

        public static LongRunningJob From(IViewModel viewModel, Func<Task> jobFactory, string progressMessage)
        {
            return new LongRunningJob(
                viewModel.ProgressIndicatorService,
                viewModel.MessageService,
                jobFactory,
                progressMessage);
        }

        public static Task Run(IViewModel viewModel, Func<Task> jobFactory, string progressMessage)
        {
            LongRunningJob job = LongRunningJob.From(viewModel, jobFactory, progressMessage);
            return job.Run();
        }

        public LongRunningJob(IProgressIndicatorService progressIndicatorService, IMessageService messageService, Func<Task> jobFactory, string progressMessage)
        {
            this.progressIndicatorService = progressIndicatorService;
            this.messageService = messageService;
            this.progressMessage = progressMessage;
            this.jobFactory = jobFactory;
        }

        public async Task Run()
        {
            bool retryAfterException = false;
            var token = this.progressIndicatorService.ShowMessage(progressMessage);

            try
            {
                Task job = this.jobFactory.Invoke();
                await job;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                MessageBoxResult result = this.messageService.ShowMessage(ErrorMessageTitle, ErrorMessageBody);
                retryAfterException = result == MessageBoxResult.OK;
            }
            finally
            {
                this.progressIndicatorService.Hide(token);
            }

            if (retryAfterException)
            {
                await this.Run();
            }
        }
    }
}
