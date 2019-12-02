using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrderScreen.Data
{
    public class Customer : Entity
    {
       /* [Key]
        public int customer_no {get; set;}*/
        public string customer_name {get; set;}
        public string customer_surname {get; set;}
    }
}