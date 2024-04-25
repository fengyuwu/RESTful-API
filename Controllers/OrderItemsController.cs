using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using RestfulAPI.Models;

namespace RestfulAPI.Controllers
{
    public class OrderItemsController : Controller
    {

        private readonly string orderItemsFilePath = "Resources/order_items.json";


        //Endpoint to retrieve order items from a JSON file.
        [HttpGet("order-items")]
        public IActionResult GetOrderItems()
        {
            string jsonContent;
            using (StreamReader reader = new StreamReader(orderItemsFilePath))
            {
                jsonContent = reader.ReadToEnd();
            }

            var orderItems = JsonSerializer.Deserialize<List<OrderItems>>(jsonContent);

            return Ok(orderItems);//Returns the contents of order_items.json file.
        }
    }
}
