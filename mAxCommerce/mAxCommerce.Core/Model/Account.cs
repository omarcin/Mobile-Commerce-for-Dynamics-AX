using System;
using System.Collections.Generic;
using System.Linq;

namespace mAxCommerce.Core
{
    public class Account
    {
        public Account()
        {
            this.DeliveryAddresses = new List<Address>();
        }

        public string Email { get; set; }
        
        public IList<Address> DeliveryAddresses { get; set; }

        public virtual bool Validate()
        {
            return !string.IsNullOrEmpty(this.Email);
        }

        public Account Clone()
        {
            return new Account
            {
                Email = this.Email,
                DeliveryAddresses = this.DeliveryAddresses.Select(a => a.Clone()).ToList()
            };
        }
    }
}
