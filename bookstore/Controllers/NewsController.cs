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
        // GET: News
        public ActionResult TinTuc()
        {
            NewsModel model = new NewsModel();
            ViewBag.interestnews = model.GetListNews(3);
            ViewBag.listallnews = model.GetListNews(2);
            return View();
        }

        public ActionResult chitiettintuc(string id_news)
        {
            NewsModel model = new NewsModel();
            ViewBag.interestnews = model.GetListNews(3);
            ViewBag.newsdetails = model.GetNewsByID(id_news);
            ViewBag.interestnews = model.GetListNews(3);
            ViewBag.othernews = model.GetListNews(4);
            return View();
        }
    }
}