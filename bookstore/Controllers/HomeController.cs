using bookstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace bookstore.Controllers
{
    public class HomeController : Controller
    {
        localhost.Service s = new localhost.Service();

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index()
        {
            if (Request.Cookies["id_user"] == null && Request.Cookies["Account"] == null)
            {
                HttpCookie httpCookie = new HttpCookie("id_user");
                DateTime dt = DateTime.Now;
                httpCookie.Values.Add("user", "U" + dt.Ticks.ToString());
                httpCookie.Expires = DateTime.Now.AddDays(20);
                Response.Cookies.Add(httpCookie);
            }

            ViewBag.hot_books = s.GetBooks("TOP 6", "WHERE [_id_category] =1");
            ViewBag.new_books = s.GetBooks("TOP 6", "WHERE [_id_category] = 2");
            ViewBag.sale_books = s.GetBooks("TOP 6", "WHERE [_id_category] = 3");
            ViewBag.authors = s.GetAuthors();
            ViewBag.slider = s.GetBooksForSlider();

            ViewBag.listnews = s.GetListNews(1);
            if (Request.Cookies["Account"] != null)
            {
                ViewBag.checklogin = "<span>Tài khoản: " + Request.Cookies["Account"].Values["user"] + " </span> &nbsp;| &nbsp;<a onclick='LoginUot()' href ='#'>Đăng xuất</a><p>Q.lí tài khoản & đơn hàng</p>";
            }
            else
            {
                ViewBag.checklogin = "<img style='float:left;' src='~/Image_System/dangnhap.png'/>&nbsp;<a class='dangnhap' href='#'>Đăng Nhập</a>&nbsp;|&nbsp;<a class='dangky' href='#'>Đăng Ký</a> <p>Q.lí tài khoản & đơn hàng</p>";
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            if (Request.Cookies["Account"] != null)
            {
                ViewBag.checklogin = "<span>Tài khoản: " + Request.Cookies["Account"].Values["user"] + " </span> &nbsp;| &nbsp;<a onclick='LoginUot()' href ='#'>Đăng xuất</a><p>Q.lí tài khoản & đơn hàng</p>";
            }
            else
            {
                ViewBag.checklogin = "<img style='float:left;' src='~/Image_System/dangnhap.png'/>&nbsp;<a class='dangnhap' href='#'>Đăng Nhập</a>&nbsp;|&nbsp;<a class='dangky' href='#'>Đăng Ký</a> <p>Q.lí tài khoản & đơn hàng</p>";
            }
            ViewBag.Message = "Your contact page.";

            return View("Contact");
        }

        public Boolean SendEmail()
        {
            try
            {
                var hoten = Request.Form["hoten"];
                var noidung = Request.Form["noidung"];
                var tieude = Request.Form["tieude"];
                var email = Request.Form["email"];
                var sdt = Request.Form["sdt"];

                s.SendMail_Contact(hoten, email, sdt, tieude, noidung);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DangNhapCheck()
        {
            String account = Request.Form["username"];
            String password = Request.Form["password"];
           
            if (s.LoginWithAccAndPass(account, password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpPost]
        public ActionResult DangNhap()
        {
            String account = Request.Form["username"];
            String password = Request.Form["password"];
           
            if (s.LoginWithAccAndPass(account, password))
            {
                var UserFromEmail = s.GetuserByEmail(account);
                if (UserFromEmail != null)
                {
                    account = UserFromEmail;
                }
                HttpCookie httpCookie = new HttpCookie("Account");
                httpCookie.Values.Add("user", account);
                httpCookie.Values.Add("password", password.GetHashCode().ToString());
                httpCookie.Expires = DateTime.Now.AddDays(20);
                Response.Cookies.Add(httpCookie);
                Session["Account"] = Request.Cookies["Account"].Value;
               
                ViewBag.hot_books = s.GetBooks("TOP 6", "WHERE [_id_category] =1");
                ViewBag.new_books = s.GetBooks("TOP 6", "WHERE [_id_category] = 2");
                ViewBag.sale_books = s.GetBooks("TOP 6", "WHERE [_id_category] = 3");
               
                ViewBag.authors = s.GetAuthors();
                ViewBag.slider = s.GetBooksForSlider();
               
                ViewBag.listnews = s.GetListNews(1);
                ViewBag.checklogin = "<span>Tài khoản: " + Request.Cookies["Account"].Values["user"] + " </span> &nbsp;| &nbsp;<a onclick='LoginUot()' href ='#'>Đăng xuất</a><p>Q.lí tài khoản & đơn hàng</p>";
                
                if (Request.Cookies["id_user"] != null)
                {
                    String iduser = Request.Cookies["id_user"].Values["user"];
                    s.UpdateIdserFromIdCustomer(iduser, account);
                }

                HttpCookie httpCookie1 = new HttpCookie("id_user");
                DateTime dt = DateTime.Now;
                httpCookie1.Values.Add("user", "U" + dt.Ticks.ToString());
                httpCookie1.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(httpCookie1);
                return View("Index");

            }
            else
            {
                ViewBag.checklogin = "<img style='float:left;' src='~/Image_System/dangnhap.png'/>&nbsp;<a class='dangnhap' href='#'>Đăng Nhập</a>&nbsp;|&nbsp;<a class='dangky' href='#'>Đăng Ký</a> <p>Q.lí tài khoản & đơn hàng</p>";
                ViewBag.login_false = "Sai tài khoản hoặc mật khẩu!";
            }

            return View("DangNhap");
        }


        public ActionResult logout()
        {
            HttpCookie httpCookie = new HttpCookie("Account");
            httpCookie.Values.Add("user", "34");
            httpCookie.Values.Add("password", "43");
            httpCookie.Expires = DateTime.Now.AddDays(-2);
            Response.Cookies.Add(httpCookie);

            HttpCookie httpCookie1 = new HttpCookie("id_user");
            DateTime dt = DateTime.Now;
            httpCookie1.Values.Add("user", "U" + dt.Ticks.ToString());
            httpCookie1.Expires = DateTime.Now.AddDays(20);
            Response.Cookies.Add(httpCookie1);

            ViewBag.hot_books = s.GetBooks("TOP 6", "WHERE [_id_category] =1");
            ViewBag.new_books = s.GetBooks("TOP 6", "WHERE [_id_category] = 2");
            ViewBag.sale_books = s.GetBooks("TOP 6", "WHERE [_id_category] = 3");
            ViewBag.authors = s.GetAuthors();
            ViewBag.slider = s.GetBooksForSlider();
           
            ViewBag.listnews = s.GetListNews(1);
            ViewBag.checklogin = "<img style='float:left;' src='~/Image_System/dangnhap.png'/>&nbsp;<a class='dangnhap' href='#'>Đăng Nhập</a>&nbsp;|&nbsp;<a class='dangky' href='#'>Đăng Ký</a> <p>Q.lí tài khoản & đơn hàng</p>";
            return View("Index");
        }
        public int Signin()
        {
            var name = Request.Form["name"];
            var email = Request.Form["email"];
            var username = Request.Form["username"];
            var password = Request.Form["password"];
           
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
         @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
         @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(email))
            {
                int checkCoEmail = s.GetIDCustomerByEmail(email);
                if (checkCoEmail >= 0)
                {
                    return 3;// đã có email
                }
                else
                {
                    if (s.GetIDCustomerByUser(username) >= 0)
                    {
                        return 1;// đã có user này rồi.
                    }
                    else
                    {
                        s.Signin(email, username, password, name);
                    }
                }
            }
            else
            {
                return 2;//lỗi email sai
            }

            return 0;
        }
    }
}