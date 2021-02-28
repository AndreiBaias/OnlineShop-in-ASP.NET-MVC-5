using Microsoft.AspNet.Identity;
using Proiect_Tudose_Baias.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proiect_Tudose_Baias.Controllers
{
    [Authorize(Roles = "Colaborator, Admin")]
    public class RequestsController : Controller
    {


        public ApplicationDbContext db = new ApplicationDbContext();


        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            //var articles = from article in db.Articles
            // select article;
            // Solutia: Eager loading
            var requests = db.Requests.ToList();
            ViewBag.Requests = requests;
            if (TempData.ContainsKey("Messagge"))
            {
                ViewBag.Messagge = TempData["Messagge"].ToString();
            }
            return View();
        }

        
        public ActionResult Show(int id)
        {
            Request request = db.Requests.Find(id);
            if(request.ColabId == User.Identity.GetUserId() || User.IsInRole("Admin"))
                return View(request);
            return Redirect("/");

        }
        public ActionResult New()
        {
            ViewBag.Categories = db.Categories;
            return View();
        }

        [HttpPost]
        public ActionResult New(Request request, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    var guid = Guid.NewGuid();
                    var extension = file.FileName.Split('.').Last();
                    string fileName = guid.ToString() + '.' + extension;
                    string path = Path.Combine(Server.MapPath("~/Images"),
                                                   Path.GetFileName(fileName));

                    request.RequestImage = $"~/Images/{Path.GetFileName(fileName)}";

                    if (!ModelState.IsValid)
                        return View(request);
                    //request.RequestId = 20;
                    request.Status = Status.Pending;
                    request.ColabId = User.Identity.GetUserId();
                    db.Requests.Add(request);
                    db.SaveChanges();
                    file.SaveAs(path);
                    return Redirect("/Requests/Show/" + request.RequestId);
                }
                catch (Exception e)
                {
                    /*ViewBag.Id = request.RequestId;*/
                    return View(request);
                }
            }
            else
            {
                if (!ModelState.IsValid)
                    return View(request);
                //request.RequestId = 20;
                request.Status = Status.Pending;
                request.RequestImage = " ";
                request.ColabId = User.Identity.GetUserId();
                db.Requests.Add(request);
                db.SaveChanges();
                return Redirect("/Requests/Show/" + request.RequestId);
            }
        }
        public ActionResult Edit(int id)
        {
            Request request = db.Requests.Find(id);
            ViewBag.Request = request;
            var requests = from req in db.Requests select req;
            ViewBag.Requests = requests;
            return View();
        }

        [Authorize(Roles = "Colaborator")]
        [HttpPut]
        public ActionResult Edit(int id, Request requestRequest)
        {
            try
            {
                Request request = db.Requests.Find(id);
                if (request.ColabId != User.Identity.GetUserId())
                    return Redirect("/");
                if (TryUpdateModel(request))
                {
                    request.RequestTitle = requestRequest.RequestTitle;
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
        [Authorize(Roles = "Colaborator")]
        public ActionResult Delete(int id)
        {
            Request request = db.Requests.Find(id);
            if (request.ColabId != User.Identity.GetUserId())
                return Redirect("/");
            if (request != null)
            {
                db.Requests.Remove(request);
                db.SaveChanges();
            }
            TempData["Messagge"] = "Cererea pentru " + request.RequestTitle + " a fost stearsa";
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Accept (int id)
        {
            Request request = db.Requests.Find(id);
            if(request != null)
            {
                request.Status = Status.Accepted;
                request.AdminId = User.Identity.GetUserId();
                /* if (!ModelState.IsValid)
                 {
                     ViewBag.Categories = db.Categories;
                     return View(product);

                 }*/
                var product = new Product
                {
                    RequestId = request.RequestId,
                    ProductTitle = request.RequestTitle,
                    ProductDescription = request.RequestDescription,
                    ProductPrice = request.RequestPrice,
                    CategoryId = request.CategoryId,
                    ProductImage = request.RequestImage,
                    ProductRating = 0,
                    UserId = request.ColabId
                }
                ;
                db.Products.Add(product);

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Deny(int id)
        {
            Request request = db.Requests.Find(id);
            if (request != null)
            {
                request.Status = Status.Rejected;
                request.AdminId = User.Identity.GetUserId();
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

    }

}