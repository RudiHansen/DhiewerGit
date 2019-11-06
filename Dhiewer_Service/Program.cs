using Dhiewer_DataLibrary.Model;
using Dhiewer_DataLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Dhiewer_Service
{
    class Program
    {
        static void Main()
        {
            bool InitRSSFeeds = false;

            if (InitRSSFeeds)
            {
                CreateRSSFeeds();
            }

            List<RSSFeed> RSSFeedList = GetRSSFeeds();
            List<RSSPost> RSSPostList = new List<RSSPost>();

            Console.WriteLine("Reading feeds from RRS Feeds.");
            foreach (var item in RSSFeedList)
            {
                RSSPostList.AddRange(GetRSSPostsFromFeed(item));
            }

            Console.WriteLine("Saving feeds to SQL.");
            SaveRSSPostList(RSSPostList);

            Console.WriteLine("Press key to continue.");
            Console.ReadKey();
        }

        private static void SaveRSSPostList(List<RSSPost> rSSPostList)
        {
            IRSSPostRepository repository = new RSSPostRepository();

            foreach (var RSSPost in rSSPostList)
            {
                var existing = repository.GetByFeedRefIdSubject(RSSPost);
                if(existing == null)
                {
                    if (RSSPost.Validate())
                    {
                        Console.WriteLine($"Saving RSSPost \"{RSSPost.Subject.Substring(0,Math.Min(RSSPost.Subject.Length,70))}\"");
                        repository.Add(RSSPost);
                    }
                }
                else
                {
                    if(RSSPost.LastUpdated > existing.LastUpdated)
                    {
                        Console.WriteLine($"RSSPost {RSSPost.Id} was updated.");
                        repository.Update(RSSPost);
                    }
                    if (RSSPost.LastUpdated < existing.LastUpdated)
                    {
                        // Not sure what to do here.
                        Console.WriteLine($"RSSPost {RSSPost.Id} was updated. (But LastUpdated date seems wrong!");
                    }
                    if (RSSPost.Published != existing.Published)
                    {
                        // Not sure what to do here.
                        Console.WriteLine($"RSSPost {RSSPost.Id} with changed Published date found!");
                    }
                }

            }
        }

        private static List<RSSPost> GetRSSPostsFromFeed(RSSFeed rSSFeed)
        {
            List<RSSPost> output = new List<RSSPost>();

            XmlReader reader = XmlReader.Create(rSSFeed.URL);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();
            foreach (SyndicationItem item in feed.Items)
            {
                string subject = item.Title?.Text;
                string summary = "";
                string contentType = "";
                string postURL = item.Links[0].Uri.AbsoluteUri;

                if(item.Summary != null)
                {
                    summary = item.Summary.Text;
                    contentType = item.Summary.Type;
                }
                else
                {
                    TextSyndicationContent content = (TextSyndicationContent)item.Content;
                    if (content != null)
                    {
                        summary = content.Text;
                        contentType = content.Type;
                    }
                }
                DateTime lastUpdated = item.LastUpdatedTime.DateTime;
                DateTime published = item.PublishDate.DateTime;

                if(published <= DateTime.MinValue)
                {
                    if (lastUpdated >= DateTime.MinValue)
                    {
                        published = lastUpdated;
                        lastUpdated = DateTime.MinValue;
                    }
                }

                // We are not saving more than 800 chars of the Summary from a post.
                if (summary.Length > 800)
                    summary = summary.Substring(0, 799);

                output.Add
                (
                    new RSSPost
                    {
                        Id = 1,
                        FeedRefId = rSSFeed.Id,
                        Subject = subject,
                        ContentType = contentType,
                        Content = summary,
                        PostURL = postURL,
                        Published = published,
                        LastUpdated = lastUpdated
                    }
                ) ;
            }
            return output;
        }
        private static List<RSSFeed> GetRSSFeeds()
        {
            RSSFeedRepository repository = new RSSFeedRepository();

            List<RSSFeed> output = repository.ReadAll();
            //List<RSSFeed> output = new List<RSSFeed>();

            //output.Add(new RSSFeed { Id = 1, Name = "On MSFT", URL = "https://www.onmsft.com/feed" });
            //output.Add(new RSSFeed { Id = 2, Name = "Microsoft Dynamics AX Forum", URL = "https://community.dynamics.com/ax/f/microsoft-dynamics-ax-forum/rss" });
            //output.Add(new RSSFeed { Id = 3, Name = "Reddit - Android", URL = "https://www.reddit.com/r/Android/.rss" });

            return output;
        }
        private static void CreateRSSFeeds()
        {
            IRSSFeedRepository repository = new RSSFeedRepository();

            repository.Add(new RSSFeed { Id = 1, Name = "On MSFT", URL = "https://www.onmsft.com/feed" });
            repository.Add(new RSSFeed { Id = 2, Name = "Microsoft Dynamics AX Forum", URL = "https://community.dynamics.com/ax/f/microsoft-dynamics-ax-forum/rss" });
            repository.Add(new RSSFeed { Id = 3, Name = "Reddit - Android", URL = "https://www.reddit.com/r/Android/.rss" });
        }
    }
}
