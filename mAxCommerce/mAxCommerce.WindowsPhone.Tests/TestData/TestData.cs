using mAxCommerce.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace mAxCommerce.WindowsPhone.Tests
{
    public static class TestData
    {
        private static readonly Random random = new Random();

        public static int GetRandomInt()
        {
            return random.Next();
        }

        public static string GetRandomString()
        {
            return Guid.NewGuid().ToString();
        }

        public static List<T> CreateList<T>(Func<T> factory, int count)
        {
            return Enumerable.Range(0, count).Select(_ => factory()).ToList();
        }

        public static class Accounts
        {
            public const string Email = "email@email.com";
            public const string Password = "Password1@";
            public const string Password2 = "SomeOtherPassword";
            private const int DefaultAddressesCount = 5;

            public static Account CreateAccount(int addressesCount = DefaultAddressesCount)
            {
                var addresses = CreateList(TestData.Addresses.CreateAddress, addressesCount);

                return new Account
                {
                    Email = Email,
                    DeliveryAddresses = addresses
                };
            }
        }

        public static class Addresses
        {
            public const string City = "New York";
            public const string Country = "USA";
            public const string PostalCode = "11-123";
            public const string Street = "Broadway";
            public const string Number = "123, apt. 15";
            public const string FirstName = "Anne";
            public const string LastName = "Snow";

            public static Address CreateAddress()
            {
                return new Address
                {
                    City = "City " + TestData.GetRandomString(),
                    Country = "Country " + TestData.GetRandomString(),
                    FirstName = "FirstName " + TestData.GetRandomString(),
                    Id = TestData.GetRandomString(),
                    LastName = "LastName " + TestData.GetRandomString(),
                    Number = "17 " + TestData.GetRandomString(),
                    PostalCode = "12-123 " + TestData.GetRandomString(),
                    Street = "Street " + TestData.GetRandomString()
                };
            }
        }

        public static class Products
        {
            private const int DefaultProductsCount = 20;

            public static List<Product> CreateProducts(int count = DefaultProductsCount)
            {
                return CreateList(CreateProduct, count);
            }

            public static Product CreateProduct()
            {
                return new Product
                {
                    Id = TestData.GetRandomInt().ToString(),
                    Name = "Product " + TestData.GetRandomString(),
                    Description = "Lorem ipsum dolor sit amet",
                    Price = 9.99m,
                    Images = new List<Uri> { new Uri("/Img/" + TestData.GetRandomString() + ".jpg", UriKind.Relative) }
                };
            }
        }

        public static class Categories
        {
            private const int DefaultCategoriesCount = 8;

            public static Category CreateCategories(int childrenCount = DefaultCategoriesCount)
            {
                Category category = CreateCategory();
                category.ChildCategories = CreateList(CreateCategory, childrenCount);
                return category;
            }

            public static Category CreateCategory()
            {
                return new Category
                {
                    Id = TestData.GetRandomInt().ToString(),
                    Name = "Category " + TestData.GetRandomString()
                };
            }
        }
    }
}