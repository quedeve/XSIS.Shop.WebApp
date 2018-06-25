using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSIS.Shop.Models;
using XSIS.Shop.ViewModels;
using System.Data.Entity;

namespace XSIS.Shop.Repository
{
    public class SupplierRepository
    {
        public List<SupplierVIewModel> GetAllSupplier()
        {
            using(ShopDBEntities db = new ShopDBEntities())
            {
                List<SupplierVIewModel> supplierVIews = new List<SupplierVIewModel>();
                foreach (var item in db.Suppliers.ToList())
                {
                    SupplierVIewModel vIewModel = new SupplierVIewModel();
                    vIewModel.Id = item.Id;
                    vIewModel.CompanyName = item.CompanyName;
                    vIewModel.ContactName = item.ContactName;
                    vIewModel.ContactTitle = item.ContactTitle;
                    vIewModel.City = item.City;
                    vIewModel.Country = item.Country;
                    vIewModel.Phone = item.Phone;
                    vIewModel.Fax = item.Fax;

                    supplierVIews.Add(vIewModel);
                }

                return supplierVIews;
            }
        }

        public SupplierVIewModel GetSupplierById(int id)
        {
            using(ShopDBEntities db = new ShopDBEntities())
            {
                Supplier supplier = db.Suppliers.Find(id);
                SupplierVIewModel supplierVIew = new SupplierVIewModel();
                supplierVIew.Id = supplier.Id;
                supplierVIew.CompanyName = supplier.CompanyName;
                supplierVIew.ContactName = supplier.ContactName;
                supplierVIew.ContactTitle = supplier.ContactTitle;
                supplierVIew.City = supplier.City;
                supplierVIew.Country = supplier.Country;
                supplierVIew.Phone = supplier.Phone;
                supplierVIew.Fax = supplier.Fax;

                List<ProductViewModel> Listproduct = new List<ProductViewModel>();
                var products = db.Products.Where(m => m.SupplierId == supplierVIew.Id).ToList();
                    
                    foreach (var item in products)
                    {
                    ProductViewModel productView = new ProductViewModel();
                    productView.Id = item.Id;
                    productView.ProductName = item.ProductName;
                    productView.SupplierId = item.SupplierId;
                    productView.CompanyName = item.Supplier.CompanyName;
                    productView.UnitPrice = item.UnitPrice;
                    productView.Package = item.Package;
                    productView.IsDiscontinued = item.IsDiscontinued;

                    Listproduct.Add(productView);
                }
                supplierVIew.listproduct = Listproduct;
                


                return supplierVIew;
            }
        }

        public void AddNewSupplier(SupplierVIewModel supplierVIew)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Supplier supplier = new Supplier();
                supplier.Id = supplierVIew.Id;
                supplier.CompanyName = supplierVIew.CompanyName;
                supplier.ContactName = supplierVIew.ContactName;
                supplier.ContactTitle = supplierVIew.ContactTitle;
                supplier.City = supplierVIew.City;
                supplier.Country = supplierVIew.Country;
                supplier.Phone = supplierVIew.Phone;
                supplier.Fax = supplierVIew.Fax;

                db.Suppliers.Add(supplier);
                db.SaveChanges();
            }
        }

        public void UpdateSupplier(SupplierVIewModel supplierVIew)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Supplier supplier = new Supplier();
                supplier.Id = supplierVIew.Id;
                supplier.CompanyName = supplierVIew.CompanyName;
                supplier.ContactName = supplierVIew.ContactName;
                supplier.ContactTitle = supplierVIew.ContactTitle;
                supplier.City = supplierVIew.City;
                supplier.Country = supplierVIew.Country;
                supplier.Phone = supplierVIew.Phone;
                supplier.Fax = supplierVIew.Fax;

                db.Entry(supplier).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public void DeleteSupplier(int id)
        {
            using(ShopDBEntities db = new ShopDBEntities())
            {
                Supplier supplier = db.Suppliers.Find(id);
                db.Suppliers.Remove(supplier);
                db.SaveChanges();
            }
        }

       
    }
}
