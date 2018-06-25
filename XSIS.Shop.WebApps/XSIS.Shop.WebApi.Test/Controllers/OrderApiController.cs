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
    public class OrderApiController : ApiController
    {
        private OrderRepository service = new OrderRepository();
        [HttpGet]
        public List<OrderViewModel> Get()
        {
            var result = service.GetAllOrder();
            return result;
        }

        public OrderViewModel Get(int id)
        {
            var result = service.GetOrderById(id);
            return result;
        }
        [HttpGet]
        public List<OrderViewModel> SearchByKey(string id)
        {
            string[] Parameters = id.Split('|');

            string fullName = Parameters[0];
            string cityCountry = Parameters[1];
            string email = Parameters[2];

            string[] date = cityCountry.Split('-');
            if (!string.IsNullOrEmpty(cityCountry) && !string.IsNullOrWhiteSpace(cityCountry))
            {
                cityCountry = date[1] + '/' + date[0] + '/' + date[2];
            }
               

            var result = service.SearchByKey(fullName, cityCountry, email);
            return result;
        }
    }
}
