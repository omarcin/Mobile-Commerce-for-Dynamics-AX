using System;
using System.Collections.Generic;
using System.Linq;
using mAxCommerce.Core;
using mAxCommerce.WindowsPhone.ViewModels;

namespace mAxCommerce.WindowsPhone.DesignData
{
    public class Design_ProductDetailsPageViewModel : ProductDetailsPageViewModel
    {
        public Design_ProductDetailsPageViewModel()
        {
            this.Product = new Product
            {
                Description = "lorem ipsum",
                Images = new List<Uri> 
                { 
                    // placeholder
                },
                Name = "NAME",
                Price = 123.4m
            };
        }
    }
}
