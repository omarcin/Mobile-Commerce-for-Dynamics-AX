using System;
using System.Linq;

namespace mAxCommerce.WindowsPhone.Services
{
    public interface IProgressIndicatorService
    {
        ProgressIndicatorServiceToken ShowMessage(string message);
        void Hide(ProgressIndicatorServiceToken token);
    }
}
