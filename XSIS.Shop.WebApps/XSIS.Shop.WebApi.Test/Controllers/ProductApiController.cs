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
    public class ProductApiController : ApiController
    {
        private ProductRepository service = new ProductRepository();

        [HttpGet]
        public List<ProductViewModel> Get()
        {
            var result = service.GetAllProduct();
            return result;
        }

        public ProductViewModel Get(int id)
        {
            var result = service.GetProductById(id);
            return result;
        }
        [HttpPost]
        public int Post(ProductViewModel product)
        {
            try
            {
                service.AddNewProduct(product);
                return 1;
            }
            catch (Exception)
            {
                return 0;

            }
        }
        [HttpPut]
        public int Put(ProductViewModel product)
        {
            try
            {
                service.UpdateProduct(product);
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
                service.DeleteProduct(id);
                return 1;
            }
            catch (Exception)
            {

                return 0;
            }
        }


    }
}
