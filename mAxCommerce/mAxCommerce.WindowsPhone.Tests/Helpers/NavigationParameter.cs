using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace mAxCommerce.WindowsPhone.Tests.Helpers
{
    public class NavigationParameter<TViewModel>
    {
        public NavigationParameter(Expression<Func<TViewModel, string>> property, string value)
        {
            this.Property = property;
            this.Value = value;
        }

        public Expression<Func<TViewModel, string>> Property { get; set; }
        public string Value { get; set; }
    }
}
