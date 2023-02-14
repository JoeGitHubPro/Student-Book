using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Student_Book;
using Microsoft.AspNet.Identity;
using static System.Web.HttpSessionStateBase;

namespace Student_Book.Controllers
{
    public class favsController : Controller
    {
        private ScienceEntities db = new ScienceEntities();

        // GET: favs
        public ActionResult Index()
        {

           
            string username = User.Identity.GetUserName().ToString();
            var favs = db.favs.Where(f=>f.UserName== username).Include(f => f.Subject).ToList();
            var sub = db.Subjects;

            List<Subject> list = new List<Subject>();

            foreach (var item in sub)
            {
                
                foreach (var items in favs)
                {
                    if (item.Id_Subject == items.Id_Subject )
                    {    
                        list.Add(item);
                      //  return View("Error");
                    }

                }
                
            }
            if (favs == null)
            {
                return View();
            }
          // db.Subjects.Include(x => x.Id_Subject).ToList();
            return View(list);
        }

        // GET: favs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fav fav = db.favs.Find(id);
            if (fav == null)
            {
                return HttpNotFound();
            }
            return View(fav);
        }

        // GET: favs/Create
        public ActionResult Create()
        {
            ViewBag.Id_Subject = new SelectList(db.Subjects, "Id_Subject", "Subject_Name");
            return View();
        }
        public ActionResult Favorite(string username, int subjectid)
        {
          
            fav fav = new fav();
            fav.UserName = username;
            fav.Id_Subject = subjectid;
            Subject sub = db.Subjects.Find(subjectid);
            fav.Subject = sub;
            db.favs.Add(fav);
            
            try
            {      
               db.SaveChanges();
            }
            catch (Exception)
            {
                return  RedirectToAction("Index", "Subjects");
            }
            
            return RedirectToAction("Index","Subjects");
        }
       // POST: favs/Create
       // To protect from overposting attacks, enable the specific properties you want to bind to, for 
       // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserName,Id_Subject")] fav fav/* string username , int subjectid*/)
        {
            //fav fav = new fav();
            //fav.UserName = username;
            //fav.Id_Subject = subjectid;
            //Subject sub = db.Subjects.Find(subjectid);
            //fav.Subject = sub;

            if (ModelState.IsValid)
            {
                db.favs.Add(fav);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Subject = new SelectList(db.Subjects, "Id_Subject", "Subject_Name", fav.Id_Subject);
            return View(fav);
        }

        // GET: favs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fav fav = db.favs.Find(id);
            if (fav == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Subject = new SelectList(db.Subjects, "Id_Subject", "Subject_Name", fav.Id_Subject);
            return View(fav);
        }

        // POST: favs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserName,Id_Subject")] fav fav)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fav).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Subject = new SelectList(db.Subjects, "Id_Subject", "Subject_Name", fav.Id_Subject);
            return View(fav);
        }

        // GET: favs/Delete/5
        public ActionResult Delete(int SubjectID)
        {
           // return View("Error");
            string username = User.Identity.GetUserName().ToString();
            fav fav = db.favs.Where(x => x.UserName == username && x.Id_Subject == SubjectID).SingleOrDefault();
            if (fav == null)
            {
                return View("Error");
            }
            db.favs.Remove(fav);
            db.SaveChanges();
            return RedirectToAction("Index","favs");

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //fav fav = db.favs.Find(id);
            //if (fav == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(fav);
        }

        // POST: favs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            
            fav fav = db.favs.Find(id);
            db.favs.Remove(fav);
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
