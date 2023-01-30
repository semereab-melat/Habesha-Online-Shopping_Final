using DukanHabeshaDataAccess.Repository.IRepository;
using DukanHabeshaModels;
using DukanHabeshaWebApp.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;

namespace DukanHabeshaTest
{
    public class ProductTest
    {
        private readonly ProductController _productController;
        private readonly OrderController _orderController;
        private readonly Mock<IUnitOfWork> _productRepoMock = new Mock<IUnitOfWork>();
        private readonly Mock<IWebHostEnvironment> _productHostEnvironmentMock = new Mock<IWebHostEnvironment>();


        public ProductTest()
        {
            _productController = new ProductController(_productRepoMock.Object, _productHostEnvironmentMock.Object);
            _orderController = new OrderController(_productRepoMock.Object);
        }


        [Fact]
        public void Checking_If_CreatingNewProduct_IsWorking()
        {
            Product product = new Product()
            {
                Id = 1,
                Name = "Test",
                Description = "Good Quality",
                Price = 20,
                ImageUrl = "Test",
                CategoryId = 1,
                OriginId = 1,    
            };


            Assert.True(product.Name.Equals("Test"));
            Assert.True(product.Description.Equals("Good Quality"));
        }

        [Fact]
        public void Products_WhenProductObjectAdded_IndexMethodShouldReturnTrue()
        {
            //Arrange

            Product product = new Product()
            {
                Id = 1,
                Name = "Test",
                Description = "Good Quality",
                Price = 20,
                ImageUrl = "Test",
                CategoryId = 1,
                OriginId = 1,
            };

            _productRepoMock.Setup(x => x.Product.Add(product));

            //Act
            var productOne = _productController.Index();

            //Assert
            Assert.NotNull(productOne);

        }


        [Fact]
        public void WhenProductAdded_ToShoppingcart_ShouldReturnTrue()
        {
            //Arrange

            Product product = new Product()
            {
                Id = 1,
                Name = "Test",
                Description = "Good Quality",
                Price = 20,
                ImageUrl = "Test",
                CategoryId = 1,
                OriginId = 1,
            };

            
            _productRepoMock.Setup(x => x.Product.Add(product));
            ShoppingCart shopping = new ShoppingCart();
           
            //Act
            var productOne = _orderController.Index();
         
            Assert.NotNull(productOne);

        }

       /* [Fact]
        public void When_CategoryObjectEdited_ShouldReturnTrue()
        {
            //Arrange -- Declaring variables, objects, instantiating mocks, etc

            Category category = new Category()
            {
                Id = 1,
                Name = "Test",
                DisplayOrder = 1,
                CreateDateTime = DateTime.Now,
            };
            _categoryRepoMock.Setup(x => x.Category.Add(category));


            //Act -- Inserting the method that needs to be tasted


            category.Name = "Test2";
            var catagory = _categoryController.Edit(category.Id);

            //Assert -- ensures that code behaves as expected means yielding expected output
            Assert.Equal(category.Name, "Test2");

        }

        [Fact]
        public void When_CategoryObjectDelated_ShouldReturnNull()
        {
            //Arrange -- Declaring variables, objects, instantiating mocks, etc

            Category category = new Category()
            {
                Id = 1,
                Name = "Test",
                DisplayOrder = 1,
                CreateDateTime = DateTime.Now,
            };
            _categoryRepoMock.Setup(x => x.Category.Add(category));


            //Act -- Inserting the method that needs to be tasted


            //category.Name = "Test2";
            var catagoryDelated = _categoryController.Delete(category.Id);

            //Assert -- ensures that code behaves as expected means yielding expected output
            Assert.Equal(category.Name=null, category.Name);

        }*/
    }
}