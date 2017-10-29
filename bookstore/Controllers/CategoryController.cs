using bookstore.Models;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bookstore.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Category(string name_type,int? page)
        {
            if (Request.Cookies["Account"] != null)
            {
                ViewBag.checklogin = "<span>Tài khoản: " + Request.Cookies["Account"].Values["user"] + " </span> &nbsp;| &nbsp;<a onclick='LoginUot()' href ='#'>Đăng xuất</a><p>Q.lí tài khoản & đơn hàng</p>";
            }
            else
            {
                ViewBag.checklogin = "<img style='float:left;' src='~/Image_System/dangnhap.png'/>&nbsp;<a class='dangnhap' href='#'>Đăng Nhập</a>&nbsp;|&nbsp;<a class='dangky' href='#'>Đăng Ký</a> <p>Q.lí tài khoản & đơn hàng</p>";
            }
            BookModel model = new BookModel();
            ViewBag.name_type = name_type;
            CategoryModel cm = new CategoryModel();
            List<Book> booklist = cm.GetBookByCategory(name_type);
            ViewBag.ListBooks = booklist;
            ViewBag.slider = model.GetBooksForSlider();
            NewsModel new_model = new NewsModel();
           
            List < News > newlist= new_model.GetListNews(1);
            ViewBag.listnews = newlist;
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            return View(booklist.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult GetCategory()
        {
            var nhaptrang = Request.Form["nhaptrang"];
            CategoryModel cm = new CategoryModel();
            ViewBag.ListBooks = cm.GetBookByCategory("Tiểu thuyết");
            return View();
        }

    }
}