using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proiect_Tudose_Baias.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [RegularExpression("[A-Z][a-z ]*", ErrorMessage = "Doar litere si spatiu")]
        public string CategoryName { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<Request> Requests { get; set; }


    }
}