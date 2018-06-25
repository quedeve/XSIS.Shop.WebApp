using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace XSIS.Shop.WebApps.ViewModels
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "First Name Tidak Boleh Kosong"), Display(Name = "Nama Depan"), MaxLength(40, ErrorMessage = "Terlalu Panjang Nama Depan Ente")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name Tidak Boleh Kosong"), Display(Name = "Nama Belakang"), MaxLength(40, ErrorMessage = "Terlalu Panjang Nama Belakang Ente")]
        public string LastName { get; set; }
        [MaxLength(40, ErrorMessage = "Ga boleh Lebih dari 40 character bray")]
        public string City { get; set; }
        [MaxLength(40, ErrorMessage = "Ga boleh Lebih dari 40 character bray")]
        public string Country { get; set; }
        [MaxLength(20, ErrorMessage = "Ga boleh Lebih dari 20 character bray"), Phone(ErrorMessage = "Mesti Nomor telpon coy")]
        public string Phone { get; set; }
        [EmailAddress, MaxLength(50, ErrorMessage = "Ga boleh Lebih dari 50 character bray")]
        public string Email { get; set; }
    }
}