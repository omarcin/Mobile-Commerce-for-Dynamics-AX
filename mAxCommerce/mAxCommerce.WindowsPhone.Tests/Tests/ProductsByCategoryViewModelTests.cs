using mAxCommerce.Core;
using mAxCommerce.WindowsPhone.Services;
using mAxCommerce.WindowsPhone.Tests.Helpers;
using mAxCommerce.WindowsPhone.ViewModels;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mAxCommerce.WindowsPhone.Tests.Tests
{
    [TestClass]
    public class ProductsByCategoryViewModelTests : ViewModelTestBase<ProductsByCategoryViewModel>
    {
        private List<Product> products;
        private Mock<IProductRepository> productRepository;
        private Category category;

        [TestInitialize]
        public void TestInitialize()
        {
            this.products = TestData.Products.CreateProducts();
            this.category = TestData.Categories.CreateCategories();

            this.productRepository = new Mock<IProductRepository>();
            this.productRepository.SetupGetCategoryById(this.category);
            this.productRepository.SetupGetProductsByCategoryId(this.category.Id, this.products);

            this.viewModel.DisplayCategoryId = this.category.Id;
            this.viewModel.ProductRepository = this.productRepository.Object;
        }

        [TestMethod]
        public void ProductsForCorrectCategoryAreLoadedFromProductRepository()
        {
            // Act
            this.viewModel.Activate();

            // Assert
            CollectionAssert.AreEqual(
                this.products.Select(p => p.Id).ToList(),
                this.viewModel.CategoriesAndProducts.OfType<Product>().Select(p => p.Id).ToList());
        }

        [TestMethod]
        public void CorrectDisplayCategoryIsLoadedFromProductRepository()
        {
            // Act
            this.viewModel.Activate();

            // Assert
            Assert.AreEqual(this.category, this.viewModel.DisplayCategory);
        }

        [TestMethod]
        public void CorrectChildCategoriesAreLoadedFromProductRepository()
        {
            // Act
            this.viewModel.Activate();

            // Assert
            CollectionAssert.AreEqual(
                this.category.ChildCategories.Select(p => p.Id).ToList(),
                this.viewModel.CategoriesAndProducts.OfType<Category>().Select(p => p.Id).ToList());
        }

        [TestMethod]
        public void WhenNoDisplayCategoryIdRootCategoryIsLoadedFromProductRepository()
        {
            // Arrange
            Category rootCategory = TestData.Categories.CreateCategories();
            List<Product> rootCategoryProducts = TestData.Products.CreateProducts();

            this.productRepository.SetupGetCategories(rootCategory);
            this.productRepository.SetupGetProductsByCategoryId(rootCategory.Id, rootCategoryProducts);

            this.viewModel.DisplayCategoryId = null;

            // Act
            this.viewModel.Activate();

            // Assert
            Assert.AreEqual(rootCategory, this.viewModel.DisplayCategory);
        }

        [TestMethod]
        public void WhenNoDisplayCategoryIdProductsForRootCategoryAreLoaded()
        {
            // Arrange
            Category rootCategory = TestData.Categories.CreateCategories();
            List<Product> rootCategoryProducts = TestData.Products.CreateProducts();

            this.productRepository.SetupGetCategories(rootCategory);
            this.productRepository.SetupGetProductsByCategoryId(rootCategory.Id, rootCategoryProducts);

            this.viewModel.DisplayCategoryId = null;

            // Act
            this.viewModel.Activate();

            // Assert
            CollectionAssert.AreEqual(
                rootCategoryProducts.Select(p => p.Id).ToList(),
                this.viewModel.CategoriesAndProducts.OfType<Product>().Select(p => p.Id).ToList());
        }

        [TestMethod]
        public void CategoriesAndProductsAreNotReloadedOnActivate()
        {
            // Arrange
            IEnumerable<string> productsIds = this.products.Select(p => p.Id);
            IEnumerable<string> categoriesIds = this.category.ChildCategories.Select(p => p.Id);
            List<string> ids = categoriesIds.Union(productsIds).ToList();

            this.viewModel.Activate();
            this.viewModel.Deactivate(false);

            this.category.ChildCategories = new List<Category>();
            var newProducts = TestData.Products.CreateProducts();
            this.productRepository.SetupGetProductsByCategoryId(this.category.Id, newProducts);

            // Act
            this.viewModel.Activate();

            // Assert
            CollectionAssert.AreEqual(
                ids,
                this.viewModel.CategoriesAndProducts.Select((dynamic o) => o.Id).ToList());
        }

        [TestMethod]
        public void ProgressIndicatorIsShownWhileLoadingProducts()
        {
            // Arrange
            this.progressIndicatorService.SetupMessage();

            // Act
            this.viewModel.Activate();

            // Assert
            this.progressIndicatorService.VerifyAll();
        }

        [TestMethod]
        public void NavigateToProductDetailsPassesProductToProductDetailsPage()
        {
            // Arrange
            var product = this.products[11];

            this.navigationService.SetupNavigateToWithParam<ProductDetailsPageViewModel>(vm => vm.ProductId, product.Id);

            this.viewModel.Activate();

            // Act
            this.viewModel.NavigateToProductDetails(product);

            // Assert
            this.navigationService.VerifyAll();
        }

        [TestMethod]
        public void NavigateToCategoryPassesCategoryDataToChildCategoriesPage()
        {
            // Arrange
            var selectedCategory = this.category.ChildCategories[3];

            this.navigationService.SetupNavigateToWithParams<ChildCategoriesPageViewModel>(
                vm => vm.DisplayCategoryId, selectedCategory.Id,
                vm => vm.ParentCategoryId, this.category.Id);

            this.viewModel.Activate();

            // Act
            this.viewModel.NavigateToCategory(selectedCategory);

            // Assert
            this.navigationService.VerifyAll();
        }
    }
}
