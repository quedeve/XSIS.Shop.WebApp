using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XSIS.Shop.Models;
using XSIS.Shop.ViewModels;

namespace XSIS.Shop.Repository
{
    public class CustomerRepository
    {
        //Select * From CUstomer
      public List<CustomerViewModel> GetAllCustomer()
        {
            using (ShopDBEntities db = new ShopDBEntities()) {
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


                    listCustomer.Add(cvm);
                }
                return listCustomer;
            }
           
        }
        public CustomerViewModel GetCustomerById(int id)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Customer customer = db.Customers.Find(id);
                CustomerViewModel model = new CustomerViewModel();
                model.Id = customer.Id;
                model.FirstName = customer.FirstName;
                model.LastName = customer.LastName;
                model.City = customer.City;
                model.Country = customer.Country;
                model.Phone = customer.Phone;
                model.Email = customer.Email;

                return model;

            }
        }

        public void AddNewCustomer(CustomerViewModel customer)
        {
            
            using(ShopDBEntities db = new ShopDBEntities())
            {
                Customer model = new Customer();
                model.FirstName = customer.FirstName;
                model.LastName = customer.LastName;
                model.City = customer.City;
                model.Country = customer.Country;
                model.Phone = customer.Phone;
                model.Email = customer.Email;
                db.Customers.Add(model);
                db.SaveChanges();
            }
            
        }

        public void UpdateCUstomer(CustomerViewModel customer)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Customer model = new Customer();
                model.Id = customer.Id;
                model.FirstName = customer.FirstName;
                model.LastName = customer.LastName;
                model.City = customer.City;
                model.Country = customer.Country;
                model.Phone = customer.Phone;
                model.Email = customer.Email;
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void DeleteCustomer(int id)
        {
            using(ShopDBEntities db = new ShopDBEntities())
            {
                Customer customer = db.Customers.Find(id);
                db.Customers.Remove(customer);
                db.SaveChanges();
            }
        }

        public List<CustomerViewModel> SearchByKey(string FullName, string CityCountry, string Email)
        {
            //if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrEmpty(Email))
            //{
            //    Email = "";
            //}
           
            using(ShopDBEntities db = new ShopDBEntities())
            {
                List<CustomerViewModel> listCustomer = new List<CustomerViewModel>();
                
                if (string.IsNullOrEmpty(FullName) && !string.IsNullOrEmpty(CityCountry) && string.IsNullOrEmpty(Email))
                {
                    foreach (var item in db.Customers.Where(c => c.City.ToLower().Contains(CityCountry.ToLower()) || c.Country.ToLower().Contains(CityCountry.ToLower())))
                    {
                        CustomerViewModel cvm = new CustomerViewModel();
                        cvm.Id = item.Id;
                        cvm.FirstName = item.FirstName;
                        cvm.LastName = item.LastName;
                        cvm.City = item.City;
                        cvm.Country = item.Country;
                        cvm.Phone = item.Phone;
                        cvm.Email = item.Email;


                        listCustomer.Add(cvm);
                    }
                }
                if (string.IsNullOrEmpty(FullName) && string.IsNullOrEmpty(CityCountry) && !string.IsNullOrEmpty(Email))
                {
                    foreach (var item in db.Customers.Where(c => c.Email.ToLower().Contains(Email.ToLower())))
                    {
                        CustomerViewModel cvm = new CustomerViewModel();
                        cvm.Id = item.Id;
                        cvm.FirstName = item.FirstName;
                        cvm.LastName = item.LastName;
                        cvm.City = item.City;
                        cvm.Country = item.Country;
                        cvm.Phone = item.Phone;
                        cvm.Email = item.Email;


                        listCustomer.Add(cvm);
                    }
                }
                if (!string.IsNullOrEmpty(FullName) && !string.IsNullOrEmpty(CityCountry) && !string.IsNullOrEmpty(Email))
                {
                    foreach (var item in db.Customers.Where(c => (c.FirstName + " " + c.LastName).ToLower().Contains(FullName.ToLower()) && (c.City.ToLower().Contains(CityCountry.ToLower()) || c.Country.ToLower().Contains(CityCountry.ToLower())) && c.Email.ToLower().Contains(Email.ToLower())))
                    {
                        CustomerViewModel cvm = new CustomerViewModel();
                        cvm.Id = item.Id;
                        cvm.FirstName = item.FirstName;
                        cvm.LastName = item.LastName;
                        cvm.City = item.City;
                        cvm.Country = item.Country;
                        cvm.Phone = item.Phone;
                        cvm.Email = item.Email;


                        listCustomer.Add(cvm);
                    }
                }
                if (!string.IsNullOrEmpty(FullName) && !string.IsNullOrEmpty(CityCountry) && string.IsNullOrEmpty(Email))
                {
                    foreach (var item in db.Customers.Where(c => (c.FirstName + " " + c.LastName).ToLower().Contains(FullName.ToLower()) && (c.City.ToLower().Contains(CityCountry.ToLower()) || c.Country.ToLower().Contains(CityCountry.ToLower())) ))
                    {
                        CustomerViewModel cvm = new CustomerViewModel();
                        cvm.Id = item.Id;
                        cvm.FirstName = item.FirstName;
                        cvm.LastName = item.LastName;
                        cvm.City = item.City;
                        cvm.Country = item.Country;
                        cvm.Phone = item.Phone;
                        cvm.Email = item.Email;


                        listCustomer.Add(cvm);
                    }
                }
                if (!string.IsNullOrEmpty(FullName) && string.IsNullOrEmpty(CityCountry) && !string.IsNullOrEmpty(Email))
                {
                    foreach (var item in db.Customers.Where(c => (c.FirstName + " " + c.LastName).ToLower().Contains(FullName.ToLower()) && c.Email.ToLower().Contains(Email.ToLower())))
                    {
                        CustomerViewModel cvm = new CustomerViewModel();
                        cvm.Id = item.Id;
                        cvm.FirstName = item.FirstName;
                        cvm.LastName = item.LastName;
                        cvm.City = item.City;
                        cvm.Country = item.Country;
                        cvm.Phone = item.Phone;
                        cvm.Email = item.Email;


                        listCustomer.Add(cvm);
                    }
                }
                if (string.IsNullOrEmpty(FullName) && !string.IsNullOrEmpty(CityCountry) && !string.IsNullOrEmpty(Email))
                {
                    foreach (var item in db.Customers.Where(c =>  (c.City.ToLower().Contains(CityCountry.ToLower()) || c.Country.ToLower().Contains(CityCountry.ToLower())) && c.Email.ToLower().Contains(Email.ToLower())))
                    {
                        CustomerViewModel cvm = new CustomerViewModel();
                        cvm.Id = item.Id;
                        cvm.FirstName = item.FirstName;
                        cvm.LastName = item.LastName;
                        cvm.City = item.City;
                        cvm.Country = item.Country;
                        cvm.Phone = item.Phone;
                        cvm.Email = item.Email;


                        listCustomer.Add(cvm);
                    }
                }
                if (!string.IsNullOrEmpty(FullName) && string.IsNullOrEmpty(CityCountry) && string.IsNullOrEmpty(Email))
                {
                    foreach (var item in db.Customers.Where(c => (c.FirstName + " " + c.LastName).ToLower().Contains(FullName.ToLower())))
                    {
                        CustomerViewModel cvm = new CustomerViewModel();
                        cvm.Id = item.Id;
                        cvm.FirstName = item.FirstName;
                        cvm.LastName = item.LastName;
                        cvm.City = item.City;
                        cvm.Country = item.Country;
                        cvm.Phone = item.Phone;
                        cvm.Email = item.Email;


                        listCustomer.Add(cvm);
                    }
                }
                return listCustomer;
            }
        }

        public bool SearchFullName(string FullName)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                var customer = db.Customers.Where(c => (c.FirstName + " " + c.LastName).ToLower().Equals(FullName.ToLower())).SingleOrDefault();
                if (customer == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public bool SearchEmail(string Email)
        {
            using(ShopDBEntities db = new ShopDBEntities())
            {
                var customer = db.Customers.Where(c => c.Email.ToLower().Equals(Email.ToLower())).FirstOrDefault();
                if (customer == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }



    }

}
