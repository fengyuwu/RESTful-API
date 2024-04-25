namespace RestfulAPI.Models
{
    public class Customers
    {
        //customer entity with basic information


        //Getter and Setter for the fields
        public int? customer_id { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? email { get; set; }
        public string? country { get; set; }
    }

}
