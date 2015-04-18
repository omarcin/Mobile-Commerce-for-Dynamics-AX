using Caliburn.Micro;
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
    public class ChildCategoriesPageViewModelTests : ViewModelTestBase<ChildCategoriesPageViewModel>
    {
        private Mock<IProductRepository> productRepository;
        private Category parentCategory;
        private Category displayCategory;

        [TestInitialize]
        public void TestInitialize()
        {
            this.parentCategory = TestData.Categories.CreateCategories();
            this.displayCategory = this.parentCategory.ChildCategories[2];

            this.productRepository = new Mock<IProductRepository>();
            this.productRepository.SetupGetCategoryById(this.parentCategory);

            this.viewModel.ProductRepository = this.productRepository.Object;
            this.viewModel.ParentCategoryId = this.parentCategory.Id;
            this.viewModel.DisplayCategoryId = this.displayCategory.Id;

            IoC.GetInstance = IoCHelper.GetInstanceFromFactory(CreateMockedProductsByCategoryViewModel);
        }

        [TestMethod]
        public void ActivateLoadsParentCategory()
        {
            // Act
            this.viewModel.Activate();

            // Assert
            Assert.AreEqual(this.parentCategory, this.viewModel.ParentCategory);
        }

        [TestMethod]
        public void ActivateCreatesViewModelForEachChildCategory()
        {
            // Arrange
            List<string> childCategoriesIds = this.parentCategory.ChildCategories.Select(c => c.Id).ToList();
            List<string> childCategoriesNames = this.parentCategory.ChildCategories.Select(c => c.Name).ToList();

            // Act
            this.viewModel.Activate();

            // Assert
            CollectionAssert.AreEqual(
                childCategoriesIds,
                this.viewModel.Items.Cast<IProductsByCategoryViewModel>().Select(vm => vm.DisplayCategoryId).ToList());

            CollectionAssert.AreEqual(
                childCategoriesNames,
                this.viewModel.Items.Cast<IProductsByCategoryViewModel>().Select(vm => vm.DisplayCategoryName).ToList());
        }

        [TestMethod]
        public void ActivateSetsActiveItemBasedOnDisplayCategoryId()
        {
            // Act
            this.viewModel.Activate();

            // Assert
            Assert.AreEqual(this.displayCategory.Id, this.viewModel.ActiveItem.DisplayCategoryId);
        }

        [TestMethod]
        public void WhenInitializingProgressIndicatorIsShown()
        {
            // Arrange
            this.progressIndicatorService.SetupMessage();

            // Act
            this.viewModel.Activate();

            // Assert
            this.progressIndicatorService.VerifyAll();
        }

        private IProductsByCategoryViewModel CreateMockedProductsByCategoryViewModel()
        {
            var mock = new Mock<IProductsByCategoryViewModel>();
            mock.SetupAllProperties();
            return mock.Object;
        }
    }
}
