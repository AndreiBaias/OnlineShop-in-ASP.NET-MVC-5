using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proiect_Tudose_Baias.Models
{
    public class Cart
    {

        public int CartId { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }

        public string CartAdress { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Order> Orders { get; set; }


    }
}