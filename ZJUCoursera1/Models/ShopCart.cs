using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZJUCoursera1.DAL;

namespace ZJUCoursera1.Models
{
    /*************************************************************/
    [Serializable]
    public class ShopCartItem
    {
        public Course theBook { get; set; }
        public int Quantity { get; set; }

        public ShopCartItem(Course book, int quantity = 1)
        {
            this.theBook = book;
            this.Quantity = quantity;
        }

    }

    /*************************************************************/
    [Serializable]
    public class ShopCart
    {
        private List<ShopCartItem> ItemList = new List<ShopCartItem>();
        public int Count { get { return ItemList.Count; } }

        public ShopCart()
        {
        }

        public List<ShopCartItem> ToList() { return ItemList; }

        public ShopCartItem this[int index]
        {
            get { return ItemList[index]; }
            set { ItemList[index] = value; }
        }

        public void Add(ShopCartItem item)
        {
            ShopCartItem temp = ItemList.SingleOrDefault(r => r.theBook.ID == item.theBook.ID);
            if (temp == null)
            {
                ItemList.Add(item);
            }
        }
        public void Remove(ShopCartItem item)
        {
            ItemList.Remove(item);
        }
        public string RemoveByID(int id)
        {
            ShopCartItem item = ItemList.SingleOrDefault(r => r.theBook.ID == id);
            if (item != null)
            {
                ItemList.Remove(item);
                return "已经从购物车中删除指定的书！";
            }
            else
            {
                return "选择的书本身就不在购物车内！";
            }
        }

        public string UpdateQuantity(int bookId, int newQuantity)
        {
            ShopCartItem item = ItemList.SingleOrDefault(r => r.theBook.ID == bookId);
            if (item == null)
            {
                using (ZJUSCDB db = new ZJUSCDB())
                {
                    Course book = db.Courses.Find(bookId);
                    if (book != null)
                    {
                        ItemList.Add(new ShopCartItem(book, newQuantity));
                        return "选购的书不在购物车内，已加入购物车！";
                    }
                    else
                    {
                        return "不存在选购的书！";
                    }
                }
            }
            else
            {
                item.Quantity = newQuantity;
                return "数量已修改！";
            }
        }

    }
}