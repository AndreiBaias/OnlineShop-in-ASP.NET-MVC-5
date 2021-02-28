using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proiect_Tudose_Baias.Models
{
    public class Request
    {

        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequestId { get; set; }

        public string ColabId { get; set; }

        public string RequestImage { get; set; }
        public string AdminId { get; set; }

        public Status Status { get; set; }

        public string RequestTitle { get; set; }

        public string RequestDescription { get; set; }

        public int RequestPrice { get; set; }

        public int  CategoryId { get; set; }

        public virtual Product Product { get; set; }

        public virtual Category Category { get; set; }

        public virtual ApplicationUser Colab { get; set; }

        public virtual ApplicationUser Admin { get; set; }

    }
}