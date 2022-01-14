using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ZJUCoursera1.DAL;
using ZJUCoursera1.Models;

namespace ZJUCoursera1.Controllers
{
    public class HomeController : Controller
    {
        private ZJUSCDB db = new ZJUSCDB();
        List<Subject> PublisherList;
        SelectList PublisherSelector;
        Student CurrentUser;
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            PublisherList = db.Subjects.ToList();
            PublisherSelector = new SelectList(PublisherList, "Id", "Name", "");
            ViewBag.Database = db;
            ViewBag.Controller = RouteData.Values.ContainsKey("controller") ? RouteData.Values["controller"].ToString().ToLower() : "home";
            ViewBag.Action = RouteData.Values.ContainsKey("action") ? RouteData.Values["action"].ToString().ToLower() : "index";
            ViewBag.ID = RouteData.Values.ContainsKey("id") ? RouteData.Values["id"].ToString().ToLower() : string.Empty;
            CurrentUser = (Session["CurrentUser"] == null) ? null : ((Student)(Session["CurrentUser"]));
        }

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
        //这个没啥用
        public ActionResult Index_original()
        {
            return View();
        }

        public ActionResult Index2()
        {
            List<Course> BookList = db.Courses.OrderByDescending(r => r.ID).Take(10).ToList();
            ViewBag.BookList = BookList;
            return View(db.Weekdays.ToList());
        }
        //显示学习工具页面
        public ActionResult Tool()
        {
            return View();
        }
        //显示所有学科和课程
        public ActionResult Index()
        {
            List<Course> BookList = db.Courses.OrderByDescending(r => r.ID).Take(10).ToList();
            ViewBag.BookList = BookList;
            return View(db.Subjects.ToList());
        }
        //搜索课程
        public ActionResult GetSearchCourses(string id = "")
        {
            List<Course> BookList;
            if (id == "")
            {
                BookList = db.Courses.OrderByDescending(r => r.ID).Take(10).ToList();
            }
            else
            {
                BookList = db.Courses.Where(r => r.Name.Contains(id)).OrderByDescending(r => r.ID).ToList();
            }
            return PartialView(BookList);
        }
        //按条件分类检索用
        public ActionResult GetSubjectCourses(int id = 1)
        {
            List<Course> BookList = db.Courses.Where(r => r.SBID == id).OrderByDescending(r => r.ID).ToList();
            return PartialView(BookList);
        }
        public ActionResult GetWeekdayCourses(int id = 1)
        {
            List<Course> BookList = db.Courses.Where(r => r.WDID == id).OrderByDescending(r => r.ID).ToList();
            return PartialView(BookList);
        }
        //显示主页，网站介绍
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        //显示联系我们
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        //预选课
        public ActionResult AddToShopCart(int id)
        {
            if (Session["CurrentUser"] == null)
            {
                return Content("亲，未登录时不能选课呢~");
            }
            else
            {
                ShopCart theCart = (Session["ShopCart"] == null) ? new ShopCart() : (ShopCart)Session["ShopCart"];
                Course theBook = db.Courses.Find(id);
                if (theBook != null)
                {
                    theCart.Add(new ShopCartItem(theBook));
                    Session["ShopCart"] = theCart;
                }
                return Content("已经将" + id.ToString() + "号课程加入预选课程！");
            }
        }
    }
}
