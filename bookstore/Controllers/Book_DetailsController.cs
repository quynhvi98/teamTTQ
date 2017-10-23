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
        // GET: Book_Details
        public ActionResult BookDetails(string id)
        {
            if (Request.Cookies["Account"] != null)
            {
                ViewBag.checklogin = "<span>Tài khoản: " + Request.Cookies["Account"].Values["user"] + " </span> &nbsp;| &nbsp;<a onclick='LoginUot()' href ='#'>Đăng xuất</a><p>Q.lí tài khoản & đơn hàng</p>";
            }
            else
            {
                ViewBag.checklogin = "<img style='float:left;' src='~/Image_System/dangnhap.png'/>&nbsp;<a class='dangnhap' href='#'>Đăng Nhập</a>&nbsp;|&nbsp;<a class='dangky' href='#'>Đăng Ký</a> <p>Q.lí tài khoản & đơn hàng</p>";
            }
            BookModel book_model = new BookModel();
            ReviewModel review_model = new ReviewModel();
            ViewBag.book_detail = book_model.GetBookByID(id);
            ViewBag.reviews = review_model.GetReviews(id);
            ViewBag.point = book_model.GetRating(id)*2;
            return View();
        }
    }
}