using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using XSIS.Shop.Repository;
using XSIS.Shop.ViewModels;
using System.Web.Configuration;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace XSIS.Shop.WebApps.Controllers
{
    public class CustomersController : Controller
    {
        private string ApiURL = WebConfigurationManager.AppSettings["XSIS.Shop.API"];
        private CustomerRepository service = new CustomerRepository();
        // GET: Customers
        [HttpGet]
        public ActionResult Index(string FullName, string CityCountry, string Email)
        {
            List<CustomerViewModel> list = null;
            var result = list;
            if (string.IsNullOrEmpty(FullName) && string.IsNullOrEmpty(CityCountry) && string.IsNullOrEmpty(Email))
            {
                string ApiEndPoint = ApiURL + "api/CustomerApi/";
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.GetAsync(ApiEndPoint).Result;

                string ListResult = response.Content.ReadAsStringAsync().Result.ToString();
                result = JsonConvert.DeserializeObject<List<CustomerViewModel>>(ListResult);
            }
            else if (!string.IsNullOrEmpty(FullName) || !string.IsNullOrEmpty(CityCountry) || !string.IsNullOrEmpty(Email))
            {
                if (string.IsNullOrEmpty(FullName)||string.IsNullOrWhiteSpace(FullName))
                {
                    FullName = "";
                }
                if (string.IsNullOrEmpty(CityCountry) || string.IsNullOrWhiteSpace(CityCountry))
                {
                    CityCountry = "";
                }
                if (string.IsNullOrEmpty(Email) || string.IsNullOrWhiteSpace(Email))
                {
                    Email = "";
                }
                string ApiEndPoint = ApiURL + "api/CustomerApi/SearchByKey/" + (FullName + "|" + CityCountry + "|" + Email);
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.GetAsync(ApiEndPoint).Result;

                string ListResult = response.Content.ReadAsStringAsync().Result.ToString();
                result = JsonConvert.DeserializeObject<List<CustomerViewModel>>(ListResult);
            }
            else
            {
                string ApiEndPoint = ApiURL + "api/CustomerApi/";
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.GetAsync(ApiEndPoint).Result;

                string ListResult = response.Content.ReadAsStringAsync().Result.ToString();
                result = JsonConvert.DeserializeObject<List<CustomerViewModel>>(ListResult);
            }

            return View(result.ToList());
        }
        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {

            

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idx = id.HasValue ? id.Value : 0;
           

            //API Akses
            string apiEndPoint = ApiURL + "api/CustomerApi/" + idx;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiEndPoint).Result;

            string result = response.Content.ReadAsStringAsync().Result.ToString();
            var custVM = JsonConvert.DeserializeObject<CustomerViewModel>(result);

           
            if (custVM == null)
            {
                return HttpNotFound();
            }
         
            return View(custVM);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,City,Country,Phone,Email")] CustomerViewModel customer)
        {
            bool email = false;
            if (ModelState.IsValid)
            {

                bool fullName = service.SearchFullName((customer.FirstName + " " + customer.LastName));
                if (!string.IsNullOrEmpty(customer.Email)|| !string.IsNullOrWhiteSpace(customer.Email))
                {
                    email = service.SearchEmail(customer.Email);
                    if (email)
                    {
                        ModelState.AddModelError("", "Email Ini sudah terpakai");
                    }
                }
                if (fullName)
                {
                    ModelState.AddModelError("", "Nama lengkap ( nama depan + nama belakang) sudah terpakai");
                }
                else
                {

                    //create API Access
                    string json = JsonConvert.SerializeObject(customer);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    string apiEndPoint = ApiURL + "api/CustomerApi/";
                    HttpClient client = new HttpClient();
                    HttpResponseMessage response = client.PostAsync(apiEndPoint,byteContent).Result;

                    string result = response.Content.ReadAsStringAsync().Result.ToString();
                    int success = int.Parse(result);
                    if (success == 1)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(customer);
                    }
                  
                }
               
            }
            return View(customer);

        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idx = id.HasValue ? id.Value : 0;


            //API Akses
            string apiEndPoint = ApiURL + "api/CustomerApi/" + idx;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiEndPoint).Result;

            string result = response.Content.ReadAsStringAsync().Result.ToString();
            var custVM = JsonConvert.DeserializeObject<CustomerViewModel>(result);


            if (custVM == null)
            {
                return HttpNotFound();
            }

            return View(custVM);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,City,Country,Phone,Email")] CustomerViewModel customer)
        {
            if (ModelState.IsValid)
            {
                //Update API Access
                string json = JsonConvert.SerializeObject(customer);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string apiEndPoint = ApiURL + "api/CustomerApi/";
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
                    return View(customer);
                }
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idx = id.HasValue ? id.Value : 0;


            //API Akses
            string apiEndPoint = ApiURL + "api/CustomerApi/" + idx;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiEndPoint).Result;

            string result = response.Content.ReadAsStringAsync().Result.ToString();
            var custVM = JsonConvert.DeserializeObject<CustomerViewModel>(result);


            if (custVM == null)
            {
                return HttpNotFound();
            }

            return View(custVM);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string apiEndPoint = ApiURL + "api/CustomerApi/" + id;
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
