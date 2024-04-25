using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;
using System.Globalization;
using RestfulAPI.Models;
using RestfulAPI.Services;

namespace RestfulAPI.Controllers
{
    // Controller for handling GET /customer-summary requests.
    public class CustomersSummaryController : Controller
    {


        private readonly CustomerSummaryService _customerSummaryService;

        //Constructor with a CustomerSummaryService dependency injected.
        public CustomersSummaryController(CustomerSummaryService customerSummaryService)
        {
            _customerSummaryService = customerSummaryService;
        }


        [HttpGet("customer-summary")]
        
        public IActionResult GetCustomerSummary()
        {

            //Retrieve customer summary data from the service
            var customerSummary = _customerSummaryService.GetCustomerSummary();

            //Returns customer-level information with the following fields: customer_id,first_name,last_name,email,country,total_orders,total_amount_spent
            return customerSummary;
        }

    }
}
