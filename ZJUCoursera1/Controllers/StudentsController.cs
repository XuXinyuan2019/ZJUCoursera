using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ZJUCoursera1.DAL;
using ZJUCoursera1.Models;

namespace ZJUCoursera1.Controllers
{
    public class StudentsController : Controller
    {
        private ZJUSCDB db = new ZJUSCDB();

        // GET: Students
        public ActionResult Index()
        {
            return View(db.Students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        //学生注册
        public ActionResult Registe()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registe(RegisteModel model, string VerifyCode)
        {
            if (ModelState.IsValid)
            {
                Student aStudent = new Student();
                aStudent.Number = model.Number;
                aStudent.Name = model.Name;
                aStudent.Year = model.Year;
                aStudent.Major = model.Major;
                aStudent.Password = model.Password;
                db.Students.Add(aStudent);
                db.SaveChanges();
                Session["CurrentUser"] = aStudent;
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        //学生登录
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            string Result;
            if (ModelState.IsValid) { Result = ValidateUser(model); }
            else { Result = "提供的用户名或密码不正确。"; }
            if (Result == "登录成功！")
            {
                return RedirectToLocal(returnUrl);
            }
            else
            {
                ModelState.AddModelError("", Result);
                return View(model);
            }
        }

        //管理员登录
        [AllowAnonymous]
        public ActionResult Login2(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login2(LoginModel2 model, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            string Result;
            if (ModelState.IsValid) { Result = ValidateUser2(model); }
            else { Result = "提供的用户名或密码不正确。"; }
            if (Result == "登录成功！")
            {
                return RedirectToLocal(returnUrl);
            }
            else
            {
                ModelState.AddModelError("", Result);
                return View(model);
            }
        }
        //判断学生登录信息是否合法
        private string ValidateUser(LoginModel model)
        {
            string Result;
            Student aUser = db.Students.SingleOrDefault(r => r.Number == model.Number);
            if (aUser == null) { Result = "用户名不存在！"; }
            else if (aUser.Password != model.Password) { Result = "用户名密码不匹配！"; }
            else  //校验通过
            {
                Session["CurrentUser"] = aUser;
                if (model.RememberMe)
                {
                    HttpCookie TokenCookie = new HttpCookie("Token");
                    TokenCookie.Value = aUser.ID.ToString() + "/" + aUser.Number + "/" + aUser.Name;
                    TokenCookie.Expires = DateTime.Now.AddDays(7);
                    //TokenCookie.Expires = DateTime.Now.AddMinutes(10);
                    TokenCookie.HttpOnly = true;
                    Response.Cookies.Add(TokenCookie);
                    //Response.Cookies["Token"] = aUser.Id.ToString() + "/" + aUser.LoginName + "/" + aUser.Name;
                }
                Result = "登录成功！";
            }
            return Result;
        }
        //判断管理员登录是否合法
        private string ValidateUser2(LoginModel2 model)
        {
            string Result;
            if ((model.Password).Equals(123456) && (model.Number).Equals(123456))
            //校验通过
            {
                Result = "登录成功！";
                Session["CurrentManager"] = 1;
            }
            else
            {
                Result = "登录成功！";
                Session["CurrentManager"] = 2;
            }
            return Result;
        }

        //通用注销
        [HttpPost]
        public ActionResult LogOff(string returnUrl)
        {
            Session["CurrentUser"] = null;
            Session["CurrentManager"] = null;
            Response.Cookies.Remove("Token");

            return RedirectToLocal(returnUrl);
        }




        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Number,Name,Year,Major,Password")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Number,Name,Year,Major,Password")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
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
