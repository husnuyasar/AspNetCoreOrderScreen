using System;

namespace OrderScreen.Models
{
    public class CustomerOrderModel
    {
        public int customer_no {get; set;}
        public string customer_name {get; set;}
        public string customer_surname {get; set;}
        public string order_no {get; set;}
        public DateTime order_date {get; set;}
    }
}