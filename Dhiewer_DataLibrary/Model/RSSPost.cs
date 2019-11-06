using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhiewer_DataLibrary.Model
{
    public class RSSPost
    {
        public int Id { get; set; }
        public int FeedRefId { get; set; }
        public string Subject { get; set; }
        public string ContentType { get; set; }
        public string Content { get; set; }
        public string PostURL { get; set; }
        public DateTime Published { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool Read { get; set; }

        public override string ToString()
        {
            return $"{FeedRefId} - {Id} - {Subject}";
        }
        public bool Validate()
        {
            if (Subject.Length > 200)
                return false;

            if (ContentType.Length > 30)
                return false;
            if (Content.Length > 800)
                return false;
            if (PostURL.Length > 200)
                return false;

            return true;
        }
    }
}
