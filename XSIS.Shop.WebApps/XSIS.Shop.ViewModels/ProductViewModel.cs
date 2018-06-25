using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSIS.Shop.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Field Ini tidak boleh kosong"), MaxLength(50, ErrorMessage = "Panjang character tidak boleh lebih dari 50"), Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Field ini tidak boleh kosong")]
        public int SupplierId { get; set; }
        public string CompanyName { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        [MaxLength(30, ErrorMessage = "Panjang character tidak boleh lebih dari 30"), Display(Name = "Package")]
        public string Package { get; set; }
        public bool IsDiscontinued { get; set; }
    }
}
