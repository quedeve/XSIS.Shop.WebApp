using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSIS.Shop.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Field Ini tidak boleh kosong")]
        public System.DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; }
        [Required(ErrorMessage = "Field Ini tidak boleh kosong")]
        public int CustomerId { get; set; }
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }

        public List<OrderItemViewModel> Detail { get; set; }
        public List<CustomerViewModel> listCustomer { get; set; }

    }
}
