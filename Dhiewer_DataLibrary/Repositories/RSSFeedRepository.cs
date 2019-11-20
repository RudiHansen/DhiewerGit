using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dhiewer_DataLibrary.Model;

namespace Dhiewer_DataLibrary.Repositories
{
    public class RSSFeedRepository : IRSSFeedRepository
    {
        public void Add(RSSFeed rssFeed)
        {
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString))
            {
                string insertQuery = @"INSERT INTO [Dhiewer].[RSSFeed]([Name], [URL]) VALUES (@Name, @URL)";

                var result = db.Execute(insertQuery, rssFeed);
            }
        }

        public void Delete(RSSFeed rssFeed)
        {
            throw new NotImplementedException();
        }

        public RSSFeed GetById(int id)
        {
            throw new NotImplementedException();
        }

        public RSSFeed GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<RSSFeed> ReadAll()
        {
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString))
            {
                string selectQuery = @"SELECT *, " +
                                     @"    (SELECT COUNT(*)" +
                                     @"        FROM [Dhiewer].[RSSPost]" +
                                     @"        WHERE [Dhiewer].[RSSPost].[FeedRefId] =[Dhiewer].[RSSFeed].[Id]" +
                                     @"        AND [Dhiewer].[RSSPost].[Read] = 0) UnreadPosts" +
                                     @"    FROM [Dhiewer].[RSSFeed]";
                return db.Query<RSSFeed>(selectQuery).ToList();
            }
        }
        public void Update(RSSFeed rssFeed)
        {
            throw new NotImplementedException();
        }
    }
}
