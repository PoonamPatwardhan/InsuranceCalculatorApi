using Insurance.Api.Application.DTO;
using Insurance.Api.Application.Interfaces;
using Insurance.Api.Web.Controllers;
using Xunit;

namespace Insurance.Tests
{
    public class InsuranceControllerTests
    {

        [Fact]
        public void InsuranceController_GivenInsuranceProductFound_ShouldReturnInsuranceDtoForThatProduct()
        {
            //Arrange
            var sut = SetupController();
            var toInsureDto = new ProductInsuranceCreateDto();

            //Act
            var insuranceDtoResponse = sut.CalculateInsuranceForProduct(toInsureDto);
            //Assert
            Assert.NotNull(insuranceDtoResponse);  
        }

        /*[Fact]
        public void InsuranceController_GivenInvalidInput_ShouldReturnBadRequestResponse()
        {
            //Arrange
            var sut = SetupController();
            //Act 
            var exception = Record.Exception(() => sut.CalculateInsuranceForOrder(null));
            Assert.NotNull(exception);       
        }


        [Fact]
        public void InsuranceController_GivenListOfProducts_ShouldCalculateInsurnaceWithNoErrorsThrown()
        {
            //Arrange
            var sut = SetupController();
            
            //Assert
            var exception = Record.Exception(() => sut.CalculateInsuranceForOrder(new List<int>() {}));
            Assert.Null(exception);
            
        }*/

        private InsuranceController SetupController()
        {
            //ICalculateInsuranceCommand dummyCalculateCommand = new DummyCalculateInsuranceCommand();
            IInsuranceService dummyInsuranceService = new DummyInsuranceService();
            return new InsuranceController(dummyInsuranceService, null);
        }
    }


    class DummyInsuranceService : IInsuranceService
    {
        public ProductInsuranceReadDto CalculateInsuranceForProduct(int productId)
        {
            return new ProductInsuranceReadDto();
        }
    }

    class DummyInsuranceServiceForInvalidInput : IInsuranceService
    {
        public ProductInsuranceReadDto CalculateInsuranceForProduct(int productId)
        {
            return null;
        }
    } 
}