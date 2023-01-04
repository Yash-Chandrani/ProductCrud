using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ADO_Example.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required]
        [DisplayName("Product Name")]
        public String ProductName { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Qty { get; set; }
        public String Remarks { get; set; }
    }
}