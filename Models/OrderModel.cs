using System;

namespace OrderScreen.Models
{
    public class OrderModel
    {
	    public int product_id  {get; set;}
	    public string product_name  {get; set;}
	    public int count  {get; set;}
	    public double discount  {get; set;}
        public int warehouse_id  {get; set;}
        public string warehouse_name  {get; set;}
        public string order_price  {get; set;}
        public DateTime created_at  {get; set;}
    }
}