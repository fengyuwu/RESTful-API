using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using RestfulAPI.Models;

namespace RestfulAPI.Controllers
{
    public class CustomersController : Controller
    {
        //Location to the customers CSV file
        private readonly string customersFilePath = "Resources/customers.csv";


        [HttpGet("customers")]
        public IActionResult GetCustomers()
        {
            List<Customers> customers;

            // Read the CSV file and deserialize it
            using (var reader = new StreamReader(customersFilePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                customers = csv.GetRecords<Customers>().ToList();
            }

            //Returns the contents of customers.csv file in JSON format
            return Ok(customers);
        }
    }
}
