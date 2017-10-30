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
                ViewBag.checklogin = "<span>Tài khoản: " + Request.Cookies["Account"].Values["user"] + " </span> &nbsp;| &nbsp;<a onclick='LoginUot()' href ='#'>Đăng xuất</a><p>Q.lí tài khoản & đơn hàng</p>";
            }
            else
            {
                ViewBag.checklogin = "<img style='float:left;' src='~/Image_System/dangnhap.png'/>&nbsp;<a class='dangnhap' href='#'>Đăng Nhập</a>&nbsp;|&nbsp;<a class='dangky' href='#'>Đăng Ký</a> <p>Q.lí tài khoản & đơn hàng</p>";
            }
            CartModel cartModel = new CartModel();
            List<Cart> listcarrt;
            if (Request.Cookies["id_user"] != null)
            {
                listcarrt = cartModel.getCartByIdUser(Request.Cookies["id_user"].Values["user"]);
                ViewBag.tong_tien = cartModel.gettongtien(Request.Cookies["id_user"].Values["user"]);
            }
            else
            {
                listcarrt = cartModel.getCartByIdUser(Request.Cookies["Account"].Values["user"]);
                ViewBag.tong_tien = cartModel.gettongtien(Request.Cookies["Account"].Values["user"]);
            }


            ViewBag.cart_list = listcarrt;
            if (listcarrt.Count <= 0)
            {
                List<Cart> cartlist = new List<Cart>();
                String iduser = Request.Cookies["id_user"].Values["user"];
                cartlist = cartModel.getCartByIdUser(iduser);
                ViewBag.tongtien = cartModel.gettongtien(iduser);
                ViewBag.listCart = cartlist;
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
                CustomerModel customerModel = new CustomerModel();
                var id_customer = customerModel.GetIDCustomerByUser(User);
                CustomerAddressModel customerAddressModel = new CustomerAddressModel();
                CartModel cartModel = new CartModel();
                Decimal tongtien = cartModel.gettongtien(User);
                OrderModel orderModel = new OrderModel();
                orderModel.creatOrder(tongtien, id_customer, Int32.Parse(idAddressRequest));
                int idOrder = orderModel.GetIDOrderFromTotalBillIdCustomrAndCustomerAddress(tongtien, id_customer, Int32.Parse(idAddressRequest));
                List<RefProductOrder> list = cartModel.GetlistProductFromIdUser(User);
                RefProductOrdermodel refProductOrdermodel = new RefProductOrdermodel();
                foreach (var item in list)
                {
                    refProductOrdermodel.creatRefProductOrder(item, idOrder);
                }
                cartModel.Deletecart(User);
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
                        CustomerModel customerModel = new CustomerModel();
                        var id_customer = customerModel.GetIDCustomerByUser(User);
                        CustomerAddressModel customerAddressModel = new CustomerAddressModel();
                        customerAddressModel.creatCustomerAddressHaveEmail(email, nhahoten, diachi, sodienthoai, thanhpho, quanhuyen, id_customer);
                        var idAddress = customerAddressModel.GetIDCustomerAddressrTop1UniqueByIdCustomer(id_customer);
                        CartModel cartModel = new CartModel();
                        Decimal tongtien = cartModel.gettongtien(User);
                        OrderModel orderModel = new OrderModel();
                        orderModel.creatOrder(tongtien, id_customer, idAddress);
                        int idOrder = orderModel.GetIDOrderFromTotalBillIdCustomrAndCustomerAddress(tongtien, id_customer, idAddress);
                        List<RefProductOrder> list = cartModel.GetlistProductFromIdUser(User);
                        RefProductOrdermodel refProductOrdermodel = new RefProductOrdermodel();
                        foreach (var item in list)
                        {
                            refProductOrdermodel.creatRefProductOrder(item, idOrder);
                        }
                        cartModel.Deletecart(User);

                    }
                    else
                    {
                        var id = Request.Cookies["id_user"].Values["user"];
                        CustomerModel customerModel = new CustomerModel();
                        var id_customer = customerModel.GetIDCustomerByEmail(email);
                        if (id_customer < 0)
                        {
                            try
                            {
                                customerModel.creatCustomer(email, id, nhahoten);
                                id_customer = customerModel.GetIDCustomerByEmail(email);
                            }
                            catch (Exception)
                            {
                                id_customer = customerModel.GetIDCustomerByUser(id);

                            }
                           
                        }
                        CustomerAddressModel customerAddressModel = new CustomerAddressModel();
                        customerAddressModel.creatCustomerAddressHaveEmail(email, nhahoten, diachi, sodienthoai, thanhpho, quanhuyen, id_customer);
                        var id_customerAddress = customerAddressModel.GetIDCustomerAddressrUniqueByIdCustomer(id_customer);
                        CartModel cartModel = new CartModel();
                        Decimal tongtien = cartModel.gettongtien(id);
                        OrderModel orderModel = new OrderModel();
                        orderModel.creatOrder(tongtien, id_customer, id_customerAddress);
                        int idOrder = orderModel.GetIDOrderFromTotalBillIdCustomrAndCustomerAddress(tongtien, id_customer, id_customerAddress);
                        List<RefProductOrder> list = cartModel.GetlistProductFromIdUser(id);
                        RefProductOrdermodel refProductOrdermodel = new RefProductOrdermodel();
                        foreach (var item in list)
                        {
                            refProductOrdermodel.creatRefProductOrder(item, idOrder);
                        }
                        cartModel.Deletecart(id);
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
            CartModel cartModel = new CartModel();
            List<Cart> listcarrt;
            if (Request.Cookies["id_user"] != null)
            {
                listcarrt = cartModel.getCartByIdUser(Request.Cookies["id_user"].Values["user"]);
                ViewBag.tong_tien = cartModel.gettongtien(Request.Cookies["id_user"].Values["user"]);
            }
            else
            {
                var user = Request.Cookies["Account"].Values["user"];
                listcarrt = cartModel.getCartByIdUser(user);
                ViewBag.tong_tien = cartModel.gettongtien(user);
                CustomerAddressModel customerAddressModel = new CustomerAddressModel();
                CustomerModel customerModel = new CustomerModel();
                var IdCustomer = customerModel.GetIDCustomerByUser(user);
                ViewBag.AddressList = customerAddressModel.GetListAddressCustomerByCustomerId(IdCustomer);
            }


            ViewBag.cart_list = listcarrt;
            if (listcarrt.Count <= 0)
            {
                List<Cart> cartlist = new List<Cart>();
                String iduser;
                if (Request.Cookies["id_user"] != null)
                {
                    iduser = Request.Cookies["id_user"].Values["user"];
                }
                else
                {
                    iduser = Request.Cookies["Account"].Values["user"];
                }
                cartlist = cartModel.getCartByIdUser(iduser);
                ViewBag.tongtien = cartModel.gettongtien(iduser);
                ViewBag.listCart = cartlist;
                return View("~/Views/Cart/Cart.cshtml");
            }
            return View();
        }
        public int CheckEmail()
        {
            var email = Request.Form["email"];
            CustomerModel customerModel = new CustomerModel();
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
                int checkCoEmail = customerModel.GetIDCustomerByEmail(email);
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

            }
            else
            {
                return 2;
            }
            return 0;
        }
    }
}