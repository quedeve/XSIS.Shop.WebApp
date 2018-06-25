using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSIS.Shop.ViewModels
{
    public class OrderItemViewModel
    {

        public int Id { get; set; }
        public int OrderId { get; set; }
        [Display(Name = "Product item")]
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        [Display(Name = "Quantity"),
         RegularExpression("[0-9]+", ErrorMessage = "Only positive numbers")]
        public int Quantity { get; set; }

        public Nullable<decimal> TotalAmount { get; set; }
        public string ProductName { get; set; }
        public List<ProductViewModel> ListProduct { get; set; }
    }
}
