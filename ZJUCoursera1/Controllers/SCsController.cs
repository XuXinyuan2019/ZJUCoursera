using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ZJUCoursera1.DAL;

namespace ZJUCoursera1.Controllers
{
    public class SCsController : Controller
    {
        private ZJUSCDB db = new ZJUSCDB();

        public ActionResult Index()
        {
            ZJUCoursera1.DAL.Student CurrentUser = (ZJUCoursera1.DAL.Student)(Session["CurrentUser"]);
            var sCs = db.SCs.Include(s => s.Course).Include(s => s.Student);
            var sCs2 = sCs.ToList();
            foreach (var item in sCs)
            {
                if (item.SID != CurrentUser.ID)
                {
                    sCs2.Remove(item);
                }
            }

            return View(sCs2);
        }
        // GET: SCs

        public ActionResult Index_original()
        {
            var sCs = db.SCs.Include(s => s.Course).Include(s => s.Student);
            return View(sCs.ToList());
        }

        // GET: SCs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SC sC = db.SCs.Find(id);
            if (sC == null)
            {
                return HttpNotFound();
            }
            return View(sC);
        }

        // GET: SCs/Create
        public ActionResult Create()
        {
            ViewBag.CID = new SelectList(db.Courses, "ID", "Name");
            ViewBag.SID = new SelectList(db.Students, "ID", "Number");
            return View();
        }

        // POST: SCs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SID,CID,SelectAt")] SC sC)
        {
            if (ModelState.IsValid)
            {
                db.SCs.Add(sC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CID = new SelectList(db.Courses, "ID", "Name", sC.CID);
            ViewBag.SID = new SelectList(db.Students, "ID", "Number", sC.SID);
            return View(sC);
        }

        // GET: SCs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SC sC = db.SCs.Find(id);
            if (sC == null)
            {
                return HttpNotFound();
            }
            ViewBag.CID = new SelectList(db.Courses, "ID", "Name", sC.CID);
            ViewBag.SID = new SelectList(db.Students, "ID", "Number", sC.SID);
            return View(sC);
        }

        // POST: SCs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SID,CID,SelectAt")] SC sC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CID = new SelectList(db.Courses, "ID", "Name", sC.CID);
            ViewBag.SID = new SelectList(db.Students, "ID", "Number", sC.SID);
            return View(sC);
        }

        // GET: SCs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SC sC = db.SCs.Find(id);
            if (sC == null)
            {
                return HttpNotFound();
            }
            return View(sC);
        }

        // POST: SCs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SC sC = db.SCs.Find(id);
            db.SCs.Remove(sC);
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
