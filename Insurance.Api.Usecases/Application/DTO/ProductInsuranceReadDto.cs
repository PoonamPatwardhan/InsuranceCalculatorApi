using System.Text.Json.Serialization;

namespace Insurance.Api.Application.DTO
{
    public class ProductInsuranceReadDto
    {
        public float InsuranceValue { get; set; }

        public string ProductTypeName { get; set; }

        public float SalesPrice { get; set; }

        [JsonIgnore]
        public int ProductId { get; set; }

        [JsonIgnore]
        public bool ProductTypeHasInsurance { get; set; }
    }
}