using _1611061593_lab3.common;
using _1611061593_lab3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace _1611061593_lab3.Controllers
{
    public class BlogController : Controller
    {
        ThucHanhTMDTEntities db = new ThucHanhTMDTEntities();
        public ActionResult PostFeed(string type)
        {
            Category category = db.Categories.Where(s => s.Alias.Contains(type)).FirstOrDefault();
            if (category == null)
            {
                return HttpNotFound();
            }

            IEnumerable<Article> posts = (from s in db.Articles where s.Category.Alias.Contains(type) select s).ToList();

            var feed = new SyndicationFeed(category.Name, "RSS Feed",
                       new Uri("https://vnexpress.net/RSS"),
                       Guid.NewGuid().ToString(),
                       DateTime.Now);
            var items = new List<SyndicationItem>();
            foreach (Article art in posts)
            {
                string postUri = String.Format("https://vnexpress.net/" + art.Alias + "-{0}", art.Id);
                var item =
                    new SyndicationItem(Helper.RemoveIllegalCharacters(art.Title),
                                        Helper.RemoveIllegalCharacters(art.Description),
                                        new Uri(postUri),
                                        art.Id.ToString(),
                                        art.PublishedDate.Value);
                items.Add(item);
            }

            feed.Items = items;
            RSSActionResult rSSAction = new RSSActionResult(feed);
            return rSSAction;
        }
        public ActionResult ReadRSS()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ReadRSS(string url)
        {
            WebClient webClient = new WebClient();
            webClient.Encoding = ASCIIEncoding.UTF8;
            string RSSData = webClient.DownloadString(url);

            XDocument xml = XDocument.Parse(RSSData, LoadOptions.PreserveWhitespace);
            var RSSFeedData = (from x in xml.Descendants("item")
                               select new RSSFeed
                               {
                                   Title = ((string)x.Element("title")),
                                   Link = ((string)x.Element("link")),
                                   Description = ((string)x.Element("description")),
                                   PubDate = ((string)x.Element("pubDate"))
                               });
            ViewBag.RSSFeed = RSSFeedData;
            ViewBag.URL = url;
            return View();
        }
    }

}