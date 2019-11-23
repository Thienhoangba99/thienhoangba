using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using bai5_Anh.Models;
using PagedList;
namespace bai5_Anh.Controllers
{
    public class ImagesController : Controller
    {
        private DataContext db = new DataContext();

        [HttpPost]
        public bool UpdateCount(int c)
        {
            var img = db.img.Include(i => i.Images_GroupImages);
            Images imgtemp = img.FirstOrDefault(i => i.IdImages == c);
            imgtemp.CountView++;
            db.Entry(imgtemp).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }
        // GET: Images

        public ActionResult Index(int? page, string sx)
        {
            var img = db.img.Include(i => i.Images_GroupImages);
            switch (sx)
            {
                case "NameGroup":
                    img = img.OrderBy(i => i.Images_GroupImages.NameGroup);
                    break;
                case "ImagePath":
                    img = img.OrderBy(i => i.ImagePath);
                    break;
                case "Title":
                    img = img.OrderBy(i => i.Title);
                    break;
                case "Introduce":
                    img = img.OrderBy(i => i.Introduce);
                    break;
                case "DateSubmit":
                    img = img.OrderBy(i => i.DateSubmit);
                    break;
                case "Email":
                    img = img.OrderBy(i => i.Email);
                    break;
                case "CountView":
                    img = img.OrderBy(i => i.CountView);
                    break;
                default:
                    img = img.OrderBy(i => i.IdImages);
                    break;
            }
            return View(img.ToPagedList(page ?? 1, 5));
        }

        // GET: Images/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Images images = db.img.Find(id);
            if (images == null)
            {
                return HttpNotFound();
            }
            return View(images);
        }

        // GET: Images/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.IdGroupImage = new SelectList(db.grimg, "IdGroupImage", "NameGroup");
            return View();
        }

        // POST: Images/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdImages,ImagePath,Title,Introduce,DateSubmit,Email,CountView,IdGroupImage")] Images images, HttpPostedFileBase ImagePath)
        { 
            images.DateSubmit = DateTime.Now;
            images.Email = User.Identity.Name;
            string fileName = Path.GetFileNameWithoutExtension(ImagePath.FileName);
            string extension = Path.GetExtension(ImagePath.FileName).ToLower();
            if (extension == ".jpeg" || extension == ".png" || extension == ".jpg" || extension == ".gif")
            {
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                fileName = Path.Combine(Server.MapPath("~/Content/Images/" + fileName));
                ImagePath.SaveAs(fileName);
                string[] ArrUrl = fileName.Split('\\');
                fileName = "~/" + ArrUrl[ArrUrl.Length - 3] + "/" + ArrUrl[ArrUrl.Length - 2] + "/" + ArrUrl[ArrUrl.Length - 1];
                images.ImagePath = fileName;
                if (ModelState.IsValid)
                {
                    db.img.Add(images);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.IdGroupImage = new SelectList(db.grimg, "IdGroupImage", "NameGroup", images.IdGroupImage);
            return View(images);
        }

        // GET: Images/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Images images = db.img.Find(id);
            if (images == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdGroupImage = new SelectList(db.grimg, "IdGroupImage", "NameGroup", images.IdGroupImage);
            return View(images);
        }

        // POST: Images/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdImages,ImagePath,Title,Introduce,DateSubmit,Email,CountView,IdGroupImage")] Images images, HttpPostedFileBase ImagePath)
        {
            images.DateSubmit = DateTime.Now;
            //images.Email = User.Identity.Name;
            string fileName = Path.GetFileNameWithoutExtension(ImagePath.FileName);
            string extension = Path.GetExtension(ImagePath.FileName).ToLower();
            if (extension == ".jpeg" || extension == ".png" || extension == ".jpg" || extension == ".gif")
            {
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                fileName = Path.Combine(Server.MapPath("~/Content/Images/" + fileName));
                ImagePath.SaveAs(fileName);
                string[] ArrUrl = fileName.Split('\\');
                fileName = "~/" + ArrUrl[ArrUrl.Length - 3] + "/" + ArrUrl[ArrUrl.Length - 2] + "/" + ArrUrl[ArrUrl.Length - 1];
                images.ImagePath = fileName;
                if (ModelState.IsValid)
                {
                    db.Entry(images).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.IdGroupImage = new SelectList(db.grimg, "IdGroupImage", "NameGroup", images.IdGroupImage);
            return View(images);
        }

        // GET: Images/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Images images = db.img.Find(id);
            if (images == null)
            {
                return HttpNotFound();
            }
            return View(images);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Images images = db.img.Find(id);
            db.img.Remove(images);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
