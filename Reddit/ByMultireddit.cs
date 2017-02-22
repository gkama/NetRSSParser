using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetRSSParser.Reddit
{
    class ByMultireddit
    {
        private const string BASEURL = "http://www.reddit.com/r/";
        private List<string> Subreddits { get; set; }
        private RSS.RSS rss;
        /// <summary>
        /// Retrieves data for specified subreddits
        /// </summary>
        /// <param name="Subreddits">A list of subreddits to retrieve RSS's from</param>
        public ByMultireddit(List<string> Subreddits)
        {
            try
            {
                this.Subreddits = Subreddits;
                StringBuilder subreddits = new StringBuilder();
                foreach (string s in Subreddits)
                    subreddits.Append(s).Append("+");
                subreddits.Length--;
                rss = new RSS.RSS(BASEURL + subreddits.ToString().Trim().ToLower() + "/.rss");
            }
            catch (Exception e) { throw e; }
        }

        //Variables
        public string Title { get { return rss.Title; } }
        public string Link { get { return rss.Link; } }
        public string ID { get { return rss.ID; } }
        public string Updated { get { return rss.Updated; } }
        public RSS.Entry._Category Category { get { return rss.Category; } }
        public string URL { get { return rss.URL; } }
        public List<string> Titles { get { return rss.Titles; } }
        public List<string> Contents { get { return rss.Contents; } }
        public List<string> IDs { get { return rss.IDs; } }
        public List<string> Updateds { get { return rss.Updateds; } }
        public Dictionary<string, List<string>> TitlesLinks { get { return rss.TitlesLinks; } }
        public List<RSS.Entry> Entries { get { return rss.Entries; } }
    }
}
