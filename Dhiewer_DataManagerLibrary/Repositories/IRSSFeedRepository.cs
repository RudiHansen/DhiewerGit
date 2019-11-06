using Dhiewer_DataManagerLibrary.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dhiewer_DataManagerLibrary.Repositories
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
