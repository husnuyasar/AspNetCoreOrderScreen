using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OrderScreen.Data;
using OrderScreen.Database;
using OrderScreen.Models;

namespace OrderScreen.Controllers
{
    public class HomeController : Controller
    {
        private IRepository<Customer> repoCustomer;
        private IRepository<Product> repoProduct;
        private IRepository<Warehouse> repoWarehouse;
        private IRepository<Orders> repoOrders;
        private CoreDbContext dbContext;
        public HomeController(IRepository<Warehouse> repoWarehouse, IRepository<Product> repoProduct,
        IRepository<Customer> repoCustomer, IRepository<Orders> repoOrders, CoreDbContext dbContext)
        {
            this.repoCustomer = repoCustomer;
            this.repoProduct = repoProduct;
            this.repoWarehouse = repoWarehouse;
            this.repoOrders = repoOrders;
            this.dbContext = dbContext;

        }

        [Authorize]
        public IActionResult Index()
        {
            var name = User.Claims.Where(c => c.Type == ClaimTypes.Name)
               .Select(c => c.Value).SingleOrDefault();

            HttpContext.Session.Clear("orders");
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetCustomer([FromBody] int customerNo)
        {
            if (customerNo == 0)
            {
                return await Task.FromResult(Json(-1));
            }
            var customer = repoCustomer.GetById(customerNo);
            //var customer = repoCustomer.Where(x=> x.customer_no==customerNo).FirstOrDefault();

            if (customer == null)
            {
                return await Task.FromResult(Json(0));
            }

            return await Task.FromResult(Json(customer));
        }

        [HttpPost]
        public async Task<JsonResult> GetProducts()
        {
            var productList = repoProduct.All();
            return await Task.FromResult(Json(productList));
        }

        [HttpPost]
        public async Task<JsonResult> GetWareHouses()
        {
            var warehouseList = repoWarehouse.All();
            return await Task.FromResult(Json(warehouseList));
        }

        [HttpPost]
        public IActionResult AddProductToSession([FromForm] OrderModel order)
        {
            var session = HttpContext.Session.GetObjectFromJson<OrderSessionModel>("orders");
            order.created_at = DateTime.Now;
            if (session == null)
            {
                var orderList = new OrderSessionModel();
                orderList.Order.Add(order);
                // HttpContext.Session.SetObject("orders", order);
                HttpContext.Session.SetObjectAsJson("orders", orderList);
                session = HttpContext.Session.GetObjectFromJson<OrderSessionModel>("orders");
            }
            else
            {
                session.Order.Add(order);
                HttpContext.Session.SetObjectAsJson("orders", session);
            }
            var o =session.Order.OrderByDescending(x=>x.created_at).FirstOrDefault();
            return Json(o);
        }

        [HttpPost]
        public IActionResult Save([FromForm] CustomerOrderModel customerOrder)
        {

            if (customerOrder.customer_name == null || customerOrder.customer_surname == null
            || customerOrder.order_no == null || customerOrder.order_date.Year==1)
            {
                return Json("-1");
            }

            var customer = repoCustomer.Where(x =>
                x.customer_name == customerOrder.customer_name && x.customer_surname == customerOrder.customer_surname
            ).FirstOrDefault();
            var customer_id = 0;
            if (customer == null)
            {
                var newCustomer = new Customer()
                {
                    customer_name = customerOrder.customer_name.ToUpper(new CultureInfo("tr-TR", false)),
                    customer_surname = customerOrder.customer_surname.ToUpper(new CultureInfo("tr-TR", false)),
                    created_at = DateTime.Now
                };
                customer_id = repoCustomer.Add(newCustomer);
            }
            else
            {
                if (customer.id != customerOrder.customer_no)
                {
                    return Json("-3");
                }
            }
            var orderItemList = HttpContext.Session.GetObjectFromJson<OrderSessionModel>("orders");

            if (orderItemList == null)
            {
                return Json("-2");
            }

            foreach (var orderItem in orderItemList.Order)
            {
                var newOrder = new Orders()
                {
                    count = orderItem.count,
                    created_at = customerOrder.order_date,
                    customer_id = customerOrder.customer_no != 0 ? customerOrder.customer_no : customer_id,
                    discount = orderItem.discount,
                    order_no = customerOrder.order_no.ToUpper(new CultureInfo("tr-TR", false)),
                    order_price =!string.IsNullOrEmpty(orderItem.order_price) ? Convert.ToDecimal(orderItem.order_price.Replace(".",",")) : 0,
                    product_id = orderItem.product_id,
                    warehouse_id = orderItem.warehouse_id
                };

                repoOrders.Add(newOrder);
            }
            return Json("1");

        }
        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<JsonResult> SearchOrder([FromForm] FilterModel model)
        {
            try
            {
                var customername = !string.IsNullOrEmpty(model.customername) ? model.customername.ToUpper(new CultureInfo("tr-TR", false)) : null;
                var orderno = !string.IsNullOrEmpty(model.orderno) ? model.orderno.ToUpper(new CultureInfo("tr-TR", false)) : null;
                var query = (from o in dbContext.orders
                        join c in dbContext.customer
                        on o.customer_id equals c.id  
                        //where o.order_no.Contains(orderno) || c.customer_name.Contains(customername)
                        //|| c.customer_surname.Contains(customername) 
                        //|| (o.created_at>=model.startDate && o.created_at<=model.endDate)
                        group new {o,c} by new {o.order_no,c.customer_name,c.customer_surname,o.created_at} into g
                        select new CustomerSearchModel{
                            nameSurname = g.Key.customer_name+' '+g.Key.customer_surname,
                            orderNo = g.Key.order_no,
                            totalPrice = g.Sum(x=> x.o.order_price),
                            orderDate = g.Key.created_at
                        }
                ).Distinct().ToList();
                var list = new List<CustomerSearchModel>();
                if (!string.IsNullOrEmpty(customername))
                {
                    list = query.Where(x=> x.nameSurname.Contains(customername)).Distinct().ToList();
                }

                if (!string.IsNullOrEmpty(orderno))
                {
                    list = query.Where(x=> x.orderNo.Contains(orderno)).Distinct().ToList();
                }

                if (model.startDate.Year>1 && model.startDate.Year>1)
                {
                    list = query.Where(x=> 
                        x.orderDate>= model.startDate && x.orderDate<=model.endDate
                    ).Distinct().ToList();
                }

                if (!string.IsNullOrEmpty(customername) && model.startDate.Year>1 && model.startDate.Year>1)
                {
                    list = query.Where(x=> 
                        x.nameSurname.Contains(customername) &&
                        (x.orderDate>= model.startDate && x.orderDate<=model.endDate)
                    ).Distinct().ToList();
                }

                if (!string.IsNullOrEmpty(orderno) && model.startDate.Year>1 && model.startDate.Year>1)
                {
                    list = query.Where(x=> 
                        x.orderNo.Contains(orderno) &&
                        (x.orderDate>= model.startDate && x.orderDate<=model.endDate)
                    ).Distinct().ToList();
                }

                if (!string.IsNullOrEmpty(customername) && !string.IsNullOrEmpty(orderno))
                {
                    list = query.Where(x=> x.orderNo.Contains(orderno) && x.nameSurname.Contains(customername)).Distinct().ToList();
                    
                }

                if (!string.IsNullOrEmpty(customername) && !string.IsNullOrEmpty(orderno) && model.startDate.Year>1 && model.startDate.Year>1)
                {
                    list = query.Where(x=> 
                        x.orderNo.Contains(orderno) && x.nameSurname.Contains(customername)
                        && (x.orderDate>= model.startDate && x.orderDate<=model.endDate)
                    ).Distinct().ToList();
                }

                if (list == null)
                {
                    return await Task.FromResult(Json("-1"));
                }
                return await Task.FromResult(Json(list));
            }
            catch (System.Exception ex)
            {
                return await Task.FromResult(Json("-1"));
            }

            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
