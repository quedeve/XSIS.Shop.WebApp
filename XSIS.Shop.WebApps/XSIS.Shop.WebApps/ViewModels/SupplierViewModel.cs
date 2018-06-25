using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace XSIS.Shop.WebApps.ViewModels
{
    public class SupplierViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Field Ini tidak boleh kosong"), MaxLength(40, ErrorMessage = "Panjang character tidak boleh lebih dari 40"), Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [MaxLength(50, ErrorMessage = "Panjang character tidak boleh lebih dari 50")]
        public string ContactName { get; set; }
        [MaxLength(40, ErrorMessage = "Panjang character tidak boleh lebih dari 40")]
        public string ContactTitle { get; set; }
        [MaxLength(40, ErrorMessage = "Panjang character tidak boleh lebih dari 40")]
        public string City { get; set; }
        [MaxLength(40, ErrorMessage = "Panjang character tidak boleh lebih dari 40")]
        public string Country { get; set; }
        [Phone(ErrorMessage = "Mesti Anggka Bro"), MaxLength(30, ErrorMessage = "Panjang character tidak boleh lebih dari 30")]
        public string Phone { get; set; }
        [MaxLength(30, ErrorMessage = "Panjang character tidak boleh lebih dari 30")]
        public string Fax { get; set; }
    }
}