using bookstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bookstore.Controllers
{
    public class Book_DetailsController : Controller
    {

        localhost.Service s = new localhost.Service();
        // GET: Book_Details
        public ActionResult BookDetails(string id)
        {
            if (Request.Cookies["Account"] != null)
            {
                ViewBag.user = Request.Cookies["Account"].Values["user"];
                ViewBag.checklogin = "<span>Tài khoản: " + Request.Cookies["Account"].Values["user"] + " </span> &nbsp;| &nbsp;<a onclick='LoginUot()' href ='#'>Đăng xuất</a><p>Q.lí tài khoản & đơn hàng</p>";
                ViewBag.checkcomment = true;
            }
            else
            {
                ViewBag.checklogin = "<img style='float:left;' src='~/Image_System/dangnhap.png'/>&nbsp;<a class='dangnhap' href='#'>Đăng Nhập</a>&nbsp;|&nbsp;<a class='dangky' href='#'>Đăng Ký</a> <p>Q.lí tài khoản & đơn hàng</p>";
                ViewBag.checkcomment = false;
            }
           
            ViewBag.book_detail = s.GetBookByID(id);
            ViewBag.reviews = s.GetReviews(id);
            ViewBag.point = s.GetRating(id);
            
            return View();
        }

        public bool Comment()
        {
            try
            {
                var id = Request.Form["id"];
                var rate = Request.Form["rate"];
                var comment = Request.Form["comment"];
                var user = Request.Cookies["Account"].Values["user"];
                
                s.Comment_Book(id, int.Parse(rate), comment,user);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}