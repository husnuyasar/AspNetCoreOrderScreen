using System;
using System.Collections.Generic;

namespace OrderScreen.Models
{
    public class CustomerSearchModel
    {
       public string nameSurname { get; set; }
       public string orderNo { get; set; }
       public decimal totalPrice { get; set; }
       public DateTime orderDate { get; set; }
    }
}
