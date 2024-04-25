using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using RestfulAPI.Models;
using RestfulAPI.Services;
using Newtonsoft.Json.Linq;

namespace RestfulAPI.Controllers
{
    public class DemoController : Controller
    {

        private readonly DataService _dataService;

        //Constructor - DI
        public DemoController(DataService dataService)
        {
            //initialize the DemoController with a DataService dependency
            _dataService = dataService;
        }

        //The following endpoints is for demonstrate only, It should not be Exposed 

        #region The following end points are for Demo purpose only 


        //Endpoint to demonstrate the correctness of "Merge the data from the /customers and /orders endpoints based on the customer_id column"
        [HttpGet("demo2-1")]
        public IActionResult GetMergedData()
        {
            var mergedData = _dataService.GetMergedData();
            return Ok(mergedData);
        }


        //Endpoint to demonstrate the correctness of "Calculate the total number of orders for each customer"
        [HttpGet("demo2-2")]
        public IActionResult GetTotalNumberOfOrdersForEachCustomer()
        {
            var result = _dataService.GetTotalOrders();

            return result;
        }

        //Endpoint to demonstrate the correctness of "Calculate the total amount spent by each customer"
        [HttpGet("demo2-3")]
        public IActionResult GetTotalAmountSpentByEachCustomer()
        {
            var result = _dataService.GetTotalAmountSpent();

            return result;
        }


        //Endpoint to demonstrate the correctness of "Using the data from the /order-items and /products endpoints, calculate the total quantity and total revenue for each product category"
        [HttpGet("demo2-4")]
        public IActionResult GetQuantityAndRevenueForEachProductCategory()
        {
            var result = _dataService.CalculateProductStats();

            return result;
        }

        //Endpoint to demonstrate the correctness of "Identify the top 5 customers based on the total amount spent"
        [HttpGet("demo2-5")]
        public IActionResult GetTopFiveCustomers()
        {
            var result = _dataService.IdentifyTop5Customers();

            return result;
        }

        //Endpoint to demonstrate the correctness of "For each country, find the most popular product category (based on the total quantity sold)"
        [HttpGet("demo2-6")]
        public IActionResult GetMostPopularProductCategory()
        {
            var result = _dataService.FindMostPopularProductCategoryByCountry();

            return result;
        }
        #endregion
    }
}
