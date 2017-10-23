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
        // GET: Cart
        public ActionResult Cart()
        {
            //if (Request.Cookies["id_user"] == null && Request.Cookies["Account"] == null)
            //{
            //    HttpCookie httpCookie = new HttpCookie("id_user");
            //    DateTime dt = DateTime.Now;
            //    httpCookie.Values.Add("user", "U" + dt.Ticks.ToString());
            //    Response.Cookies.Add(httpCookie);
            //}
            if (Request.Cookies["Account"] != null)
            {
                ViewBag.checklogin = "<span>Tài khoản: " + Request.Cookies["Account"].Values["user"] + " </span> &nbsp;| &nbsp;<a onclick='LoginUot()' href ='#'>Đăng xuất</a><p>Q.lí tài khoản & đơn hàng</p>";
            }
            else
            {
                ViewBag.checklogin = "<img style='float:left;' src='~/Image_System/dangnhap.png'/>&nbsp;<a class='dangnhap' href='#'>Đăng Nhập</a>&nbsp;|&nbsp;<a class='dangky' href='#'>Đăng Ký</a> <p>Q.lí tài khoản & đơn hàng</p>";
            }

            if (Request.Cookies["id_user"] != null)
            {
                CartModel cartModel = new CartModel();
                List<Cart> cartlist = new List<Cart>();
                String iduser = Request.Cookies["id_user"].Values["user"];
                cartlist = cartModel.getCartByIdUser(iduser);
                ViewBag.tongtien = cartModel.gettongtien(iduser);
                ViewBag.listCart = cartlist;

            }
            else
            {
                CartModel cartModel = new CartModel();
                List<Cart> cartlist = new List<Cart>();
                String iduser = Request.Cookies["Account"].Values["user"];
                cartlist = cartModel.getCartByIdUser(iduser);
                ViewBag.tongtien = cartModel.gettongtien(iduser);
                ViewBag.listCart = cartlist;
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
            CartModel cartModel = new CartModel();
            cartModel.updatecart(a, iduser, product);
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
            CartModel cartModel = new CartModel();
            cartModel.creatAndUpdate(iduser, product);
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
                CartModel cartModel = new CartModel();
                cartModel.DeletecartFromUserIdAndProductId(iduser, product);
            }
            else
            {
                iduser = Request.Cookies["Account"].Values["user"];
                CartModel cartModel = new CartModel();
                cartModel.DeletecartFromUserIdAndProductId(iduser, product);

            }           

            return View();
        }
        public Decimal Cart1()
        {
            CartModel cartModel = new CartModel();
            String iduser;
            if (Request.Cookies["id_user"] == null)
            {
                iduser = Request.Cookies["Account"].Values["user"];
            }
            else
            {
                iduser = Request.Cookies["id_user"].Values["user"];
            }
            return cartModel.gettongtien(iduser);
        }
    }
}