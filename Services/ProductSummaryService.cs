using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Globalization;
using RestfulAPI.Models;

namespace RestfulAPI.Services
{

    public class ProductSummaryService
    {
        //Service class responsible for generating product summary


        //File paths for order items and products data
        private readonly string orderItemsFilePath = "Resources/order_items.json";
        private readonly string productsFilePath = "Resources/products.csv";

        //Logger instance for logging errors and information
        private readonly ILogger<ProductSummaryService> _logger;

        #region Constructor
        //Inject logger
        public ProductSummaryService(ILogger<ProductSummaryService> logger)
        {
            _logger = logger;
        }

        #endregion


        #region Get Product Level Info
        public IActionResult GetProductSummary()
        {


            try
            {
                // Read Data from json file
                List<OrderItems> orderItems;
                using (var reader = new StreamReader(orderItemsFilePath))
                {
                    string json = reader.ReadToEnd();
                    orderItems = JsonConvert.DeserializeObject<List<OrderItems>>(json);
                }

                // Read  data from products csv file 
                List<Products> products;
                using (var reader = new StreamReader(productsFilePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    products = csv.GetRecords<Products>().ToList();
                }

                // Merge order items and products data and save it for later use.
                var mergedData = from item in orderItems
                                 join product in products on item.product_id equals product.product_id
                                 select new
                                 {
                                     category = product.category,
                                     quantity = item.quantity,
                                     revenue = item.quantity * product.price
                                 };

                // Compute total quantity and total revenue for each product category
                var productSummary = mergedData
                    .GroupBy(x => x.category)
                    .Select(g => new
                    {
                        category = g.Key,
                        total_quantity = g.Sum(x => x.quantity),
                        total_revenue = g.Sum(x => x.revenue)
                    })
                    .ToList();

                return new JsonResult(productSummary);//Returns product-level information with the following fields: category,total_quantity,total_revenue
            }
            catch (Exception ex)
            {
                // Catch exception and log it. Also return status code
                _logger.LogError(ex, "An error occurred in product summary service");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            #endregion
        }
    }
}
