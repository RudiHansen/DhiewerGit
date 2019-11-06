using Dhiewer_DataLibrary.Model;
using Dhiewer_DataLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dhiewer.Controllers
{
    public class RSSMainController : Controller
    {
        // GET: RSSMain
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListRSSPosts()
        {
            IRSSPostRepository repository = new RSSPostRepository();

            List<RSSPost> rSSPosts = repository.ReadUnread();

            return View(rSSPosts);
        }
    }
}