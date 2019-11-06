using System;
using System.Collections.Generic;
using Dhiewer_DataLibrary.Model;

namespace Dhiewer_DataLibrary.Repositories
{
    public interface IRSSFeedRepository
    {
        void Add(RSSFeed rssFeed);
        void Update(RSSFeed rssFeed);
        void Delete(RSSFeed rssFeed);

        List<RSSFeed> ReadAll();
        RSSFeed GetById(int id);
        RSSFeed GetByName(string name);
    }
}
