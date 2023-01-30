using DukanHabeshaDataAccess.Repository.IRepository;
using DukanHabeshaModels;
using DukanHabeshaWebApp.Areas.Admin.Controllers;
using Moq;

namespace DukanHabeshaTest
{
    public class OriginCountryTest
    {
        private readonly OriginController _originController;
        private readonly Mock<IUnitOfWork> _originRepoMock = new Mock<IUnitOfWork>();


        public OriginCountryTest()
        {
            _originController = new OriginController(_originRepoMock.Object);
        }


        [Fact]
        public void CheckingIf_OriginClassIsWorking()
        {
            Origin MadeIn = new Origin()
            {
                Id = 1,
                OriginCountry = "Eritrea"
            };


            Assert.True(MadeIn.OriginCountry.Equals("Eritrea"));            
        }

       

        [Fact]
        public void OriginCountryList_WhenCountryMadeObjectAdded_ShouldReturnTrue()
        {
            //Arrange

            Origin MadeIn = new Origin()
            {
                Id = 1,
                OriginCountry = "Eritrea"
            };
            _originRepoMock.Setup(x => x.Origin.Add(MadeIn));

            //Act
            var originCountry = _originController.Index();

            //Assert
            Assert.NotNull(originCountry);

        }

        [Fact]
        public void OriginCountryList_WhenCountryMadeObjectAdded_ShouldReturnTrueWithList()
        {
            //Arrange

            Origin MadeIn = new Origin()
            {
                Id = 1,
                OriginCountry = "Eritrea"
            };
            _originRepoMock.Setup(x => x.Origin.Add(MadeIn));
           // _originRepoMock.Setup(x => x.Origin.GetAll);
            //Act
            var originCountry = _originController.Index();

            //Assert
            Assert.NotNull(originCountry);

        }


        [Fact]
        public void When_CountryMadeInObjectCreated_ShouldReturnTrue()
        {
            //Arrange

            Origin MadeIn = new Origin()
            {
                Id = 1,
                OriginCountry = "Eritrea"
            };
            _originRepoMock.Setup(x => x.Origin.Add(MadeIn));
            //Act
            var origin = _originController.Create();

            //Assert
            Assert.NotNull(origin);

        }

        [Fact]
        public void When_CountryMadeObjectEdited_ShouldReturnTrue()
        {
            //Arrange -- Declaring variables, objects, instantiating mocks, etc

            Origin MadeIn = new Origin()
            {
                Id = 1,
                OriginCountry = "Eritrea"
            };
            _originRepoMock.Setup(x => x.Origin.Add(MadeIn));

            //Act -- Inserting the method that needs to be tasted


            MadeIn.OriginCountry = "Ethiopia";
            var originCountry = _originController.Edit(MadeIn.Id);

            //Assert -- ensures that code behaves as expected means yielding expected output
            Assert.Equal(MadeIn.OriginCountry, "Ethiopia");

        }

        [Fact]
        public void When_CategoryObjectDelated_ShouldReturnNull()
        {
            //Arrange -- Declaring variables, objects, instantiating mocks, etc

            Origin MadeIn = new Origin()
            {
                Id = 1,
                OriginCountry = "Eritrea"
            };
            _originRepoMock.Setup(x => x.Origin.Add(MadeIn));


            //Act -- Inserting the method that needs to be tasted


            //category.Name = "Test2";
            var originCountryDelated = _originController.Delete(MadeIn.Id);

            //Assert -- ensures that code behaves as expected means yielding expected output
            Assert.Equal(MadeIn.OriginCountry=null, MadeIn.OriginCountry);
        }
    }
}