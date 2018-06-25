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
    public class ProductRepository
    {
        public List<ProductViewModel> GetAllProduct()
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                var products = db.Products.Include(p => p.Supplier);

                List<ProductViewModel> listProduct = new List<ProductViewModel>();
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

                    listProduct.Add(productView);

                }
                return listProduct;
            }
        }
        public ProductViewModel GetProductById(int id)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Product product = db.Products.Find(id);
                ProductViewModel productView = new ProductViewModel();
                productView.Id = product.Id;
                productView.ProductName = product.ProductName;
                productView.SupplierId = product.SupplierId;
                productView.CompanyName = product.Supplier.CompanyName;
                productView.UnitPrice = product.UnitPrice;
                productView.Package = product.Package;
                productView.IsDiscontinued = product.IsDiscontinued;

                return productView;

            }
        }

        public void AddNewProduct(ProductViewModel product)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Product productView = new Product();
                productView.Id = product.Id;
                productView.ProductName = product.ProductName;
                productView.SupplierId = product.SupplierId;
                productView.UnitPrice = product.UnitPrice;
                productView.Package = product.Package;
                productView.IsDiscontinued = product.IsDiscontinued;
                db.Products.Add(productView);
                db.SaveChanges();
            }
        }

        public void UpdateProduct(ProductViewModel product)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Product productView = new Product();

                productView.Id = product.Id;
                productView.ProductName = product.ProductName;
                productView.SupplierId = product.SupplierId;
                productView.UnitPrice = product.UnitPrice;
                productView.Package = product.Package;
                productView.IsDiscontinued = product.IsDiscontinued;
                db.Entry(productView).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void DeleteProduct(int id)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Product product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();
            }
        }

        public List<SupplierVIewModel> GetAllSupplier()
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                var ListSupplier = db.Suppliers.ToList();
                List<SupplierVIewModel> supplierViews = new List<SupplierVIewModel>();
                foreach (var item in db.Suppliers.ToList())
                {
                    SupplierVIewModel viewModel = new SupplierVIewModel();
                    viewModel.Id = item.Id;
                    viewModel.CompanyName = item.CompanyName;
                    viewModel.ContactName = item.ContactName;
                    viewModel.ContactTitle = item.ContactTitle;
                    viewModel.City = item.City;
                    viewModel.Country = item.Country;
                    viewModel.Phone = item.Phone;
                    viewModel.Fax = item.Fax;

                    supplierViews.Add(viewModel);
                }
                return supplierViews;
            }
        }

    }

}
