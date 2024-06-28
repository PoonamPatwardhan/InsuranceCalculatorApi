using Insurance.Api.Application.DTO;
using System.Collections.Generic;

namespace Insurance.Api.Application.Commands
{
    public interface ICalculateInsuranceCommand
    {
        public ProductInsuranceReadDto Execute(ProductInsuranceCreateDto productToInsureDto);

        public float Execute(List<ProductInsuranceCreateDto> products);
    }
}