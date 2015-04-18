using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mAxCommerce.WindowsPhone.Tests.Helpers
{
    public static class IoCHelper
    {
        public static Func<Type, string, object> GetInstanceFromFactory<TType>(Func<TType> factory)
        {
            return (type, key) => 
            {
                if (type.IsAssignableFrom(typeof(TType)))
                {
                    return factory.Invoke();
                }

                throw new InvalidOperationException();
            };
        }
    }
}
