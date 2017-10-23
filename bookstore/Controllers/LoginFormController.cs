using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bookstore.Controllers
{
    public class LoginFormController : Controller
    {
        // GET: LoginForm
        public ActionResult LoginForm()
        {
            return View();
        }
    }
}