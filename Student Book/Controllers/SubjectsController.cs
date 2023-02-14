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
    public class SubjectsController : Controller
    {
        private ScienceEntities db = new ScienceEntities();

        // GET: Subjects
        public ActionResult Index()
        {
            return View(db.Subjects.ToList());



        }
       

        // GET: Subjects/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Link Link = db.Links.Find(id);
            if (db.Links.ToList().Where(x => x.Id_Subject == id) == null)
            {
                return View("LinkErorr");
              //  return HttpNotFound();

            }
            string name = db.Subjects.Find(id).Subject_Name.ToString();
            ViewBag.Subject = name;
            TempData["Subjectid"] = id;
            TempData["id"] = id;
            TempData["Subjectname"] = name;
            ViewBag.SubjectDescrption = db.Subjects.Find(id).Subject_Descrption.ToString();

            return View(db.Links.ToList().Where(x => x.Id_Subject == id));
        }

        public ActionResult Search(string S)
        {
            S = S.ToUpper();
            if (S == null||S=="")
            {
                return View("Error");

                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Student_Book.Subject> subject = db.Subjects.ToList();
            Subject Sub = subject.Where(x => x.Subject_Descrption == S).Single();
            if (db.Subjects.ToList().Where(x => x.Subject_Descrption == S) == null)
            {
                return View("LinkErorr");
                //  return HttpNotFound();

            }
            ViewBag.Subject = db.Subjects.Find(Sub.Id_Subject).Subject_Name.ToString();
            ViewBag.SubjectDescrption = db.Subjects.Find(Sub.Id_Subject).Subject_Descrption.ToString();

            return View("Details", db.Links.ToList().Where(x => x.Id_Subject == Sub.Id_Subject));
        }

        // GET: Subjects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Subjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Subject,Subject_Name,Subject_Descrption")] Subject subject)
        {
            if (ModelState.IsValid)
            {
               subject.Subject_Descrption = subject.Subject_Descrption.ToUpper();  
                db.Subjects.Add(subject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(subject);
        }

        // GET: Subjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = db.Subjects.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // POST: Subjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Subject,Subject_Name,Subject_Descrption")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                subject.Subject_Descrption = subject.Subject_Descrption.ToUpper();
                db.Entry(subject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subject);
        }

       // GET: Subjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = db.Subjects.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);

        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Subject subject = db.Subjects.Find(id);
            db.Subjects.Remove(subject);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
       // GET: Links/Delete/5
    

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
