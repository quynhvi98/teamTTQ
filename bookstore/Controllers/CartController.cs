using bookstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bookstore.Controllers
{
    public class CartController : Controller
    {
           localhost.Service s = new localhost.Service();
        // GET: Cart
        public ActionResult Cart()
        {
            if (Request.Cookies["id_user"] == null && Request.Cookies["Account"] == null)
            {
                HttpCookie httpCookie = new HttpCookie("id_user");
                DateTime dt = DateTime.Now;
                httpCookie.Values.Add("user", "U" + dt.Ticks.ToString());
                Response.Cookies.Add(httpCookie);
            }
            if (Request.Cookies["Account"] != null)
            {
                ViewBag.checklogin = "<a href='#'>Tài khoản: " + Request.Cookies["Account"].Values["user"] + " </a> &nbsp;| &nbsp;<a onclick='LoginUot()' href ='#'>Đăng xuất</a><p>Q.lí tài khoản & đơn hàng</p>";
            }
            else
            {
                ViewBag.checklogin = "<img style='float:left;' src='~/Image_System/dangnhap.png'/>&nbsp;<a class='dangnhap' href='#'>Đăng Nhập</a>&nbsp;|&nbsp;<a class='dangky' href='#'>Đăng Ký</a> <p>Q.lí tài khoản & đơn hàng</p>";
            }

            if (Request.Cookies["id_user"] != null)
            {
          
                string iduser = Request.Cookies["id_user"].Values["user"];            
                ViewBag.tongtien = s.gettongtien(iduser);
                ViewBag.listCart = s.getCartByIdUser(iduser);
            }
            else
            {
                string iduser = Request.Cookies["Account"].Values["user"];
                ViewBag.tongtien = s.gettongtien(iduser);
                ViewBag.listCart = s.getCartByIdUser(iduser);
            }
            return View();
        }
        [HttpPost]
        public void UpdateData()
        {
            var a = Request.Form["quantity"];
            var product = Request.Form["product"];
            String iduser;
            if (Request.Cookies["id_user"] == null)
            {
                iduser = Request.Cookies["Account"].Values["user"];
            }
            else
            {
                iduser = Request.Cookies["id_user"].Values["user"];
            }
        
            s.updatecart(a, iduser, product);
        }
        [HttpPost]
        public ActionResult CreatDataCart()
        {
            var product = Request.Form["product"];
            String iduser;
            if (Request.Cookies["id_user"]== null)
            {
                iduser = Request.Cookies["Account"].Values["user"];
            }
            else
            {
                iduser = Request.Cookies["id_user"].Values["user"];
            }
            
            s.creatAndUpdate(iduser, product);
            return View();
        }
        [HttpPost]
        public ActionResult deleteDataCart()
        {
            var product = Request.Form["product"];
            String iduser;
            if (Request.Cookies["id_user"] != null)
            {
                iduser = Request.Cookies["id_user"].Values["user"];
             
                s.DeletecartFromUserIdAndProductId(iduser, product);
            }
            else
            {
                iduser = Request.Cookies["Account"].Values["user"];
             
                s.DeletecartFromUserIdAndProductId(iduser, product);

            }           

            return View();
        }
        public Decimal Cart1()
        {
           
            String iduser;
            if (Request.Cookies["id_user"] == null)
            {
                iduser = Request.Cookies["Account"].Values["user"];
            }
            else
            {
                iduser = Request.Cookies["id_user"].Values["user"];
            }
            return s.gettongtien(iduser);
        }
    }
}