namespace OrderScreen.Data
{
    public class Orders : Entity
    {
        public int product_id { get; set; }
        public int customer_id { get; set; }
        public string order_no { get; set; }
        public int count { get; set; }
        public double discount { get; set; }
        public int warehouse_id { get; set; }
        public decimal order_price { get; set; }
    }
}