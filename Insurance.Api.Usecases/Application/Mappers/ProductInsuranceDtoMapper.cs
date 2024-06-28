using Insurance.Api.Application.DTO;
using Insurance.Api.Domain;

namespace Insurance.Api.Application.Mappers
{
    public class ProductInsuranceDtoMapper
    {
        public ProductInsurance ToProductInsuranceEntity(ProductInsuranceCreateDto productInsuranceDto)
        {
            ProductInsurance productToInsure = new ProductInsurance()
            {
                ProductId = productInsuranceDto.ProductId,
                ProductTypeName = productInsuranceDto.ProductTypeName,
                ProductTypeHasInsurance = productInsuranceDto.ProductTypeHasInsurance,
                SalesPrice = productInsuranceDto.SalesPrice,
                InsuranceValue = productInsuranceDto.InsuranceValue
            };
            return productToInsure;
        }

        public ProductInsuranceReadDto ToProductInsuranceReadDto(ProductInsurance product)
        {
            ProductInsuranceReadDto insuredProductDto = new ProductInsuranceReadDto()
            {
                InsuranceValue = product.InsuranceValue,
                ProductTypeName = product.ProductTypeName,
                SalesPrice = product.SalesPrice                
            };
            return insuredProductDto;
        }
    }
}