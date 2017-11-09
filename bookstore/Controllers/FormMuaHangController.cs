using bookstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace bookstore.Controllers
{

    public class FormMuaHangController : Controller
    {
        localhost.Service s = new localhost.Service();
        // GET: FormMuaHang
        public ActionResult FormMuaHang()
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
                ViewBag.checklogin = "<a href='#'>Tài khoản: " + Request.Cookies["Account"].Values["user"] + " </a> &nbsp;| &nbsp;<a onclick='LoginUot()' href ='#'>Đăng xuất</a><p>Q.lí tài khoản & đơn hàng</p>";
            }
            else
            {
                ViewBag.checklogin = "<img style='float:left;' src='~/Image_System/dangnhap.png'/>&nbsp;<a class='dangnhap' href='#'>Đăng Nhập</a>&nbsp;|&nbsp;<a class='dangky' href='#'>Đăng Ký</a> <p>Q.lí tài khoản & đơn hàng</p>";
            }
         
          
            if (Request.Cookies["id_user"] != null)
            {
                ViewBag.cart_list = s.getCartByIdUser(Request.Cookies["id_user"].Values["user"]);
                ViewBag.tong_tien = s.gettongtien(Request.Cookies["id_user"].Values["user"]);
            }
            else
            {
                ViewBag.cart_list = s.getCartByIdUser(Request.Cookies["Account"].Values["user"]);
                ViewBag.tong_tien = s.gettongtien(Request.Cookies["Account"].Values["user"]);
            }


            
            if (ViewBag.cart_list.Length <= 0)
            {
               
                String iduser = Request.Cookies["id_user"].Values["user"];
                
                ViewBag.tongtien = s.gettongtien(iduser);
                ViewBag.listCart = s.getCartByIdUser(iduser); ;
                return View("~/Views/Cart/Cart.cshtml");
            }
            return View();
        }
        [HttpPost]
        public ActionResult CreatOrderFromCart()
        {
            var idAddressRequest = Request.Form["idAddress"];
            if (idAddressRequest != null)
            {
                var User = Request.Cookies["Account"].Values["user"];
              
                var id_customer = s.GetIDCustomerByUser(User);
              
                Decimal tongtien = s.gettongtien(User);
              
                s.creatOrder(double.Parse(tongtien.ToString()), id_customer, Int32.Parse(idAddressRequest));
                int idOrder = s.GetIDOrderFromTotalBillIdCustomrAndCustomerAddress(double.Parse(tongtien.ToString()), id_customer, Int32.Parse(idAddressRequest));
             
         
                foreach (var item in s.GetlistProductFromIdUser(User))
                {
                    s.creatRefProductOrder(item, idOrder);
                }
                s.Deletecart(User);
            }
            else
            {
                var nhahoten = Request.Form["nhahoten"];
                if (nhahoten != null)
                {
                    var email = Request.Form["email"];
                    var sodienthoai = Request.Form["sodienthoai"];
                    var thanhpho = Request.Form["thanhpho"];
                    var quanhuyen = Request.Form["quanhuyen"];
                    var diachi = Request.Form["diachi"];
                    if (Request.Cookies["id_user"] == null)
                    {
                        // đăng nhập rồi.
                        var User = Request.Cookies["Account"].Values["user"];
                       
                        var id_customer = s.GetIDCustomerByUser(User);
                       
                        s.creatCustomerAddressHaveEmail(email, nhahoten, diachi, sodienthoai, thanhpho, quanhuyen, id_customer);
                        var idAddress = s.GetIDCustomerAddressrTop1UniqueByIdCustomer(id_customer);          
                        Decimal tongtien = s.gettongtien(User);                  
                        s.creatOrder(double.Parse(tongtien.ToString()), id_customer, idAddress);
                        int idOrder = s.GetIDOrderFromTotalBillIdCustomrAndCustomerAddress(double.Parse(tongtien.ToString()), id_customer, idAddress);
                        foreach (var item in s.GetlistProductFromIdUser(User))
                        {
                            s.creatRefProductOrder(item, idOrder);
                        }
                        s.Deletecart(User);
                    }
                    else
                    {
                        var id = Request.Cookies["id_user"].Values["user"];
                       
                        var id_customer = s.GetIDCustomerByEmail(email);
                        if (id_customer < 0)
                        {
                            try
                            {
                                s.creatCustomer(email, id, nhahoten);
                                id_customer = s.GetIDCustomerByEmail(email);
                            }
                            catch (Exception)
                            {
                                id_customer = s.GetIDCustomerByUser(id);

                            }
                           
                        }
                       
                        s.creatCustomerAddressHaveEmail(email, nhahoten, diachi, sodienthoai, thanhpho, quanhuyen, id_customer);
                        var id_customerAddress = s.GetIDCustomerAddressrUniqueByIdCustomer(id_customer);
                      
                        Decimal tongtien = s.gettongtien(id);
                       
                        s.creatOrder(double.Parse(tongtien.ToString()), id_customer, id_customerAddress);
                        int idOrder = s.GetIDOrderFromTotalBillIdCustomrAndCustomerAddress(double.Parse(tongtien.ToString()), id_customer, id_customerAddress);
                     
                      
                        foreach (var item in s.GetlistProductFromIdUser(id))
                        {
                            s.creatRefProductOrder(item, idOrder);
                        }
                        s.Deletecart(id);
                    }
                }
                else
                {
                    return View("FormMuaHang", "FormMuaHang");
                }
            }
            

            return View("FormMuaHang", "FormMuaHang");
        }
        public ActionResult Address()
        {

            if (Request.Cookies["Account"] != null)
            {
                ViewBag.checklogin = "<span>Tài khoản: " + Request.Cookies["Account"].Values["user"] + " </span> &nbsp;| &nbsp;<a onclick='LoginUot()' href ='#'>Đăng xuất</a><p>Q.lí tài khoản & đơn hàng</p>";
            }
            else
            {
                ViewBag.checklogin = "<img style='float:left;' src='~/Image_System/dangnhap.png'/>&nbsp;<a class='dangnhap' href='#'>Đăng Nhập</a>&nbsp;|&nbsp;<a class='dangky' href='#'>Đăng Ký</a> <p>Q.lí tài khoản & đơn hàng</p>";
                Response.Redirect("~/Home");
            }

            localhost.Cart[] listcarrt;
            if (Request.Cookies["id_user"] != null)
            {
                listcarrt = s.getCartByIdUser(Request.Cookies["id_user"].Values["user"]);
                ViewBag.tong_tien = s.gettongtien(Request.Cookies["id_user"].Values["user"]);
            }
            else
            {
                var user = Request.Cookies["Account"].Values["user"];
                listcarrt = s.getCartByIdUser(user);
                ViewBag.tong_tien = s.gettongtien(user);
                var IdCustomer = s.GetIDCustomerByUser(user);
                ViewBag.AddressList = s.GetListAddressCustomerByCustomerId(IdCustomer);
            }


            ViewBag.cart_list = listcarrt;
            if (listcarrt.Length <= 0)
            {
                localhost.Cart[] cartlist;
                String iduser;
                if (Request.Cookies["id_user"] != null)
                {
                    iduser = Request.Cookies["id_user"].Values["user"];
                }
                else
                {
                    iduser = Request.Cookies["Account"].Values["user"];
                }
                cartlist = s.getCartByIdUser(iduser);
                ViewBag.tongtien = s.gettongtien(iduser);
                ViewBag.listCart = cartlist;
                return View("~/Views/Cart/Cart.cshtml");
            }
            return View();
        }
        public int CheckEmail()
        {
            var email = Request.Form["email"];
            
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
         @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
         @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(email))
            {

            }
            else
            {
                return 2;//lỗi email sai
            }

            if (Request.Cookies["id_user"] != null)
            {
                int checkCoEmail = s.GetIDCustomerByEmail(email);
                if (checkCoEmail >= 0)
                {
                    return 3;// đã có email
                }
            }
            else
            {

            }
            return 0;
        }
        public int CheckPhone()
        {
            var sodienthoai = Request.Form["sodienthoai"];
            string strRegex = "^(([(]?(\\d{2,4})[)]?)|(\\d{2,4})|([+1-9]+\\d{1,2}))?[-\\s]?(\\d{2,3})?[-\\s]?((\\d{7,8})|(\\d{3,4}[-\\s]\\d{3,4}))$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(sodienthoai))
            {
                return 0;
            }
            else
            {
                return 2;
            }
            
        }

        public Boolean MailToCus()
        {
            try
            {
                var email = Request.Form["email"];
            
                s.MailToCustomer(email);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}