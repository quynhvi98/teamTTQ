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
        localhost.Service s = new localhost.Service();
        // GET: Category
        public ActionResult Category(string name_type,int? page)
        {
            if (Request.Cookies["Account"] != null)
            {
                ViewBag.checklogin = "<a href='#'>Tài khoản: " + Request.Cookies["Account"].Values["user"] + " </a> &nbsp;| &nbsp;<a onclick='LoginUot()' href ='#'>Đăng xuất</a><p>Q.lí tài khoản & đơn hàng</p>";
            }
            else
            {
                ViewBag.checklogin = "<img style='float:left;' src='~/Image_System/dangnhap.png'/>&nbsp;<a class='dangnhap' href='#'>Đăng Nhập</a>&nbsp;|&nbsp;<a class='dangky' href='#'>Đăng Ký</a> <p>Q.lí tài khoản & đơn hàng</p>";
            }

            ViewBag.name_type = name_type;
           
            List<localhost.Book> booklist = new List<localhost.Book>();
            foreach(var item in s.GetBookByCategory(name_type))
            {
                booklist.Add(item);
            }
            ViewBag.ListBooks = booklist;
            ViewBag.slider = s.GetBooksForSlider();
            ViewBag.listnews = s.GetListNews(1);
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(booklist.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult GetCategory()
        {
            var nhaptrang = Request.Form["nhaptrang"];
        
            ViewBag.ListBooks = s.GetBookByCategory("Tiểu thuyết");
            return View();
        }

    }
}