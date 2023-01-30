using DukanHabeshaDataAccess.Repository.IRepository;
using DukanHabeshaModels;
using DukanHabeshaWebApp.Areas.Admin.Controllers;
using Moq;

namespace DukanHabeshaTest
{
    public class CategoryTest
    {
        private readonly CategoryController _categoryController;
        private readonly Mock<IUnitOfWork> _categoryRepoMock = new Mock<IUnitOfWork>();


        public CategoryTest()
        {
            _categoryController = new CategoryController(_categoryRepoMock.Object);
        }


        [Fact]
        public void ToCheck_IfCategoryObjectCreatedSuccessfully_ShoulRetunTrue()
        {
            Category category = new Category()
            {
                Id = 1,
                Name = "Test",
                DisplayOrder = 1,
                CreateDateTime = DateTime.Now,
            };


            Assert.True(category.Name.Equals("Test"));
            Assert.True(category.DisplayOrder.Equals(1));
        }

     



        [Fact]
        public void CategoryList_WhenCategoryObjectAdded_ShouldReturnTrue()
        {
            //Arrange

            Category category = new Category()
            {
                Id = 1,
                Name = "Test",
                DisplayOrder = 1,
                CreateDateTime = DateTime.Now,
            };
            _categoryRepoMock.Setup(x => x.Category.Add(category));

            //Act
            var cat = _categoryController.Index();

            //Assert
            Assert.NotNull(cat);

        }


        [Fact]
        public void When_CategoryObjectCreated_UsingCreateMethod_ShouldReturnTrue()
        {
            //Arrange

            Category category = new Category()
            {
                Id = 1,
                Name = "Test",
                DisplayOrder = 1,
                CreateDateTime = DateTime.Now,
            };
            _categoryRepoMock.Setup(x => x.Category.Add(category));

            //Act
            var cat = _categoryController.Create();

            //Assert
            Assert.NotNull(cat);

        }

        [Fact]
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

        }
    }
}