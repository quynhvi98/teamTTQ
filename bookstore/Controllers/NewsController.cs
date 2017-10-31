using bookstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bookstore.Controllers
{
    public class NewsController : Controller
    {
        localhost.Service s = new localhost.Service();
        // GET: News
        public ActionResult TinTuc()
        {
         
            ViewBag.interestnews = s.GetListNews(3);
            ViewBag.listallnews = s.GetListNews(2);
            return View();
        }

        public ActionResult chitiettintuc(string id_news)
        {
            
            ViewBag.newsdetails = s.GetNewsByID(id_news);
            ViewBag.interestnews = s.GetListNews(3);
            ViewBag.othernews = s.GetListNews(4);
            return View();
        }
    }
}