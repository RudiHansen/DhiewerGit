using System;
using System.Collections.Generic;
using Dhiewer_DataLibrary.Model;

namespace Dhiewer_DataLibrary.Repositories
{
    public interface IRSSPostRepository
    {
        void Add(RSSPost rssPost);
        void Update(RSSPost rssPost);
        void Delete(RSSPost rssPost);
        bool Exist(RSSPost rssPost);
        List<RSSPost> ReadAll();
        List<RSSPost> ReadUnread();
        List<RSSPost> ReadUnreadFromFeedId(int FeedRefId);
        RSSPost GetById(int id);
        RSSPost GetBySubject(string subject);
        RSSPost GetByFeedRefIdSubject(RSSPost rssPost);
        void MarkRead(int id);
    }
}
