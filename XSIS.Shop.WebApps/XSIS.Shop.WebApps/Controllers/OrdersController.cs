using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using XSIS.Shop.Models;
using XSIS.Shop.Repository;
using XSIS.Shop.ViewModels;
using XSIS.Shop.WebApps.Models;

namespace XSIS.Shop.WebApps.Controllers
{
    public class OrdersController : Controller
    {

        OrderRepository service = new OrderRepository();
        private string ApiURL = WebConfigurationManager.AppSettings["XSIS.Shop.API"];
        private Shop.Models.ShopDBEntities db = new Shop.Models.ShopDBEntities();
        public List<OrderItemViewModel> ListItem = new List<OrderItemViewModel>();
        
        // GET: Orders
        [HttpGet]
        public ActionResult Index(string OrderNumber, string OrderDate, string CustomerId)

        {
            List<OrderViewModel> list = null;
            var result = list;
            if (string.IsNullOrEmpty(OrderNumber) && string.IsNullOrEmpty(OrderDate) && string.IsNullOrEmpty(CustomerId))
            {
                string ApiEndPoint = ApiURL + "api/OrderApi/";
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.GetAsync(ApiEndPoint).Result;

                string ListResult = response.Content.ReadAsStringAsync().Result.ToString();
                result = JsonConvert.DeserializeObject<List<OrderViewModel>>(ListResult);
                ViewBag.ChkDefValue = "true";
                ViewBag.CustomerId = new SelectList(service.GetAllCustomer(), "Id", "FullName");
                IEnumerable<SelectListItem> userTypeList = new SelectList(service.GetAllCustomer().Distinct().ToList(), "Id", "FullName");
                ViewData["userTypeList"] = userTypeList;
            }
            else if (!string.IsNullOrEmpty(OrderNumber) || !string.IsNullOrEmpty(OrderDate) || !string.IsNullOrEmpty(CustomerId))
            {
                if (string.IsNullOrEmpty(OrderNumber) || string.IsNullOrWhiteSpace(OrderNumber))
                {
                    OrderNumber = "";
                }
                if (string.IsNullOrEmpty(OrderDate) || string.IsNullOrWhiteSpace(OrderDate))
                {
                    OrderDate = "";
                }
                if (string.IsNullOrEmpty(CustomerId) || string.IsNullOrWhiteSpace(CustomerId))
                {
                    CustomerId = "";
                }
                if (!string.IsNullOrEmpty(OrderDate) && !string.IsNullOrWhiteSpace(OrderDate))
                {
                    string[] Parameter = OrderDate.Split('/');
                    string date1 = Parameter[0];
                    string date2 = Parameter[1];
                    string date3 = Parameter[2];
                    OrderDate = date1 + "-" + date2 + "-" + date3;
                }
              
               
                string ApiEndPoint = ApiURL + "api/OrderApi/SearchByKey/" + (OrderNumber + "|" + OrderDate + "|" + CustomerId);
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.GetAsync(ApiEndPoint).Result;

                string ListResult = response.Content.ReadAsStringAsync().Result.ToString();
                result = JsonConvert.DeserializeObject<List<OrderViewModel>>(ListResult);
               
                ViewBag.ChkDefValue = "true";
                ViewBag.CustomerId = new SelectList(service.GetAllCustomer(), "Id", "FullName");
                IEnumerable<SelectListItem> userTypeList = new SelectList(service.GetAllCustomer().Distinct().ToList(), "Id", "FullName");

            }
            else
            {
                string ApiEndPoint = ApiURL + "api/OrderApi/";
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.GetAsync(ApiEndPoint).Result;

                string ListResult = response.Content.ReadAsStringAsync().Result.ToString();
                result = JsonConvert.DeserializeObject<List<OrderViewModel>>(ListResult);
                ViewBag.ChkDefValue = "true";
                ViewBag.CustomerId = new SelectList(service.GetAllCustomer(), "Id", "FullName");
                IEnumerable<SelectListItem> userTypeList = new SelectList(service.GetAllCustomer().Distinct().ToList(), "Id", "FullName");
                ViewData["userTypeList"] = userTypeList;
            }
           

            return View(result);
            
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idx = id.HasValue ? id.Value : 0;
          
            //API Akses
            string apiEndPoint = ApiURL + "api/OrderApi/" + idx;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiEndPoint).Result;

            string result = response.Content.ReadAsStringAsync().Result.ToString();
            var order = JsonConvert.DeserializeObject<OrderViewModel>(result);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            if (Session["ListOrderItem"] != null)
            {
                ListItem = (List<OrderItemViewModel>)Session["ListOrderItem"];
            }

            Session["ListOrderItem"] = ListItem;
            ViewBag.GrandTotal = ListItem.Sum(s => s.TotalAmount);

            ViewBag.ChkDefValue = "true";
            ViewBag.CustomerId = new SelectList(service.GetAllCustomer(), "Id", "FullName");
            
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OrderDate,OrderNumber,CustomerId,TotalAmount")] OrderViewModel order)
        {
            if (ModelState.IsValid)
            {
                //db.Orders.Add(order);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ChkDefValue = "true";
            ViewBag.CustomerId = new SelectList(service.GetAllCustomer(), "Id", "FullName");
            return View(order);
        }

        public ActionResult OrderItem()
        {
            ViewBag.ProductId = new SelectList(service.GetAllProduct(), "Id", "ProductName");
            return PartialView();
        }
        public ActionResult AddCurrentItemToOrder(int ProductId, int OrderQuantity)
        {
            if (Session["ListOrderItem"] != null)
            {
                ListItem = (List<OrderItemViewModel>)Session["ListOrderItem"];
            }
            

            //API Akses
            string apiEndPoint = ApiURL + "api/ProductApi/" + ProductId;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiEndPoint).Result;

            string result = response.Content.ReadAsStringAsync().Result.ToString();
            var product = JsonConvert.DeserializeObject<ProductViewModel>(result);

            ListItem.Add(new OrderItemViewModel
            {
                ProductId = product.Id,
                ProductName = product.ProductName,
                UnitPrice = product.UnitPrice.HasValue ? product.UnitPrice.Value : 0,
                Quantity = OrderQuantity,
                TotalAmount = (product.UnitPrice.HasValue ? product.UnitPrice.Value : 0) * OrderQuantity
            });

            Session["ListOrderItem"] = ListItem;
            ViewBag.GrandTotal = ListItem.Sum(s => s.TotalAmount);

            return PartialView("_ListOrderItem", ListItem);
        }
        public ActionResult GetCurrentItemToOrder()
        {
            ListItem = (List<OrderItemViewModel>)Session["ListOrderItem"];
            return PartialView("_ListOrderItem", ListItem);
        }
        public ActionResult RemoveItemFromCurrentOrder(int ProductId)
        {
            if (Session["ListOrderItem"] != null)
            {
                ListItem = (List<OrderItemViewModel>)Session["ListOrderItem"];
            }

            var RemoveItemvarian = RemoveItem(ProductId, ListItem);
            Session["ListOrderItem"] = RemoveItemvarian;
            ViewBag.GrandTotal = RemoveItemvarian.Sum(s => s.TotalAmount);
            return PartialView("_ListOrderItem", RemoveItemvarian);
        }
        public List<OrderItemViewModel> RemoveItem(int ProductId, List<OrderItemViewModel> ListItem)
        {
            for (int i = 0; i < ListItem.Count; i++)
            {
                if (ListItem[i].ProductId == ProductId)
                {
                    ListItem.Remove(ListItem[i]);
                    break;
                }
            }
            return ListItem;
        }
        //public ActionResult Get()
        //{
        //    List<OrderItemViewModel> list = (List<OrderItemViewModel>)Session["ListOrderItem"];
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}
        //    // GET: Orders/Edit/5
        //    public ActionResult Edit(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        }
        //        OrderViewModel order = db.Orders.Find(id);
        //        if (order == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        ViewBag.CustomerId = new SelectList(db.Customers, "Id", "FirstName", order.CustomerId);
        //        return View(order);
        //    }

        //    // POST: Orders/Edit/5
        //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult Edit([Bind(Include = "Id,OrderDate,OrderNumber,CustomerId,TotalAmount")] OrderViewModel order)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            db.Entry(order).State = EntityState.Modified;
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //        ViewBag.CustomerId = new SelectList(db.Customers, "Id", "FirstName", order.CustomerId);
        //        return View(order);
        //    }

        //    // GET: Orders/Delete/5
        //    public ActionResult Delete(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        }
        //        OrderViewModel order = db.Orders.Find(id);
        //        if (order == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        return View(order);
        //    }

        //    // POST: Orders/Delete/5
        //    [HttpPost, ActionName("Delete")]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult DeleteConfirmed(int id)
        //    {
        //        OrderViewModel order = db.Orders.Find(id);
        //        db.Orders.Remove(order);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    protected override void Dispose(bool disposing)
        //    {
        //        if (disposing)
        //        {
        //            db.Dispose();
        //        }
        //        base.Dispose(disposing);
        //    }

    }
}
