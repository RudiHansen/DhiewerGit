using Dhiewer_DataLibrary.Model;
using Dhiewer_DataLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dhiewer.Controllers
{
    // https://stackoverflow.com/questions/24896507/mvc-5-show-item-details-in-the-same-page-when-item-is-selected
    public class RSSMainController : Controller
    {
        // GET: RSSMain
        public ActionResult Index()
        {
            IRSSFeedRepository repository = new RSSFeedRepository();
            int totalPosts = 0;

            List<RSSFeed> rSSFeeds = repository.ReadAll();
            foreach (var feed in rSSFeeds)
            {
                totalPosts += feed.UnreadPosts;
            }
            rSSFeeds.Insert(0, new RSSFeed { Id = 0, Name = "Total", UnreadPosts = totalPosts });

            return View(rSSFeeds);
        }

        public ActionResult ListRSSPosts()
        {
            IRSSPostRepository repository = new RSSPostRepository();

            List<RSSPost> rSSPosts = repository.ReadUnread();

            return View(rSSPosts);
        }
        public ActionResult ListRSSFeeds()
        {
            IRSSFeedRepository repository = new RSSFeedRepository();
            int totalPosts = 0;

            List<RSSFeed> rSSFeeds = repository.ReadAll();
            foreach (var feed in rSSFeeds)
            {
                totalPosts += feed.UnreadPosts;
            }
            rSSFeeds.Insert(0, new RSSFeed { Id = 0, Name = "Total", UnreadPosts = totalPosts });

            return View(rSSFeeds);
        }

        public PartialViewResult Details(int id)
        {
            IRSSPostRepository repository = new RSSPostRepository();
            List<RSSPost> rSSPosts;

            if (id == 0)
            {
                rSSPosts = repository.ReadUnread();
            }
            else
            {
                rSSPosts = repository.ReadUnreadFromFeedId(id);
            }

            return PartialView("_details", rSSPosts);
        }

        [HttpPost]
        public ActionResult MarkRead(int Id)
        {
            IRSSPostRepository repository = new RSSPostRepository();

            repository.MarkRead(Id);
            return Content("OK");
        }

    }
}