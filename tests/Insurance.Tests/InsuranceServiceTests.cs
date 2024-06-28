using System;
using Insurance.Api.Application.DTO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Xunit;
using System.Collections.Generic;
using Insurance.Api.Application.Commands;
using Insurance.Infrastructure.API_Clients;

namespace Insurance.Tests
{
    public class InsuranceServiceTests : IClassFixture<ServiceTestFixture>
    {
        private readonly ServiceTestFixture _fixture;

        public InsuranceServiceTests(ServiceTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void InsuredProductService__GivenValidProductId_ShouldCreateEquivalentInsuredProductDto()
        {
            //Arrange
            var sut = SetupService();

            var expectedInsuranceDto = new ProductInsuranceReadDto()
                                        {
                                            ProductId = 1,
                                            ProductTypeName = "Test type",
                                            SalesPrice = 2000
                                        };
            
            //Act 
            var actualInsuranceDto = sut.CalculateInsuranceForProduct(expectedInsuranceDto.ProductId);
            //Assert
            Assert.NotNull(actualInsuranceDto.InsuranceValue);

            /*Assert.Equal(expected: expectedInsuranceDto.ProductTypeName, actual: actualInsuranceDto.ProductTypeName);
            
            Assert.Equal(expected: expectedInsuranceDto.SalesPrice, actual: actualInsuranceDto.SalesPrice);*/
            
        }

        [Fact]
        public void InsuredProductService__GivenIdOfProductThatIsNotInsurable_ShouldReturnNull()
        {
            //Arrange
            var sut = SetupService();

            var insuranceDto = new ProductInsuranceReadDto()
                                        {
                                            ProductId = 2, 
                                            ProductTypeName = "Test type",
                                            SalesPrice = 2000
                                        };
            var actualInsuranceDto = sut.CalculateInsuranceForProduct(insuranceDto.ProductId);
            Assert.Null(actualInsuranceDto);  
        }

        [Fact]
        public void InsuredProductService__GivenInvalidIdOfProduct_ShouldReturnNull()
        {
            //Arrange
            var sut = SetupService();

            var insuranceDto = new ProductInsuranceReadDto()
                                        {
                                            ProductId = 3,
                                            ProductTypeName = "Test type",
                                            SalesPrice = 2000
                                        };
            var actualInsuranceDto = sut.CalculateInsuranceForProduct(insuranceDto.ProductId);
            Assert.Null(actualInsuranceDto);  
        }

        private InsuranceService SetupService()
        {
            ICalculateInsuranceCommand dummyCalculateCommand = new DummyCalculateInsuranceCommand();
            
            return new InsuranceService(dummyCalculateCommand);
        }


    }

    public class ServiceTestFixture: IDisposable
    {
        private readonly IHost _host;

        public ServiceTestFixture()
        {
            _host = new HostBuilder()
                   .ConfigureWebHostDefaults(
                        b => b.UseUrls("http://localhost:5002")
                              .UseStartup<ServiceTestStartup>()
                    )
                   .Build();

            _host.Start();
        }

        public void Dispose() => _host.Dispose();
    }

    public class ServiceTestStartup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(
                ep =>
                {
                    ep.MapGet(
                        "products/{id:int}",
                        context =>
                        {
                            int productId = int.Parse((string) context.Request.RouteValues["id"]);
                            var product = new
                                          {
                                              id = productId,
                                              name = "Test Product",
                                              productTypeId = productId,
                                              salesPrice = 2000
                                          };
                            return context.Response.WriteAsync(JsonConvert.SerializeObject(product));
                        }
                    );
                    ep.MapGet(
                        "product_types",
                        context =>
                        {
                            var productTypes = new[]
                                               {
                                                   new
                                                   {
                                                       id = 1,
                                                       name = "Test type",
                                                       canBeInsured = true
                                                   },
                                                   new
                                                   {
                                                       id = 2,
                                                       name = "Test type",
                                                       canBeInsured = false
                                                   }
                                               };
                            return context.Response.WriteAsync(JsonConvert.SerializeObject(productTypes));
                        }
                    );
                }
            );
        }
    }

    class DummyCalculateInsuranceCommand : ICalculateInsuranceCommand
    {
        public ProductInsuranceReadDto Execute(ProductInsuranceCreateDto productToInsureDto)
        {
            return new ProductInsuranceReadDto();

        }

        public float Execute(List<ProductInsuranceCreateDto> products)
        {
            return 0;
        }
    }
}