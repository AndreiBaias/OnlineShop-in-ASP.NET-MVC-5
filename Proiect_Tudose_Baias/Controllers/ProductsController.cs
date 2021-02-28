using Microsoft.AspNet.Identity;
using Proiect_Tudose_Baias.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Data.Entity;

namespace Proiect_Tudose_Baias.Views
{
    /*[Authorize]*/
    public class ProductsController : Controller
    {
        public ApplicationDbContext db = new ApplicationDbContext();
        //[Authorize(Roles = "Inregistrat, Colaborator, Admin")]
        public ActionResult Index(string searching, string sort)
        {

            var products = db.Products.ToList();
            if (TempData.ContainsKey("Messagge"))
            {
                ViewBag.Messagge = TempData["Messagge"].ToString();
            }

            if (string.IsNullOrEmpty(searching)) //nu avem searching
            {
                if (string.IsNullOrEmpty(sort))
                {
                    ViewBag.Products = products/*.OrderBy(x => x.ProductTitle)*/;
                    return View(products);
                }
                else
                {
                    if (sort == "Crescator")
                    {
                        ViewBag.Products = products.OrderBy(x => x.ProductTitle);
                        return View(products);
                    }
                    else if (sort == "Descrescator")
                    {
                        ViewBag.Products = products.OrderByDescending(x => x.ProductTitle);
                        return View(products);
                    }
                    else
                    {
                        ViewBag.Products = products;
                        return View(products);
                    }
                }
                /*ViewBag.Products = products*//*.OrderBy(x => x.ProductTitle)*//*;
                return View(products);*/
            }
            else
            {
                ViewBag.Products = products.Where(x => (x.ProductTitle.Contains(searching))).ToList();
                return View(products.ToList().Where(x => (x.ProductTitle.Contains(searching))).ToList());
            }

        }

        /*[Authorize(Roles = "Inregistrat, Colaborator, Admin")]*/
        public ActionResult Show(int id)
        {
            var iuzar = User.Identity.GetUserName();
            ViewBag.iuzar = iuzar;
            Product product = db.Products.Find(id);
            var reviews = db.Reviews.Where(x => x.RequestId == product.RequestId).
                Include(x => x.ApplicationUser).ToList();
            ViewBag.Reviews = reviews;
            return View(product);

        }
        [Authorize(Roles = "Colaborator, Admin")]
        public ActionResult New()
        {
            var category = db.Categories.ToList();
            ViewBag.Categories = db.Categories;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Colaborator, Admin")]
        public ActionResult New(Product product)
        {
            try
            {
                //product.ProductId = 2;
                //product.CategoryId = 2;
                /*product.RequestId = 6;*/
                if (!ModelState.IsValid)
                {
                    ViewBag.Categories = db.Categories;
                    return View(product);

                }
                product.UserId = User.Identity.GetUserId();
                product.ProductRating = -1;
                db.Products.Add(product);
                db.SaveChanges();
                return Redirect("/Products/Show/" + product./*ProductId*/RequestId);
            }
            catch (Exception e)
            {
                ViewBag.Id = product./*ProductId*/RequestId;
                return RedirectToAction("Index");
            }
        }
        [Authorize(Roles = "Colaborator, Admin")]
        public ActionResult Edit(int id)
        {
            Product product = db.Products.Find(id);
            ViewBag.Product = product;
            /*var products = from prod in db.Products select prod;
            ViewBag.Products = products;*/
            if (product.UserId == User.Identity.GetUserId() || User.IsInRole("Admin"))
            {
                return View(product);
            }
            else
            {
                TempData["message"] = "Nu aveti dreptul sa faceti modificari asupra unui acestui articol!";
                return RedirectToAction("Index");
            }
        }

        [HttpPut]
        [Authorize(Roles = "Colaborator, Admin")]
        public ActionResult Edit(int id, Product requestProduct)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Product product = db.Products.Find(id);

                    if (product.UserId == User.Identity.GetUserId() || User.IsInRole("Admin"))
                    {
                        if (TryUpdateModel(product))
                        {
                            product.ProductTitle = requestProduct.ProductTitle;
                            product.ProductPrice = requestProduct.ProductPrice;
                            product.ProductDescription = requestProduct.ProductDescription;
                            db.SaveChanges();
                            TempData["message"] = "Articolul a fost modificat";
                        }
                        return RedirectToAction("Index");
                    }
                    else
                    {

                        TempData["message"] = "Nu aveti dreptul sa faceti modificari asupra acestui articol!";
                        return RedirectToAction("Index");
                    }
                }

                else
                {
                    //requestProduct.Category = GetAllCategories();
                    return View(requestProduct);
                }
            }
            catch (Exception e)
            {
                //requestProduct.Category = GetAllCategories();
                return View(requestProduct);
            }

        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Product product = db.Products.Find(id);
            if (product != null)
            {
                db.Products.Remove(product);
                db.SaveChanges();
            }
            TempData["Messagge"] = "A disparut " + product.ProductTitle + " boss ia-o de unde nu e";
            return RedirectToAction("Index");
        }
    }
}