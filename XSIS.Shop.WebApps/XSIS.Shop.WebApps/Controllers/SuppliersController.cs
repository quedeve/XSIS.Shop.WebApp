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
//using XSIS.Shop.WebApps.Models;
//using XSIS.Shop.WebApps.ViewModels;

namespace XSIS.Shop.WebApps.Controllers
{
    public class SuppliersController : Controller
    {
        //private ShopDBEntities db = new ShopDBEntities();
        private string ApiURL = WebConfigurationManager.AppSettings["XSIS.Shop.API"];
        private SupplierRepository service = new SupplierRepository();



        // GET: Suppliers
        [HttpGet]
        public ActionResult Index()
        {
            List<SupplierVIewModel> list = null;
            var result = list;
            string ApiEndPoint = ApiURL + "api/SupplierApi/";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(ApiEndPoint).Result;

            string ListResult = response.Content.ReadAsStringAsync().Result.ToString();
            result = JsonConvert.DeserializeObject<List<SupplierVIewModel>>(ListResult);
            return View(service.GetAllSupplier().ToList());
        }

        // GET: Suppliers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idx = id.HasValue ? id.Value : 0;
            //API Akses
            string apiEndPoint = ApiURL + "api/SupplierApi/" + idx;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiEndPoint).Result;

            string result = response.Content.ReadAsStringAsync().Result.ToString();
            var supplierView = JsonConvert.DeserializeObject<SupplierVIewModel>(result);

            if (supplierView == null)
            {
                return HttpNotFound();
            }
           
            
            return View(supplierView);
        }

        // GET: Suppliers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SupplierVIewModel supplier)
        {

            if (ModelState.IsValid)
            {
                string json = JsonConvert.SerializeObject(supplier);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string apiEndPoint = ApiURL + "api/SupplierApi/";
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
                    return View(supplier);
                }
            }

            return View(supplier);
        }

        // GET: Suppliers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idx = id.HasValue ? id.Value : 0;
            SupplierVIewModel supplierView = service.GetSupplierById(idx);
            if (supplierView == null)
            {
                return HttpNotFound();
            }
            
            return View(supplierView);
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SupplierVIewModel supplier)
        {
            if (ModelState.IsValid)
            {
                service.UpdateSupplier(supplier);
                return RedirectToAction("Index");
            }
            return View(supplier);
        }

        // GET: Suppliers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idx = id.HasValue ? id.Value : 0;
            SupplierVIewModel supplierView = service.GetSupplierById(idx);
            if (supplierView == null)
            {
                return HttpNotFound();
            }
            return View(supplierView);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            service.DeleteSupplier(id);
            return RedirectToAction("Index");
        }

       
    }
}
