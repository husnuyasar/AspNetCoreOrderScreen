using System;
using System.ComponentModel.DataAnnotations;

namespace OrderScreen.Data
{
    public class Entity
    {
        [Key]
        public int id { get; set; }
        public DateTime created_at { get; set; }
    }
}