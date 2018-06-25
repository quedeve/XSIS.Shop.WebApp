using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using XSIS.Shop.Repository;
using XSIS.Shop.ViewModels;

namespace XSIS.Shop.WebApps.Controllers
{
    public class ProductsController : Controller
    {
        //private ProductRepository service = new ProductRepository();

        private string ApiURL = WebConfigurationManager.AppSettings["XSIS.Shop.API"];
        private ProductRepository service = new ProductRepository();
        // GET: Products
        [HttpGet]
        public ActionResult Index()
        {
            List<ProductViewModel> list = null;
            var result = list;
            string ApiEndPoint = ApiURL + "api/ProductApi/";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(ApiEndPoint).Result;

            string ListResult = response.Content.ReadAsStringAsync().Result.ToString();
            result = JsonConvert.DeserializeObject<List<ProductViewModel>>(ListResult);
            return View(result);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idx = id.HasValue ? id.Value : 0;


            //API Akses
            string apiEndPoint = ApiURL + "api/ProductApi/" + idx;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiEndPoint).Result;

            string result = response.Content.ReadAsStringAsync().Result.ToString();
            var product = JsonConvert.DeserializeObject<ProductViewModel>(result);


            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.ChkDefValue = "true";
            ViewBag.SupplierId = new SelectList(service.GetAllSupplier(), "Id", "FirstName");
            IEnumerable<SelectListItem> userTypeList = new SelectList(service.GetAllSupplier().Distinct().ToList(), "Id", "FirstName");
            ViewData["userTypeList"] = userTypeList;
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel product)
        {

            if (ModelState.IsValid)
            {
                string json = JsonConvert.SerializeObject(product);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string apiEndPoint = ApiURL + "api/ProductApi/";
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.PostAsync(apiEndPoint, byteContent).Result;

                string result = response.Content.ReadAsStringAsync().Result.ToString();
                int success = int.Parse(result);
                if (success == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(product);
                }
            }

            ViewBag.SupplierId = new SelectList(service.GetAllSupplier(), "Id", "CompanyName", product.SupplierId);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idx = id.HasValue ? id.Value : 0;


            //API Akses
            string apiEndPoint = ApiURL + "api/ProductApi/" + idx;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiEndPoint).Result;
            string result = response.Content.ReadAsStringAsync().Result.ToString();
            var product = JsonConvert.DeserializeObject<ProductViewModel>(result);

            if (product == null)
            {
                return HttpNotFound();
            }
           
            ViewBag.SupplierId = new SelectList(service.GetAllSupplier(), "Id", "CompanyName", product.SupplierId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductViewModel product)
        {
            ViewBag.SupplierId = new SelectList(service.GetAllSupplier(), "Id", "CompanyName", product.SupplierId);
            //if (ModelState.IsValid)
            //{
            //    service.UpdateProduct(product);
            //    return RedirectToAction("Index");
            //}
            if (ModelState.IsValid)
            {
                //Update API Access
                string json = JsonConvert.SerializeObject(product);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string apiEndPoint = ApiURL + "api/ProductApi/";
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.PutAsync(apiEndPoint, byteContent).Result;

                string result = response.Content.ReadAsStringAsync().Result.ToString();
                int success = int.Parse(result);
                if (success == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(product);
                }
            }
            
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idx = id.HasValue ? id.Value : 0;


            //API Akses
            string apiEndPoint = ApiURL + "api/ProductApi/" + idx;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiEndPoint).Result;
            string result = response.Content.ReadAsStringAsync().Result.ToString();
            var product = JsonConvert.DeserializeObject<ProductViewModel>(result);

            if (product == null)
            {
                return HttpNotFound();
            }

           
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string apiEndPoint = ApiURL + "api/ProductApi/" + id;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.DeleteAsync(apiEndPoint).Result;

            string result = response.Content.ReadAsStringAsync().Result.ToString();

            int success = int.Parse(result);
            if (success == 1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }

        }


    }
}
