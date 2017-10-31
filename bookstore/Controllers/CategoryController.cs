using bookstore.Models;
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
        public ActionResult Category(string name_type)
        {
            if (Request.Cookies["Account"] != null)
            {
                ViewBag.checklogin = "<span>Tài khoản: " + Request.Cookies["Account"].Values["user"] + " </span> &nbsp;| &nbsp;<a onclick='LoginUot()' href ='#'>Đăng xuất</a><p>Q.lí tài khoản & đơn hàng</p>";
            }
            else
            {
                ViewBag.checklogin = "<img style='float:left;' src='~/Image_System/dangnhap.png'/>&nbsp;<a class='dangnhap' href='#'>Đăng Nhập</a>&nbsp;|&nbsp;<a class='dangky' href='#'>Đăng Ký</a> <p>Q.lí tài khoản & đơn hàng</p>";
            }
            ViewBag.name_type = name_type;
           
            ViewBag.ListBooks = s.GetBookByCategory(name_type);      
            return View();
        }
    }
}