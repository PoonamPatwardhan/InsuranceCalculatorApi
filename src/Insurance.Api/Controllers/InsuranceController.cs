using System;
using System.Diagnostics;
using Insurance.Api.Application.DTO;
using Insurance.Api.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Api.Controllers
{
    [ApiController]
    public partial class InsuranceController : Controller
    {
        private ILogger<InsuranceController> logger;
        private readonly IInsuranceService insuranceService;

        public InsuranceController(IInsuranceService insuranceService, ILogger<InsuranceController> logger)
        {
            this.insuranceService = insuranceService;
            this.logger = logger;
        }

        [HttpGet]
        [Route("api/insurance/products/{productId}/calculate-insurance")]
        public ActionResult<ProductInsuranceReadDto> CalculateInsuranceForProduct([FromRoute] int productId)
        {
            try
            {
                var calculatedInsuranceReadDto = insuranceService.CalculateInsuranceForProduct(productId);

                return Ok(calculatedInsuranceReadDto);
            }
            catch (Exception exception)
            {
                logger.LogError(exception,
                "An exception occured while calculating insurance for product {ProductId}, Exception : {Message}, TraceId: {TraceId}",
                 productId, exception.Message, Activity.Current?.Id);

                return BadRequest("Unable to calculate insurance for given product");
            }
        }

        /*[HttpPost]
        [Route("api/insurance/products/calculate-insurance")]
        public ActionResult<int> CalculateInsuranceForOrder([FromBody]List<int> productIds)
        {  
            try
            { 
                var productDTOs = new List<ProductInsuranceCreateDto>();         
                foreach (var id in productIds)
                {
                    var insuranceDto = insuranceService.GetInsuranceDtoByProductIdAndType(id);
                    if (insuranceDto != null)
                        productDTOs.Add(insuranceDto);
                }  
                
                var totalInsuranceValue = commandToCalculateInsurance.Execute(productDTOs); 
                return Ok(totalInsuranceValue);
            }
            catch(Exception exception)  
            {
                logger.LogError(exception,
                "An exception occured while calculating insurance for given set of products, Exception : {exception.Message}, TraceId: {TraceId}",
                exception.Message, Activity.Current?.Id);
                
                return BadRequest("Unable to calculate insurance for given products");
            }
        }*/

    }
}