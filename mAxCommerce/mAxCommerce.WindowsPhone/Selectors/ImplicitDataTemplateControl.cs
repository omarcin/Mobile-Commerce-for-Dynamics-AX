using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace mAxCommerce.WindowsPhone.Selectors
{
    public class ImplicitDataTemplateControl : ContentControl
    {
        protected override void OnContentChanged(object oldContent, object newContent)
        {
            this.ContentTemplate = this.SelectDataTemplate(newContent);

            base.OnContentChanged(oldContent, newContent);
        }

        protected DataTemplate SelectDataTemplate(object content)
        {
            string key = content.GetType().Name;
            FrameworkElement element = this;
            while (element != null)
            {
                DataTemplate template = element.Resources[key] as DataTemplate;
                if (template != null)
                    return template;
                element = VisualTreeHelper.GetParent(element) as FrameworkElement;
            }
            return Application.Current.Resources[key] as DataTemplate;
        }
    }
}
