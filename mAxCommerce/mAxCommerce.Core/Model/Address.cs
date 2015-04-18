using System;
using System.Linq;

namespace mAxCommerce.Core
{
    public class Address
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public bool Validate()
        {
            return !string.IsNullOrEmpty(this.FirstName) &&
                   !string.IsNullOrEmpty(this.LastName) &&
                   !string.IsNullOrEmpty(this.Street) &&
                   !string.IsNullOrEmpty(this.Number) &&
                   !string.IsNullOrEmpty(this.City) &&
                   !string.IsNullOrEmpty(this.PostalCode) &&
                   !string.IsNullOrEmpty(this.Country);
        }

        public Address Clone()
        {
            return new Address
            {
                City = this.City,
                Country = this.Country,
                FirstName = this.FirstName,
                Id = this.Id,
                LastName = this.LastName,
                Number = this.Number,
                PostalCode = this.PostalCode,
                Street = this.Street
            };
        }
    }
}
