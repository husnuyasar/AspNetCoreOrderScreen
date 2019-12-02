using System.Collections.Generic;

namespace OrderScreen.Models
{
    public class OrderSessionModel
    {
        public List<OrderModel> Order { get; set; }=new List<OrderModel>();
    }    
}
