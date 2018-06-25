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
    public class SupplierApiController : ApiController
    {
        private SupplierRepository service = new SupplierRepository();

        [HttpGet]
        public List<SupplierVIewModel> Get()
        {
            var result = service.GetAllSupplier();
            return result;
        }
        public SupplierVIewModel Get(int id)
        {
            var result = service.GetSupplierById(id);
            return result;
        }
        [HttpPost]
        public int Post(SupplierVIewModel supplier)
        {
            try
            {
                service.AddNewSupplier(supplier);
                return 1;
            }
            catch (Exception)
            {
                return 0;

            }
        }
        [HttpPut]
        public int Put(SupplierVIewModel supplier)
        {
            try
            {
                service.UpdateSupplier(supplier);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        [HttpDelete]
        public int Delete(int id)
        {
            try
            {
                service.DeleteSupplier(id);
                return 1;
            }
            catch (Exception)
            {

                return 0;
            }
        }
    }
}
