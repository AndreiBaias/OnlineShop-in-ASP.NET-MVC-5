using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proiect_Tudose_Baias.Models
{
    public class Order
    {

        [Key]
        public int OrderId { get; set; }

        public virtual Product Product { get; set; }
        [ForeignKey("Product")]
        public int RequestId { get; set; }

        public int CartId { get; set; }
        public virtual Cart Cart{ get; set; }

    }
}