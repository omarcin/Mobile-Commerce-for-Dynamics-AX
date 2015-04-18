using Caliburn.Micro;
using System;
using System.Linq;

namespace mAxCommerce.WindowsPhone.Tests.Helpers
{
    public static class UriHelper
    {
        public static void SetupViewLocatorForAllTypes()
        {
            ViewLocator.LocateTypeForModelType = (type, _1, _2_) => type;
            ViewLocator.DeterminePackUriFromType = (type, _) => GetUriFor(type);
        }

        public static string GetUriFor(Type type)
        {
            return "/Views/" + type.Name + ".xaml";
        }

        public static string GetUriFor<T>()
        {
            return GetUriFor(typeof(T));
        }
    }
}
