using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using bai5_Anh.Models;
using PagedList;
namespace bai5_Anh.Controllers
{
    public class GroupImagesController : Controller
    {
        private DataContext db = new DataContext();

        // GET: GroupImages
        public ActionResult Index(int? page)
        {
            return View(db.grimg.OrderBy(i => i.IdGroupImage).ToPagedList(page ?? 1, 5));
        }

        // GET: GroupImages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupImages groupImages = db.grimg.Find(id);
            if (groupImages == null)
            {
                return HttpNotFound();
            }
            return View(groupImages);
        }

        // GET: GroupImages/Create
        [Authorize(Users = "admin@gmail.com")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: GroupImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdGroupImage,NameGroup")] GroupImages groupImages)
        {
            if (ModelState.IsValid)
            {
                db.grimg.Add(groupImages);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(groupImages);
        }

        // GET: GroupImages/Edit/5
        [Authorize(Users = "admin@gmail.com")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupImages groupImages = db.grimg.Find(id);
            if (groupImages == null)
            {
                return HttpNotFound();
            }
            return View(groupImages);
        }

        // POST: GroupImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdGroupImage,NameGroup")] GroupImages groupImages)
        {
            if (ModelState.IsValid)
            {
                db.Entry(groupImages).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(groupImages);
        }

        // GET: GroupImages/Delete/5
        [Authorize(Users = "admin@gmail.com")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupImages groupImages = db.grimg.Find(id);
            if (groupImages == null)
            {
                return HttpNotFound();
            }
            return View(groupImages);
        }

        // POST: GroupImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GroupImages groupImages = db.grimg.Find(id);
            db.grimg.Remove(groupImages);
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
