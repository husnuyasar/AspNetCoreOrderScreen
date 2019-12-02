using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OrderScreen.Data;
using OrderScreen.Database;
using OrderScreen.Models;

namespace OrderScreen.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRepository<Account> repoAccount;

        public AccountController(IRepository<Account> repoAccount)
        {
            this.repoAccount = repoAccount;
        }
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (LoginUser(loginModel))
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginModel.Username)
            };
 
                var userIdentity = new ClaimsIdentity(claims, "login");
 
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);
 
                //Just redirect to our index after logging in. 
                return RedirectToAction("Index","Home");
            }
            return View();
        }

        public bool LoginUser(LoginModel loginModel)
        {
            var userControl = repoAccount.Any(x=> 
                x.username == loginModel.Username && x.password == loginModel.Password
            );
            /*var userControl = dbContext.account.Any(x=>
                x.username == loginModel.Username && x.password == loginModel.Password
            );*/
            if (userControl)
            {
                return true;
            }

            return false;
        }
    }
}