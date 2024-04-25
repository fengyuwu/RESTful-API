using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using RestfulAPI.Models;

namespace RestfulAPI.Controllers
{
    public class OrdersController : Controller
    {
        //File path
        private readonly string ordersFilePath = "Resources/orders.csv";

        //Endpoint
        [HttpGet("orders")]
        public IActionResult GetOrders()
        {
            List<Order> orders;
            using (var reader = new StreamReader(ordersFilePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                orders = csv.GetRecords<Order>().ToList();
            }
            return Ok(orders);//Response Returns the contents of orders.csv file in JSON format.
        }
    }
}
