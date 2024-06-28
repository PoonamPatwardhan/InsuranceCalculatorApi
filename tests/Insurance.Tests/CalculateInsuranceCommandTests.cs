using Insurance.Api.Application.Commands;
using Insurance.Api.Application.DTO;
using Xunit;
using System.Collections.Generic;

namespace Insurance.Tests
{
    public class CalculateInsuranceCommandTests
    {
        [Theory]
        [MemberData(nameof(GetProductIdWithExpectedInsurance))]
        public void CalculateInsuranceCommand_WhenGivenProductInsuranceDto_CalculatesExpectedInsuranceCostForIt(int productId, float expectedInsuranceValue, string testName)
        {
            //Arrange
            var toInsureDto = ProductInsuranceCreateDtos[productId];
            var sut = new CalculateInsuranceCommand();

            //Act 
            var toInsureReadDto = sut.Execute(toInsureDto);

            //Assert
            Assert.Equal(expected: expectedInsuranceValue,
                actual: toInsureReadDto.InsuranceValue);

            /*Assert.Equal(expected: expectedInsuranceDto.ProductTypeName, actual: toInsureReadDto.ProductTypeName);

            Assert.Equal(expected: expectedInsuranceDto.SalesPrice, actual: toInsureReadDto.SalesPrice);*/
        }

        [Theory]
        [MemberData(nameof(GetListOfIdsWithExpectedInsurance))]
        public void CalculateInsuranceCommand__WhenGivenListOfProductIds_CalculatesInsuranceCostForAllProducts(int[] productIds, float expectedInsuranceValue)
        {
            //Arrange
            List<ProductInsuranceCreateDto> insuranceDtos = new List<ProductInsuranceCreateDto>();
            foreach(var productId in productIds)
            {
                insuranceDtos.Add(ProductInsuranceCreateDtos[productId]);
            }
            var sut = new CalculateInsuranceCommand();

            //Act 
            var totalInsuranceValue = sut.Execute(insuranceDtos);

            //Assert
            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: totalInsuranceValue
            );
        }
        
        public static IEnumerable<object[]> GetProductIdWithExpectedInsurance()
        {
                return new List<object[]>
                {
                    new object[] { 1,0, "CalculateInsurance_GivenSalesPriceLessThan500Euros_ShouldAddZeroInsuranceCost"},
                    new object[] { 2,0, "CalculateInsurance_GivenInvalidSalesPrice_ShouldAddZeroInsuranceCost"},
                    new object[] { 3,1000, "CalculateInsurance_GivenSalesPriceBetween500And2000Euros_ShouldAdd1000EurosToInsuranceCost" },
                    new object[] { 4,1000, "CalculateInsurance_GivenSalesPriceIs500Euros_ShouldAdd1000EurosToInsuranceCost" },
                    new object[] { 5,2000, "CalculateInsurance_GivenSalesPriceAbove2000Euros_ShouldAdd2000EurosToInsuranceCost"},
                    new object[] { 6,2000, "CalculateInsurance_GivenSalesPriceIsLargeAmount_ShouldAdd2000EurosToInsuranceCost"},
                    new object[] { 7,2500, "CalculateInsurance_GivenProductTypeIsLaptopAndPriceAbove2000_ShouldAdd2500EurosToInsuranceCost" },
                    new object[] { 8,500, "CalculateInsurance_GivenProductTypeIsLaptopAndPriceIs0_ShouldAddOnly500EurosToInsuranceCost" },
                    new object[] { 9,2500, "CalculateInsurance_GivenProductTypeIsSmartphoneAndPriceAbove2000_ShouldAdd2500EurosToInsuranceCost" },
                    new object[] { 10,500, "CalculateInsurance_GivenProductTypeIsSmartphoneAndPriceBelow500_ShouldAddOnly500EurosToInsuranceCost" },
                    new object[] { 11,0 , "CalculateInsurance_GivenSalesPriceAbove2000EurosButProductCannotBeInsured_ShouldAddZeroInsuranceCost"},
                    new object[] { 12,0, "CalculateInsurance_GivenProductTypeIsSmartphoneButProductCannotBeInsured_ShouldAddZeroInsuranceCost"},
                    new object[] { 13,500, "CalculateInsurance_GivenProductTypeIsDigitalCameraAndPriceBelow500_ShouldAddOnly500EurosInsuranceCost"},
                    new object[] { 14,0, "CalculateInsurance_GivenProductTypeIsDigitalCameraButProductCannotBeInsured_ShouldAddZeroInsuranceCost"},
                };
        }

        public static IEnumerable<object[]> GetListOfIdsWithExpectedInsurance()
        {
                return new List<object[]>
                {
                    new object[] { new int[]{3} , 1000},
                    new object[] {  new int[]{ 3,5 }, 3000},
                    new object[] { new int[]{} , 0},
                    new object[] { new int[]{7,10,11,13} , 3500}
                };    
        }
        
        private Dictionary<int, ProductInsuranceCreateDto> ProductInsuranceCreateDtos=  new Dictionary<int, ProductInsuranceCreateDto>() {
                                                                            { 
                                                                                1, 
                                                                                new ProductInsuranceCreateDto()
                                                                                {
                                                                                    ProductTypeName = "",
                                                                                    ProductTypeHasInsurance = true,
                                                                                    SalesPrice = 100
                                                                                }
                                                                            },
                                                                            { 
                                                                                2, 
                                                                                new ProductInsuranceCreateDto()
                                                                                {
                                                                                    ProductTypeName = "",
                                                                                    ProductTypeHasInsurance = true,
                                                                                    SalesPrice = -100
                                                                                }
                                                                            },
                                                                            { 
                                                                                3, 
                                                                                new ProductInsuranceCreateDto()
                                                                                {
                                                                                    ProductTypeName = "",
                                                                                    ProductTypeHasInsurance = true,
                                                                                    SalesPrice = 750
                                                                                }
                                                                            },
                                                                            { 
                                                                                4, 
                                                                                new ProductInsuranceCreateDto()
                                                                                {
                                                                                    ProductTypeName = "",
                                                                                    ProductTypeHasInsurance = true,
                                                                                    SalesPrice = 500
                                                                                }
                                                                            },
                                                                            { 
                                                                                5, 
                                                                                new ProductInsuranceCreateDto()
                                                                                {
                                                                                    ProductTypeName = "",
                                                                                    ProductTypeHasInsurance = true,
                                                                                    SalesPrice = 2000
                                                                                }
                                                                            },
                                                                            { 
                                                                                6, 
                                                                                new ProductInsuranceCreateDto()
                                                                                {
                                                                                    ProductTypeName = "",
                                                                                    ProductTypeHasInsurance = true,
                                                                                    SalesPrice = 100000000000
                                                                                }
                                                                            } ,
                                                                            { 
                                                                                7, 
                                                                                new ProductInsuranceCreateDto()
                                                                                {
                                                                                    ProductTypeName = "Laptops",
                                                                                    ProductTypeHasInsurance = true,
                                                                                    SalesPrice = 5000000
                                                                                }
                                                                            },
                                                                            { 
                                                                                8, 
                                                                                new ProductInsuranceCreateDto()
                                                                                {
                                                                                    ProductTypeName = "Laptops",
                                                                                    ProductTypeHasInsurance = true,
                                                                                    SalesPrice = 0
                                                                                }
                                                                            },
                                                                            { 
                                                                                9, 
                                                                                new ProductInsuranceCreateDto()
                                                                                {
                                                                                    ProductTypeName = "Smartphones",
                                                                                    ProductTypeHasInsurance = true,
                                                                                    SalesPrice = 30000
                                                                                }
                                                                            },
                                                                            { 
                                                                                10, 
                                                                                new ProductInsuranceCreateDto()
                                                                                {
                                                                                    ProductTypeName = "Smartphones",
                                                                                    ProductTypeHasInsurance = true,
                                                                                    SalesPrice = 499
                                                                                }
                                                                            },
                                                                            { 
                                                                                11, 
                                                                                new ProductInsuranceCreateDto()
                                                                                {
                                                                                    ProductTypeName = "",
                                                                                    ProductTypeHasInsurance = false,
                                                                                    SalesPrice = 9000
                                                                                }
                                                                            },
                                                                            { 
                                                                                12, 
                                                                                new ProductInsuranceCreateDto()
                                                                                {
                                                                                    ProductTypeName = "Smartphones",
                                                                                    ProductTypeHasInsurance = false,
                                                                                    SalesPrice = 900
                                                                                }
                                                                            },
                                                                            { 
                                                                                13, 
                                                                                new ProductInsuranceCreateDto()
                                                                                {
                                                                                    ProductTypeName = "Digital cameras",
                                                                                    ProductTypeHasInsurance = true,
                                                                                    SalesPrice = 499
                                                                                }
                                                                            },
                                                                            { 
                                                                                14, 
                                                                                new ProductInsuranceCreateDto()
                                                                                {
                                                                                    ProductTypeName = "Digital cameras",
                                                                                    ProductTypeHasInsurance = false,
                                                                                    SalesPrice = 200
                                                                                }
                                                                            }};
    }
}
