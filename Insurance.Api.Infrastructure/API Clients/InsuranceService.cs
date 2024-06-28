using System.Reflection.Metadata.Ecma335;
using Insurance.Api.Application.Commands;
using Insurance.Api.Application.DTO;
using Insurance.Api.Application.Interfaces;
using Newtonsoft.Json;

namespace Insurance.Infrastructure.API_Clients
{
    public class InsuranceService : IInsuranceService
    {
        private HttpClient client;
        private const string productApi = "http://localhost:5002";
        private readonly ICalculateInsuranceCommand commandToCalculateInsurance;

        public InsuranceService(ICalculateInsuranceCommand commandToCalculateInsurance)
        {
            client = new HttpClient { BaseAddress = new Uri(productApi) };
            this.commandToCalculateInsurance = commandToCalculateInsurance;
        }

        public ProductInsuranceReadDto? CalculateInsuranceForProduct(int productId)
        {
            var insuredProductDto = GetInsuranceDtoByProductIdAndType(productId);
            return insuredProductDto == null ? null : commandToCalculateInsurance.Execute(insuredProductDto);
        }

        private ProductInsuranceCreateDto? GetInsuranceDtoByProductIdAndType(int productId)
        {
            var productJson = GetProductById(productId);
            var product = JsonConvert.DeserializeObject<dynamic>(productJson);
            if (product == null) return null;

            var jsonProductTypes = GetProductTypes();
            var productTypes = JsonConvert.DeserializeObject<dynamic>(jsonProductTypes);
            var productToInsureDto = new ProductInsuranceCreateDto();
            bool productToInsureFound = false;

            foreach (var productType in productTypes)
            {
                if (productType.id == product.productTypeId && productType.canBeInsured == true)
                {
                    productToInsureDto.ProductTypeName = productType.name;
                    productToInsureDto.ProductTypeHasInsurance = true;
                    productToInsureFound = true;
                }
            }

            if (!productToInsureFound) return null;
            productToInsureDto.SalesPrice = product.salesPrice;
            return productToInsureDto;
        }

        private string GetProductById(int productId)
        {
            try
            {
                var productJson = client.GetAsync(string.Format("/products/{0:G}", productId)).Result.Content
                    .ReadAsStringAsync().Result;
                return productJson;
            }
            catch (HttpRequestException httpException)
            {
                throw new Exception($"Could not get product by ID, due to exception : {httpException.Message}",httpException);
            }
        }

        private string GetProductTypes()
        {
            try
            {
                var jsonProductTypes = client.GetAsync("/product_types").Result.Content.ReadAsStringAsync().Result;
                return jsonProductTypes;
            }
            catch (HttpRequestException httpException)
            {
                throw httpException;
            }

            return null;
        }
    }
}