using Insurance.Api.Application.DTO;

namespace Insurance.Api.Application.Interfaces
{
    public interface IInsuranceService
    {
        public ProductInsuranceReadDto CalculateInsuranceForProduct(int productId);
    }
}