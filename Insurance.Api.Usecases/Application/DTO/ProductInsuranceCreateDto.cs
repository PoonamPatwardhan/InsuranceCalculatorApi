using System.Text.Json.Serialization;

namespace Insurance.Api.Application.DTO
{
    public class ProductInsuranceCreateDto
    {
        public int ProductId { get; set; }

        [JsonIgnore]
        public float InsuranceValue { get; set; }

        [JsonIgnore]
        public string ProductTypeName { get; set; }

        [JsonIgnore]
        public bool ProductTypeHasInsurance { get; set; }
        
        [JsonIgnore]
        public float SalesPrice { get; set; }
    }
}