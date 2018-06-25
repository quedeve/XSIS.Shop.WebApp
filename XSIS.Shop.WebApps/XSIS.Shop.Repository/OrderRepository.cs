using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSIS.Shop.Models;
using XSIS.Shop.ViewModels;

namespace XSIS.Shop.Repository
{
    public class OrderRepository
    {
        public List<OrderViewModel> GetAllOrder()
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                

                List<OrderViewModel> listOrder = new List<OrderViewModel>();

                var model = (from o in db.Orders
                             join c in db.Customers on o.CustomerId equals c.Id
                             select new OrderViewModel
                             {
                                 CustomerId = o.CustomerId,
                                 CustomerName = c.FirstName + " " + c.LastName,
                                 Id = o.Id,
                                 OrderDate = o.OrderDate,
                                 OrderNumber = o.OrderNumber,
                                 TotalAmount = o.TotalAmount
                             }

                             ).ToList();
                return model;
                //foreach (var item in)
                //{
                //    OrderViewModel orderView = new OrderViewModel();
                //    orderView.Id = item.Id;
                //    orderView.OrderDate = item.OrderDate;
                //    orderView.OrderNumber = item.OrderNumber;
                //    orderView.CustomerId = item.CustomerId;
                //    orderView.TotalAmount = item.TotalAmount;

                //    listOrder.Add(orderView);

                //}
                //return listOrder;
            }
        }
        public OrderViewModel GetOrderById(int id)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Order order = db.Orders.Find(id);
                var customerName = db.Customers.Find(order.CustomerId);
                var orderItem = db.OrderItems.Where(o => o.OrderId.Equals(id)).ToList();
         

                OrderViewModel orderView = new OrderViewModel();
                orderView.Id = order.Id;
                orderView.OrderNumber = order.OrderNumber;
                orderView.OrderDate = order.OrderDate;
                orderView.CustomerId = order.CustomerId;
                orderView.TotalAmount = order.TotalAmount;
                orderView.CustomerName = customerName.FirstName+" "+customerName.LastName;
                List<OrderItemViewModel> listOrderItem = new List<OrderItemViewModel>();
                foreach (var item in orderItem)
                {
                    OrderItemViewModel orderItemViewModel = new OrderItemViewModel();
                    orderItemViewModel.Id = item.Id;
                    var productName = db.Products.Find(item.ProductId);
                    orderItemViewModel.OrderId = item.OrderId;
                    orderItemViewModel.ProductId = item.ProductId;
                    orderItemViewModel.Quantity = item.Quantity;
                    orderItemViewModel.UnitPrice = item.UnitPrice;
                    orderItemViewModel.ProductName = productName.ProductName;

                    listOrderItem.Add(orderItemViewModel);
                }
                orderView.Detail = listOrderItem;
                return orderView;

            }
        }
        public List<CustomerViewModel> GetAllCustomer()
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                List<CustomerViewModel> listCustomer = new List<CustomerViewModel>();
                foreach (var item in db.Customers.ToList())
                {
                    CustomerViewModel cvm = new CustomerViewModel();
                    cvm.Id = item.Id;
                    cvm.FirstName = item.FirstName;
                    cvm.LastName = item.LastName;
                    cvm.City = item.City;
                    cvm.Country = item.Country;
                    cvm.Phone = item.Phone;
                    cvm.Email = item.Email;
                    cvm.FullName = item.FirstName + " " + item.LastName;

                    listCustomer.Add(cvm);
                }
                return listCustomer;
            }

        }
        public List<OrderViewModel> SearchByKey(string OrderNumber, string OrderDate, string CustomerId)
        {
            //if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrEmpty(Email))
            //{
            //    Email = "";
            //}
            DateTime date = System.DateTime.Now;
           
            var model = new List<OrderViewModel>();
            if (!string.IsNullOrEmpty(OrderDate) || !string.IsNullOrWhiteSpace(OrderDate))
            {
                 date = DateTime.Parse(OrderDate);
            }
            
            using (ShopDBEntities db = new ShopDBEntities())
            {
                List<CustomerViewModel> listCustomer = new List<CustomerViewModel>();

                if (string.IsNullOrEmpty(OrderNumber) && !string.IsNullOrEmpty(OrderDate) && string.IsNullOrEmpty(CustomerId))
                {

                     model = (from o in db.Orders
                                 join c in db.Customers on o.CustomerId equals c.Id
                                 where o.OrderDate == date
                                 select new OrderViewModel
                                 {
                                     CustomerId = o.CustomerId,
                                     CustomerName = c.FirstName + " " + c.LastName,
                                     Id = o.Id,
                                     OrderDate = o.OrderDate,
                                     OrderNumber = o.OrderNumber,
                                     TotalAmount = o.TotalAmount
                                 }

                             ).ToList();
                    
                }
                if (string.IsNullOrEmpty(OrderNumber) && string.IsNullOrEmpty(OrderDate) && !string.IsNullOrEmpty(CustomerId))
                {
                    int idCustomer = int.Parse(CustomerId);
                    model = (from o in db.Orders
                             join c in db.Customers on o.CustomerId equals c.Id
                             where o.CustomerId == idCustomer
                             select new OrderViewModel
                             {
                                 CustomerId = o.CustomerId,
                                 CustomerName = c.FirstName + " " + c.LastName,
                                 Id = o.Id,
                                 OrderDate = o.OrderDate,
                                 OrderNumber = o.OrderNumber,
                                 TotalAmount = o.TotalAmount
                             }

                             ).ToList();
                }
                if (!string.IsNullOrEmpty(OrderNumber) && !string.IsNullOrEmpty(OrderDate) && !string.IsNullOrEmpty(CustomerId))
                {
                    int idCustomer = int.Parse(CustomerId);
                    
                    model = (from o in db.Orders
                             join c in db.Customers on o.CustomerId equals c.Id
                             where o.OrderDate == date && o.CustomerId == idCustomer && o.OrderNumber == OrderNumber
                             select new OrderViewModel
                             {
                                 CustomerId = o.CustomerId,
                                 CustomerName = c.FirstName + " " + c.LastName,
                                 Id = o.Id,
                                 OrderDate = o.OrderDate,
                                 OrderNumber = o.OrderNumber,
                                 TotalAmount = o.TotalAmount
                             }

                             ).ToList();
                }
                if (!string.IsNullOrEmpty(OrderNumber) && !string.IsNullOrEmpty(OrderDate) && string.IsNullOrEmpty(CustomerId))
                {
                    model = (from o in db.Orders
                             join c in db.Customers on o.CustomerId equals c.Id
                             where o.OrderDate == date && o.OrderNumber == OrderNumber
                             select new OrderViewModel
                             {
                                 CustomerId = o.CustomerId,
                                 CustomerName = c.FirstName + " " + c.LastName,
                                 Id = o.Id,
                                 OrderDate = o.OrderDate,
                                 OrderNumber = o.OrderNumber,
                                 TotalAmount = o.TotalAmount
                             }

                           ).ToList();
                }
                if (!string.IsNullOrEmpty(OrderNumber) && string.IsNullOrEmpty(OrderDate) && !string.IsNullOrEmpty(CustomerId))
                {
                    int idCustomer = int.Parse(CustomerId);
                  
                    model = (from o in db.Orders
                             join c in db.Customers on o.CustomerId equals c.Id
                             where o.CustomerId == idCustomer && o.OrderNumber == OrderNumber
                             select new OrderViewModel
                             {
                                 CustomerId = o.CustomerId,
                                 CustomerName = c.FirstName + " " + c.LastName,
                                 Id = o.Id,
                                 OrderDate = o.OrderDate,
                                 OrderNumber = o.OrderNumber,
                                 TotalAmount = o.TotalAmount
                             }

                           ).ToList();
                }
                if (string.IsNullOrEmpty(OrderNumber) && !string.IsNullOrEmpty(OrderDate) && !string.IsNullOrEmpty(CustomerId))
                {
                    int idCustomer = int.Parse(CustomerId);
                    model = (from o in db.Orders
                             join c in db.Customers on o.CustomerId equals c.Id
                             where o.OrderDate == date && o.CustomerId == idCustomer
                             select new OrderViewModel
                             {
                                 CustomerId = o.CustomerId,
                                 CustomerName = c.FirstName + " " + c.LastName,
                                 Id = o.Id,
                                 OrderDate = o.OrderDate,
                                 OrderNumber = o.OrderNumber,
                                 TotalAmount = o.TotalAmount
                             }

                           ).ToList();
                }
                if (!string.IsNullOrEmpty(OrderNumber) && string.IsNullOrEmpty(OrderDate) && string.IsNullOrEmpty(CustomerId))
                {

                    model = (from o in db.Orders
                             join c in db.Customers on o.CustomerId equals c.Id
                             where o.OrderNumber == OrderNumber
                             select new OrderViewModel
                             {
                                 CustomerId = o.CustomerId,
                                 CustomerName = c.FirstName + " " + c.LastName,
                                 Id = o.Id,
                                 OrderDate = o.OrderDate,
                                 OrderNumber = o.OrderNumber,
                                 TotalAmount = o.TotalAmount
                             }

                           ).ToList();
                }
                return model;
            }
        }

        public List<ProductViewModel> GetAllProduct()
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                List<ProductViewModel> listProduct = new List<ProductViewModel>();
               
                foreach (var item in db.Products)
                {
                    ProductViewModel productView = new ProductViewModel();
                    productView.Id = item.Id;
                    productView.ProductName = item.ProductName;

                    listProduct.Add(productView);
                }
                return listProduct;
            }
        }

    }
}
