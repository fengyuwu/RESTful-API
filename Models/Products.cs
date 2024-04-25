namespace RestfulAPI.Models
{
    public class Products
    {
        public int product_id { get; set; }
        public string? product_name { get; set; }
        public string? category { get; set; }
        public decimal price { get; set; }
    }

}
