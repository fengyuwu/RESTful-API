using Microsoft.AspNetCore.Mvc;
using RestfulAPI.Services;

namespace RestfulAPI.Controllers
{
    public class TopCustomersController : ControllerBase
    {
        private readonly DataService _dataService;



        // Constructor DI for DataService
        public TopCustomersController(DataService dataService)
        {
            _dataService = dataService;
        }


        //API Endpoint to get the top 5 customers
        [HttpGet("top-customers")]
        public IActionResult GetTopCustomers()
        {
            // Call the service method to identify top 5 customers and return result in Json Format
            var topCustomers = _dataService.IdentifyTop5CustomersSelected();




            return (topCustomers);
        }
    }
}
