using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XSIS.Shop.Repository;
using XSIS.Shop.ViewModels;

namespace XSIS.Shop.WebApi.Test.Controllers
{
    public class CustomerApiController : ApiController
    {
        private CustomerRepository service = new CustomerRepository();
        [HttpGet]
        public List<CustomerViewModel> Get()
        {
            var result = service.GetAllCustomer();

            return result;
        }

        public CustomerViewModel Get(int id)
        {
            var result = service.GetCustomerById(id);
            return result;
        }

        [HttpPost]
        public int Post(CustomerViewModel customer)
        {
            try
            {
                service.AddNewCustomer(customer);
                return 1;
            }
            catch (Exception)
            {
                return 0;
                
            }
        }
        [HttpPut]
        public int Put(CustomerViewModel customer)
        {
            try
            {
                service.UpdateCUstomer(customer);
                return 1;
            }
            catch(Exception )
            {
                return 0;
            }
        }
        [HttpDelete]
        public int Delete(int id)
        {
            try
            {
                service.DeleteCustomer(id);
                return 1;
            }
            catch (Exception)
            {

                return 0; 
            }
        }

        [HttpGet]
        public bool CekNamaExisting(string firstName, string lastName)
        {
            var result = service.SearchFullName(firstName+" "+lastName);
            return result;
        }

        [HttpGet]
        public bool CekEmailExisting(string id)
        {
            var result = service.SearchEmail(id);
            return result;
        }
        [HttpGet]
        public List<CustomerViewModel> SearchByKey(string id)
        {
            string[] Parameters = id.Split('|');

            string fullName = Parameters[0];
            string cityCountry = Parameters[1];
            string email = Parameters[2];

            var result = service.SearchByKey(fullName, cityCountry, email);
            return result;
        }

    }
}
