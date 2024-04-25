using CsvHelper;
using Microsoft.AspNetCore.Mvc;

using System.Globalization;
using RestfulAPI.Models;
using Newtonsoft.Json;



namespace RestfulAPI.Services
{
    public class DataService
    {

        private readonly string orderItemsFilePath = "Resources/order_items.json";
        private readonly string productsFilePath = "Resources/products.csv";
        private readonly string customersFilePath = "Resources/customers.csv";
        private readonly string ordersFilePath = "Resources/orders.csv";
        private readonly ILogger<DataService> _logger;


        #region Constructor


        public DataService(ILogger<DataService> logger)
        {
            _logger = logger;
        }

        public DataService()
        {
        }

        #endregion

        #region Merge data based on customer_id 
        public List<object> GetMergedData()
        {
            try
            {
                // Read data from customers csv file
                List<Customers> customers;
                using (var reader = new StreamReader(customersFilePath)) // Read from the customers csv file in Resources folder.
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture)) // Initialize a CsvReader 
                {
                    customers = csv.GetRecords<Customers>().ToList(); // Pass the CSV records into a list of customers objects.
                }

                // Read  data  from orders csv file.
                List<Order> orders;
                using (var reader = new StreamReader(ordersFilePath)) // Open a stream reader to read from the orders CSV file
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture)) // Initialize a CsvReader with the stream reader and culture settings
                {
                    orders = csv.GetRecords<Order>().ToList(); // Read the CSV records into a list of Order objects
                }

                // Merge the data based on the customer_id column. 
                var mergedData = customers.Select(c =>
                {
                    var customerOrders = orders.Where(o => o.customer_id == c.customer_id).ToList(); // Filter orders for the current customer
                    return new
                    {
                        customer_id = c.customer_id,
                        first_name = c.first_name,
                        last_name = c.last_name,
                        email = c.email,
                        country = c.country,
                        total_orders = customerOrders.Count(), // Compute the total number of orders for the customer
                        total_amount_spent = customerOrders.Sum(o => o.total_amount) // Compute the total amount spent by the customer
                    };
                }).ToList<object>();

                return mergedData; // Return summarized information based on customer_id 
            }
            catch (Exception ex)
            {
                // Log and throw exception if read is bad
                Console.WriteLine($"backend error while reading csv files: {ex.Message}");
                throw; 
            }
        }

        #endregion

        #region Calculate the total number of orders for each customer
        public IActionResult GetTotalOrders()
        {
            try
            {
                // Read data from the orders csv file
                List<Order> orders;
                using (var reader = new StreamReader(ordersFilePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    orders = csv.GetRecords<Order>().ToList();
                }

                // Group orders by customer_id and compute total orders for each customer
                var totalOrdersByCustomer = orders
                    .GroupBy(o => o.customer_id) // Group orders by customer_id
                    .Select(g => new
                    {
                        customer_id = g.Key, // Customer ID, identify the customer
                        total_orders = g.Count() // Count of orders for each 
                    })
                    .ToList();

                // Return response in Json format, can also get name by using join
                return new JsonResult(totalOrdersByCustomer);
            }
            catch (Exception ex)
            {
                // Log any exceptions that occur during data processing
        
                Console.WriteLine($"Error in backend: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion

        #region Calculate the total amount spent by each customer
        public IActionResult GetTotalAmountSpent()
        {
            try
            {
                // Read data from orders csv file. 
                List<Order> orders;
                using (var reader = new StreamReader(ordersFilePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    orders = csv.GetRecords<Order>().ToList();
                }

                // Calculate total amount spent by each customer
                var totalAmountSpentByCustomer = orders
                    .GroupBy(o => o.customer_id) // Group orders by customer_id
                    .Select(g => new
                    {
                        customer_id = g.Key,  // Customer ID, identify the customer
                        total_amount_spent = g.Sum(o => o.total_amount) //  Amount spent by this customer
                    })
                    .ToList();

                //Return response in Json format, can also get name by doing another join
                return new JsonResult(totalAmountSpentByCustomer);
            }
            catch (Exception ex)
            {
                //Handle Exception and log it
                Console.WriteLine($"Error occurred while calculating total amount spent: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion

        #region Calculate the total quantity and total revenue for each product category
        public IActionResult CalculateProductStats()
        {


       
            try
            {
                // Read order items data from file
                List<OrderItems> orderItems;
                using (var reader = new StreamReader(orderItemsFilePath))
                {
                    string json = reader.ReadToEnd();
                    orderItems = JsonConvert.DeserializeObject<List<OrderItems>>(json);
                }

                // Read products data from CSV file
                List<Products> products;
                using (var reader = new StreamReader(productsFilePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    products = csv.GetRecords<Products>().ToList();
                }

                // Merge order items and products data
                var mergedData = from item in orderItems
                                 join product in products on item.product_id equals product.product_id
                                 select new
                                 {
                                     category = product.category,
                                     quantity = item.quantity,
                                     revenue = item.quantity * product.price
                                 };

                // Calculate total quantity and total revenue for each product category
                var productStats = mergedData
                    .GroupBy(x => x.category)
                    .Select(g => new
                    {
                        category = g.Key,
                        total_quantity = g.Sum(x => x.quantity),
                        total_revenue = g.Sum(x => x.revenue)
                    })
                    .ToList();

                return new JsonResult(productStats);
            }
            catch (Exception ex)
            {
                // Log any exceptions that occur during file reading or data processing
                _logger.LogError(ex, "An error occurred while calculating product details.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion

        #region Identify the top 5 customers based on the total amount spent
        public IActionResult IdentifyTop5Customers()
        {
            try
            {
                // Read orders data from CSV file
                List<Order> orders;
                using (var reader = new StreamReader(ordersFilePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    orders = csv.GetRecords<Order>().ToList();
                }

                // Read customers data from CSV file
                List<Customers> customers;
                using (var reader = new StreamReader(customersFilePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    customers = csv.GetRecords<Customers>().ToList();
                }

                // Calculate total amount spent by each customer
                var totalAmountSpentByCustomer = orders
                    .GroupBy(o => o.customer_id)
                    .Select(g => new
                    {
                        customer_id = g.Key,
                        total_amount_spent = g.Sum(o => o.total_amount)
                    })
                    .ToList();

                // Join with customers data to get customer details and pick top five based on total amount spent
                var top5Customers = (from tac in totalAmountSpentByCustomer
                                     join c in customers on tac.customer_id equals c.customer_id
                                     orderby tac.total_amount_spent descending
                                     select new
                                     {
                                         customer_id = c.customer_id,
                                         first_name = c.first_name,
                                         last_name = c.last_name,
                                         email = c.email,
                                         country = c.country,
                                         total_amount_spent = tac.total_amount_spent
                                     }).Take(5).ToList();

                return new JsonResult(top5Customers);
            }
            catch (Exception ex)
            {
                // Log any exceptions that occur during data processing
                _logger.LogError(ex, "An error occurred while getting the top 5 customers.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion

        #region Calculate most popular product category 
        public IActionResult FindMostPopularProductCategoryByCountry()
        {


            try
            {
                // Read order_items Json
                List<OrderItems> orderItems;
                using (StreamReader reader = new StreamReader(orderItemsFilePath))
                {
                    string json = reader.ReadToEnd();
                    orderItems = JsonConvert.DeserializeObject<List<OrderItems>>(json);
                }

                // Read products csv
                List<Products> products;
                using (var reader = new StreamReader(productsFilePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    products = csv.GetRecords<Products>().ToList();
                }

                // Read customers csv
                List<Customers> customers;
                using (var reader = new StreamReader(customersFilePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    customers = csv.GetRecords<Customers>().ToList();
                }

                // Read orders csv
                List<Order> orders;
                using (var reader = new StreamReader(ordersFilePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    orders = csv.GetRecords<Order>().ToList();
                }

                // Merge order items, products, orders, and customers data and store it.
                var mergedData = from item in orderItems
                                 join product in products on item.product_id equals product.product_id
                                 join order in orders on item.order_id equals order.order_id
                                 join customer in customers on order.customer_id equals customer.customer_id
                                 select new
                                 {
                                     country = customer.country,
                                     category = product.category,
                                     quantity = item.quantity
                                 };

                // Calculate total quantity sold for each product category by country based on mergedData
                var popularProductCategoriesByCountry = mergedData
                    .GroupBy(x => new { x.country, x.category })
                    .Select(g => new
                    {
                        country = g.Key.country,
                        category = g.Key.category,
                        total_quantity_sold = g.Sum(x => x.quantity)
                    })
                    .GroupBy(x => x.country)
                    .Select(g =>
                    {
                        // Find the most popular category by total quantity sold
                        var mostPopularCategory = g.OrderByDescending(x => x.total_quantity_sold).FirstOrDefault();
                        return new
                        {
                            country = g.Key,
                            most_popular_category = mostPopularCategory?.category,
                            total_quantity_sold = mostPopularCategory?.total_quantity_sold ?? 0
                        };
                    })
                    .ToList();

                return new JsonResult(popularProductCategoriesByCountry);
            }
            catch (Exception ex)
            {
                // Log exception and return error response
                _logger.LogError(ex, "Error occurred when finding the most popular product category");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion

        #region Identify the top 5 customers based on the total amount spent Selected
        public IActionResult IdentifyTop5CustomersSelected()
        {
            try
            {
                // Read orders data from CSV file
                List<Order> orders;

                using (var reader = new StreamReader(ordersFilePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    orders = csv.GetRecords<Order>().ToList();
                }

                // Read customers data from CSV file
                List<Customers> customers;
                using (var reader = new StreamReader(customersFilePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    customers = csv.GetRecords<Customers>().ToList();
                }

                // Calculate total amount spent by each customer
                var totalAmountSpentByCustomer = orders
                    .GroupBy(o => o.customer_id)
                    .Select(g => new
                    {
                        customer_id = g.Key,
                        total_amount_spent = g.Sum(o => o.total_amount)
                    })
                    .ToList();

                // Join with customers data to get customer details and select top 5 based on total spent
                var top5Customers = (from tac in totalAmountSpentByCustomer
                                     join c in customers on tac.customer_id equals c.customer_id
                                     orderby tac.total_amount_spent descending
                                     select new
                                     {
                                         customer_id = c.customer_id,
                                         first_name = c.first_name,
                                         last_name = c.last_name,
                                         total_amount_spent = tac.total_amount_spent
                                     }).Take(5).ToList();

                return new JsonResult(top5Customers);
            }
            catch (Exception ex)
            {
                // Log exceptions that occur during processing
                _logger.LogError(ex, "An error occurred while getting the top 5 customers.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }



        #endregion

    }
}
