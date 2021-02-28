using Microsoft.AspNet.Identity;
using Proiect_Tudose_Baias.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proiect_Tudose_Baias.Views
{
    [Authorize]
    public class OrdersController : Controller
    {

        public ApplicationDbContext db = new ApplicationDbContext();

        
        public ActionResult Index()
        {
            //var articles = from article in db.Articles
            // select article;
            // Solutia: Eager loading
            var orders = db.Orders.ToList();
            ViewBag.Orders = orders;
            if (TempData.ContainsKey("Messagge"))
            {
                ViewBag.Messagge = TempData["Messagge"].ToString();
            }
            return View();
        }

        public ActionResult Show(int id)
        {
            Order order = db.Orders.Find(id);
            return View(order);

        }

        public ActionResult New()
        {
            ViewBag.Orders = db.Orders;
            ViewBag.Products = db.Products;
            return View();
        }

        [HttpPost]
        public ActionResult New(Order order)
        {
            var product = db.Products.Find(order.RequestId);
            if (!ModelState.IsValid)
                return View(order);
            var userId = User.Identity.GetUserId();
            var cart = db.Carts.FirstOrDefault(x => x.UserId == userId);
            if (cart == null)
                return Redirect("/Cart/New");
            order.Cart = cart;
            order.CartId = cart.CartId;
            order.Product = product;
            order.RequestId = product.RequestId;
            db.Orders.Add(order);
            db.SaveChanges();
            return Redirect("/Orders/Show/" + order.OrderId);
        }
        public ActionResult addToBasket(Order order, Product product)
        { 
            if (!ModelState.IsValid)
                return View(order);
            var userId = User.Identity.GetUserId();
            var cart = db.Carts.FirstOrDefault(x => x.UserId == userId);
            if (cart == null)
                return Redirect("/Cart/New");
            order.CartId = cart.CartId;
            order.Product = product;
            db.Orders.Add(order);

            return Redirect("/Cart");
        }
        public ActionResult Edit(int id)
        {
            Order order = db.Orders.Find(id);
            /*ViewBag.Order = order;
            var orders = from ord in db.Orders select ord;
            ViewBag.Orders = orders;*/
            return View(order);
        }

        [HttpPut]
        public ActionResult Edit(int id, Order requestOrder)
        {
            try
            {
                Order order = db.Orders.Find(id);
                if (TryUpdateModel(order))
                {
                    order.OrderId = requestOrder.OrderId;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Order order = db.Orders.Find(id);
            if (order != null)
            {
                db.Orders.Remove(order);
                db.SaveChanges();
            }
            TempData["Messagge"] = "A disparut " + order.OrderId + " boss ia-o de unde nu e";
            return RedirectToAction("Index");
        }

    }
}