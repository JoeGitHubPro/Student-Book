using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Student_Book;

namespace Student_Book.Controllers
{
    public class LinksController : Controller
    {
        private ScienceEntities db = new ScienceEntities();

        // GET: Links
        public ActionResult Index()
        {
            var links = db.Links/*.Include(l => l.Link11).Include(l => l.Link2).Include(l => l.Subject)*/;

            return View(links.ToList());


        }

        // GET: Links/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Link link = db.Links.Find(id);
            if (link == null)
            {
                return HttpNotFound();
            }
            return View(link);
        }

        // GET: Links/Create
        public ActionResult Create()
        {
            ViewBag.Id_Link = new SelectList(db.Links, "Id_Link", "Pdf_Name");
            ViewBag.Id_Link = new SelectList(db.Links, "Id_Link", "Pdf_Name");
            ViewBag.Id_Subject = new SelectList(db.Subjects, "Id_Subject", "Subject_Name");
            return View();
        }

        // POST: Links/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Link,Id_Subject,Pdf_Name,Link1")] Link link)
        {
            if (ModelState.IsValid)
            {
                db.Links.Add(link);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Link = new SelectList(db.Links, "Id_Link", "Pdf_Name", link.Id_Link);
            ViewBag.Id_Link = new SelectList(db.Links, "Id_Link", "Pdf_Name", link.Id_Link);
            ViewBag.Id_Subject = new SelectList(db.Subjects, "Id_Subject", "Subject_Name", link.Id_Subject);
            return View(link);
        }

        // GET: Links/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Link link = db.Links.Find(id);
            if (link == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Link = new SelectList(db.Links, "Id_Link", "Pdf_Name", link.Id_Link);
            ViewBag.Id_Link = new SelectList(db.Links, "Id_Link", "Pdf_Name", link.Id_Link);
            ViewBag.Id_Subject = new SelectList(db.Subjects, "Id_Subject", "Subject_Name", link.Id_Subject);
            return View(link);
        }

        // POST: Links/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Link,Id_Subject,Pdf_Name,Link1")] Link link)
        {
            if (ModelState.IsValid)
            {
                db.Entry(link).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Link = new SelectList(db.Links, "Id_Link", "Pdf_Name", link.Id_Link);
            ViewBag.Id_Link = new SelectList(db.Links, "Id_Link", "Pdf_Name", link.Id_Link);
            ViewBag.Id_Subject = new SelectList(db.Subjects, "Id_Subject", "Subject_Name", link.Id_Subject);
            return View(link);
        }

        // GET: Links/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Link link = db.Links.Find(id);
            if (link == null)
            {
                return HttpNotFound();
            }
            return View(link);
        }

        // POST: Links/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Link link = db.Links.Find(id);
            db.Links.Remove(link);
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
