using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proiect_Tudose_Baias.Models
{
    public class Product
    {
        [RegularExpression("[A-Z][a-z ]*", ErrorMessage = "Doar litere si spatiu, incepand cu majuscula")]
        public string ProductTitle { get; set; }

        public string ProductDescription { get; set; }

        public int ProductPrice { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public float ProductRating { get; set; }

        public string ProductImage { get; set; }

        [Key]
        [ForeignKey("Request")]
        public int RequestId { get; set; }

        public virtual Category Category { get; set; }

        public virtual Request Request { get; set; }

        public string UserId { get; set; }

        public virtual ICollection<Review>  Reviews { get; set; }
    }
}