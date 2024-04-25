using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using RestfulAPI.Models;

namespace RestfulAPI.Services
{
    public class CustomerSummaryService
    {

        private readonly string customersFilePath = "Resources/customers.csv";
        private readonly string ordersFilePath = "Resources/orders.csv";
        private readonly ILogger<CustomerSummaryService> _logger;

        #region Constructor
        //ILogger DI
        public CustomerSummaryService(ILogger<CustomerSummaryService> logger)
        {
            _logger = logger;
        }

        #endregion

        #region Get Customer Level Info

        public IActionResult GetCustomerSummary()
        {
            try
            {
                // Read customers info from csv
                List<Customers> customers;
                using (var reader = new StreamReader(customersFilePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    customers = csv.GetRecords<Customers>().ToList();
                }

                // Read data from orders.csv
                List<Order> orders;
                using (var reader = new StreamReader(ordersFilePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    orders = csv.GetRecords<Order>().ToList();
                }

                // Compute the total orders & total amount spent for individual customer
                var customerSummary = customers.Select(customer => new
                {
                    customer_id = customer.customer_id,
                    first_name = customer.first_name,
                    last_name = customer.last_name,
                    email = customer.email,
                    country = customer.country,
                    total_orders = orders.Count(order => order.customer_id == customer.customer_id),
                    total_amount_spent = orders.Where(order => order.customer_id == customer.customer_id).Sum(order => order.total_amount)
                }).ToList<object>();

                return new JsonResult(customerSummary); //returning a JSON response:
            }
            catch (Exception ex)
            {
                // Log the error and return status code
                _logger.LogError(ex, "Back end error when getting customer level info");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion
    }
}
