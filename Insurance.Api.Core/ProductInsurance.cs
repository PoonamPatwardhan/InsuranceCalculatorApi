
namespace Insurance.Api.Domain
{
    public class ProductInsurance
    {
        public int ProductId { get; set; }
        public float InsuranceValue { get; set; }
        public string ProductTypeName { get; set; }
        public bool ProductTypeHasInsurance { get; set; }
        public float SalesPrice { get; set; }

        public void CalculateInsurance() 
        {
            InsuranceValue = 0;
            if (!ProductTypeHasInsurance)
                return;

            if (isWithinLowerRange(SalesPrice))    
                InsuranceValue += 1000;

            else if (isBeyondLowerRange(SalesPrice))
                InsuranceValue += 2000;

            if (productTypeIsInsurable(ProductTypeName))
                InsuranceValue += 500;
        } 

        private bool isWithinLowerRange(float salesPrice)
        {
            return salesPrice >= lowerPriceLimit && salesPrice < upperPriceLimit;
        }

        private bool isBeyondLowerRange(float salesPrice)
        {
            return salesPrice >= upperPriceLimit;
        }

        private bool productTypeIsInsurable(string typeName)
        {
            return productsTypesToInsure.Any(productTypeName => typeName.ToUpper() == productTypeName.ToUpper());
        }

        private const int lowerPriceLimit = 500;
        private const int upperPriceLimit = 2000;
        private List<string> productsTypesToInsure = new List<string> {"Laptops", "Smartphones", "Digital cameras"};
    }
}