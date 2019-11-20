using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhiewer_DataLibrary.Model
{
    public class RSSFeed
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public int UnreadPosts { get; set; }

        public string IconURL
        {
            get 
            {
                if(URL != null)
                {
                    Uri uri = new Uri(URL);

                    return $"https://plus.google.com/_/favicon?domain={uri.DnsSafeHost}";
                }
                else
                {
                    return "/img/icon_rss16.png";
                }

            }
        }

    }
}
