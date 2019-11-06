﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dhiewer_DataLibrary.Model;

namespace Dhiewer_DataLibrary.Repositories
{
    public class RSSPostRepository : IRSSPostRepository
    {
        public void Add(RSSPost rssPost)
        {
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString))
            {
                SqlMapper.AddTypeMap(typeof(DateTime), DbType.DateTime2);
                string insertQuery = @"INSERT INTO [Dhiewer].[RSSPost]([FeedRefId], [Subject], [ContentType], [Content], [PostURL], [Published], [LastUpdated], [Read])"+
                                     @" VALUES (@FeedRefId, @Subject, @ContentType, @Content, @PostURL, @Published, @LastUpdated, @Read)";

                var result = db.Execute(insertQuery, rssPost);
            }
        }
        public void Delete(RSSPost rssPost) // NOT IMPLEMENTED
        {
            throw new NotImplementedException();
        }
        public void Update(RSSPost rssPost)
        {
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString))
            {
                string updateQuery = @"UPDATE [Dhiewer].[RSSPost] SET [Subject] = @Subject, [ContentType] = @ContentType, [Content] = @Content," +
                                     @"[PostURL] = @PostURL, [Published] = @Published, [LastUpdated] = @LastUpdated, [Read] = @Read  WHERE FeedRefId = @FeedRefId";

                var result = db.Execute(updateQuery, rssPost);
            }
        }
        public bool Exist(RSSPost rssPost) // NOT IMPLEMENTED
        {
            throw new NotImplementedException();
        }

        public List<RSSPost> ReadAll()
        {
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString))
            {
                string selectQuery = @"SELECT * FROM [Dhiewer].[RSSPost]";

                return db.Query<RSSPost>(selectQuery).ToList();
            }
        }
        public List<RSSPost> ReadUnread()
        {
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString))
            {
                string selectQuery = @"SELECT * FROM [Dhiewer].[RSSPost] WHERE [Read]=0";

                return db.Query<RSSPost>(selectQuery).ToList();
            }
        }
        public List<RSSPost> GetByFeedRefId(int feedRefId) // NOT IMPLEMENTED
        {
            throw new NotImplementedException();
        }
        public RSSPost GetByFeedRefIdSubject(RSSPost rssPost)
        {
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString))
            {
                string selectQuery = @"SELECT * FROM [Dhiewer].[RSSPost] WHERE [FeedRefId] = @FeedRefId AND [Subject] = @Subject";

                return db.Query<RSSPost>(selectQuery, rssPost).SingleOrDefault();
            }
        }
        public RSSPost GetById(int id) // NOT IMPLEMENTED
        {
            throw new NotImplementedException();
        }
        public RSSPost GetBySubject(string subject) // NOT IMPLEMENTED
        {
            throw new NotImplementedException();
        }

    }
}