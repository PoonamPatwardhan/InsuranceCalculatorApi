using Insurance.Api.Application.DTO;
using Insurance.Api.Application.Mappers;
using Insurance.Api.Domain;

namespace Insurance.Api.Application.Commands
{
    public class CalculateInsuranceCommand : ICalculateInsuranceCommand
    {
        private readonly ProductInsuranceDtoMapper entityDtoMapper;

        public CalculateInsuranceCommand()
        {
            entityDtoMapper = new ProductInsuranceDtoMapper();
        }

        public ProductInsuranceReadDto Execute(ProductInsuranceCreateDto productToInsureDto)
        {
            var actualProduct = entityDtoMapper.ToProductInsuranceEntity(productToInsureDto);

            CalculateInsuranceFor(actualProduct);            

            return entityDtoMapper.ToProductInsuranceReadDto(actualProduct);
        }

        public float Execute(List<ProductInsuranceCreateDto> insuranceDTOs)
        {
            var totalInsuranceValue = 0f;
            foreach(var insuranceDto in insuranceDTOs)
            {
                var calculatedInsuranceDto = Execute(insuranceDto);
                totalInsuranceValue += calculatedInsuranceDto != null ? calculatedInsuranceDto.InsuranceValue : 0;
            }
            return totalInsuranceValue;
        }

        private void CalculateInsuranceFor(ProductInsurance actualProductToInsure)
        {
            actualProductToInsure.CalculateInsurance();
        }

    }
}