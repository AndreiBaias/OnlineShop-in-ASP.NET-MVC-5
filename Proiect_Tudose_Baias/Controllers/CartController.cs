using Microsoft.AspNet.Identity;
using Proiect_Tudose_Baias.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proiect_Tudose_Baias.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        
        public ApplicationDbContext db = new ApplicationDbContext();
        // GET: Cart
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var cart = db.Carts.FirstOrDefault(x=>x.UserId == userId);
            return View(cart);
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(Cart cart)
        {
            try
            {
                cart.UserId = User.Identity.GetUserId();
                if (!ModelState.IsValid)
                    return View(cart);
                db.Carts.Add(cart);
                db.SaveChanges();
                return Redirect("/Cart/Index/");
            }
            catch (Exception e)
            {
                ViewBag.Id = cart.CartId;
                return View(cart);
            }
        }
    }
}