using Caliburn.Micro;
using mAxCommerce.WindowsPhone.ViewModels;
using System;
using System.Linq;

namespace mAxCommerce.WindowsPhone.Tests.Helpers
{
    public static class ViewModelExtensions
    {
        public static void Activate(this IViewModel viewModel)
        {
            ((IScreen)viewModel).Activate();
        }

        public static void Deactivate(this IViewModel viewModel, bool close)
        {
            ((IScreen)viewModel).Deactivate(close);
        }
    }
}
