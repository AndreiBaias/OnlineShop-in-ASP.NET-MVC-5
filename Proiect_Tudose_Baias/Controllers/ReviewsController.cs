using Microsoft.AspNet.Identity;
using Proiect_Tudose_Baias.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;

namespace Proiect_Tudose_Baias.Views
{

    public class ReviewsController : Controller
    {
        public ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            //var articles = from article in db.Articles
            // select article;
            // Solutia: Eager loading
            var reviews = db.Reviews.ToList();
            ViewBag.Reviews = reviews;
            if (TempData.ContainsKey("Messagge"))
            {
                ViewBag.Messagge = TempData["Messagge"].ToString();
            }
            return View();
        }

        [Authorize(Roles = "Inregistrat,Colaborator,Admin")]
        public ActionResult Show(int id)
        {
            Review review = db.Reviews.Find(id);
            ViewBag.Review = review;
            return View(review);

        }

        [Authorize(Roles = "Inregistrat,Colaborator,Admin")]
        public ActionResult New(int id)
        {

            Product product = db.Products.Find(id);
            var review = new Review
            {
                RequestId = product.RequestId

            }
            ;
            return View(review);
        }

        [HttpPost]
        public ActionResult New(Review review)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(review);
                review.UserId = User.Identity.GetUserId();
                var product = db.Products.Find(review.RequestId);
                product.Reviews.Add(review);
                review.Product = product;
                db.Reviews.Add(review);
                db.SaveChanges();

                return Redirect("/Products/Show/" + review.Product.RequestId);
            }
            catch (Exception e)
            {

                return View(review);
            }
        }

        [Authorize(Roles = "Inregistrat,Colaborator,Admin")]
        public ActionResult Edit(int id)
        {
            Review review = db.Reviews.Find(id);
            ViewBag.Review = review;
            var reviews = from cat in db.Reviews select cat;
            ViewBag.Reviews = reviews;
            return View(review);
        }

        [HttpPut]
        public ActionResult Edit(int id, Review requestReview)
        {
            try
            {
                Review review = db.Reviews.Find(id);
                if (TryUpdateModel(review))
                {
                    review.ReviewRating = requestReview.ReviewRating;
                    review.ReviewText = requestReview.ReviewText;
                    db.SaveChanges();
                }
                /*return RedirectToAction("Show" , "Products" + review.RequestId);*/
                return Redirect("/Products/Show/" + review.RequestId);
            }
            catch (Exception e)
            {
                return View();
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Inregistrat,Colaborator,Admin")]
        public ActionResult Delete(int id)
        {
            Review review = db.Reviews.Find(id);
            if (review != null /*&& review.UserId == User.Identity.GetUserId()*/)
            {
                db.Reviews.Remove(review);
                db.SaveChanges();
                TempData["Messagge"] = "Ai sters review-ul " + review.ReviewId;
                return Redirect("/Products/Show/" + review.RequestId);
            }
            else
            {
                TempData["Messagge"] = "Nu poti sterge review-ul " + review.ReviewId;
                return Redirect("/Products/Show/" + review.RequestId);
            }
            
        }

    }
}