Coolblue homework assignment - This assignment involved creating an Insurance API that calculated insurance values based on product type and sales price.
The design decisons/assumptions made while implementing the assignment are as follws:
1)	Decision : Controller delegates the operation of insurance calculation to the application layer (via interfaces of Service and Command modules). I also moved the insurance calculation logic specifically to the domain layer.
    Reason : Clean architecture principles, which promote separation of concerns between controllers (presentation layer) and business logic (application layer). The calculation was moved to Domain layer so that the domain objects encapsulate
  	         both the data AND also behavior associated with that data. This also follows SRP – to have higher cohesion.
  	
2)	Decision in Api endpoint url : URL is now - “api/insurance/products/{id}/calculate-insurance”
    Reason : This url makes it clear to clients on what to expect from it (principle of least astonishment), and it will also not be confused with create endpoint of ProductsDataApi.
  	
3)	Assumption/Decision : Insurance DTO split into Read and Create. Since Product Id was not used anywhere in existing business code, I assumed that ID is irrelevant for insurance products, and hence did not add it to ReadDto in response.
    On the other hand, for input of the API, only the ID of InsuranceDto was being used, there was no need of insurance value. 
    Reason : User should see only the fields that are relevant to him while passing data to request and receiving response from the API.  If user can manipulate insurance value, even before passing it to API, it can introduce some side effects
  	or bugs.
  	
4)	Decision for signature of Commands : The methods inside ICalculateInsuranceCommand do not have void as return type.
    Reason : For the purpose of this API, which does not store any data in persistent storage, I am returning the calculated values.
  	
5)	Assumption : Assumed no validation rules need to be applied for fields inside ProductInsuranceDto.
    Reason : Assumed those properties will have validations in ProductDataAPI service. 

NOTE - A word document has also been uploaded with code, which documents the reasons behind using Clean architecture and Service Layer pattern.
