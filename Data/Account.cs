using System.ComponentModel.DataAnnotations;

namespace OrderScreen.Data
{
    public class Account : Entity
    {
        public string username {get; set;}
        public string password {get; set;}
    }
}