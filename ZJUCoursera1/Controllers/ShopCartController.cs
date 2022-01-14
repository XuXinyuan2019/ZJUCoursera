using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZJUCoursera1.DAL;
using ZJUCoursera1.Models;
using ZJUCoursera1.Controllers;
using System.Net;

namespace BookStoreMVC5.Controllers
{
    public class ShopCartController : Controller
    {
        //BookStoreEntities db = new BookStoreEntities();
        private ZJUSCDB db = new ZJUSCDB();
        ShopCart theCart;
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            theCart = (Session["ShopCart"] == null) ? new ShopCart() : (ShopCart)Session["ShopCart"];
        }

        // GET: ShopCart
        public ActionResult Index()
        {
            return View(theCart.ToList());
        }
        [HttpPost]
        public ActionResult UpdateQuantity(int bookId, int newQuantity)
        {
            string Result = theCart.UpdateQuantity(bookId, newQuantity);
            return Content(Result);
        }
        [HttpPost]
        public ActionResult DeleteBook(int bookId)
        {
            string Result = theCart.RemoveByID(bookId);
            return Content(Result);
        }
        //确认预选课程
        public ActionResult TurnToOrder()
        {
            Student theUser = (Session["CurrentUser"] == null) ? null : ((Student)(Session["CurrentUser"]));
            if (theCart.Count > 0 && theUser != null)
            {
                int theOrderID = GenerateOrder(theCart, theUser);
                return View();
            }
            return Content("购物车中没选择的书！请后退");
        }
        #region private method
        private int GenerateOrder(ShopCart theCart, Student theUser)
        {
            int theOrderId = theUser.ID;
            for (int i = 0; i < theCart.Count; i++)
            {
                SC aItem = new SC();
                aItem.SID = theOrderId;
                aItem.CID = theCart[i].theBook.ID;
                aItem.SelectAt = DateTime.Now;
                try
                {
                    db.SCs.Add(aItem);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    System.Console.WriteLine("传递过来的异常值为：{0}", e);
                }
            }
            return theOrderId;
        }
        #endregion
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

    }
}