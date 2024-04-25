using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using RestfulAPI.Models;

namespace RestfulAPI.Controllers
{
    public class ProductsController : Controller
    {

        private readonly string FilePath = "Resources/products.csv";

        [HttpGet("products")]
        public IActionResult GetProducuts()
        {
            //Load the csv file using CsvHelper Library
            List<Products> products;
            using (var reader = new StreamReader(FilePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                products = csv.GetRecords<Products>().ToList();
            }
            return Ok(products);//Return the contents of products.csv file in JSON format.
        }
    }


}
