using Proiect_Tudose_Baias.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proiect_Tudose_Baias.Views
{

    public class CategoriesController : Controller
    {
        public ApplicationDbContext db = new ApplicationDbContext();

        /*[Authorize(Roles = "Inregistrat,Colaborator,Admin")]*/
        public ActionResult Index()
        {
            //var articles = from article in db.Articles
            // select article;
            // Solutia: Eager loading
            var categories = db.Categories.ToList();
            ViewBag.Categories = categories;
            if (TempData.ContainsKey("Messagge"))
            {
                ViewBag.Messagge = TempData["Messagge"].ToString();
            }
            return View();
        }

        /*[Authorize(Roles = "Inregistrat,Colaborator,Admin")]*/
        public ActionResult Show(int id)
        {
            Category category = db.Categories.Find(id);
            ViewBag.Category = category;
            return View(category);

        }

        [Authorize(Roles = "Colaborator,Admin")]
        public ActionResult New()
        {

            return View();
        }

        [HttpPost]
        public ActionResult New(Category category)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(category);
                db.Categories.Add(category);
                db.SaveChanges();
                return Redirect("/Categories/Show/" + category.CategoryId);
            }
            catch (Exception e)
            {

                return View(category);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            Category category = db.Categories.Find(id);
            ViewBag.Category = category;
            var categories = from cat in db.Categories select cat;
            ViewBag.Categories = categories;
            return View(category);
        }

        [HttpPut]
        public ActionResult Edit(int id, Category requestCategory)
        {
            try
            {
                Category category = db.Categories.Find(id);
                if (TryUpdateModel(category))
                {
                    category.CategoryName = requestCategory.CategoryName;
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
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Category category = db.Categories.Find(id);
            if (category != null)
            {
                db.Categories.Remove(category);
                db.SaveChanges();
            }
            TempData["Messagge"] = "A disparut " + category.CategoryName + " boss ia-o de unde nu e";
            return RedirectToAction("Index");
        }

    }
}