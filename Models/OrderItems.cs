namespace RestfulAPI.Models
{
    public class OrderItems
    {
        //Entity with basic information that matches the resources file
        public int order_id { get; set; }
        public int product_id { get; set; }
        public int quantity { get; set; }
    }
}
