using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Formats.Asn1;
using System.Globalization;
using RestfulAPI.Models;
using RestfulAPI.Services;

namespace RestfulAPI.Controllers
{
    public class ProductSummaryController : Controller
    {
 
        private readonly ProductSummaryService _productSummaryService;

        // Constructor with dependency injection for ProductSummaryService.
        public ProductSummaryController(ProductSummaryService productSummaryService)
        {
            _productSummaryService = productSummaryService;
        }


        [HttpGet("product-summary")]

        public IActionResult GetProductSummary()
        {
            // Getting product summary from ProductSummaryService.
            var productSummary = _productSummaryService.GetProductSummary();
            return productSummary;// Returns product-level information with the following fields:category,total_quantity,total_revenue
        }



    }
}
