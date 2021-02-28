
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proiect_Tudose_Baias.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }


        [ForeignKey("Product")]
        public int RequestId { get; set; }

        public string ReviewText { get; set; }

        public int ReviewRating { get; set; }

        public virtual Product Product { get; set; }
    }
}