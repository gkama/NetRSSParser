using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetRSSParser.GoogleNews
{
    public static class Entertainment
    {
        private static RSS rss = new RSS(RSS_URL.URL["Entertainment"]);

        //Variables
        public static string Title { get { return rss.Title; } }
        public static string Link { get { return rss.Link; } }
        public static string Language { get { return rss.Language; } }
        public static string URL { get { return rss.URL; } }
        public static string PublishedDate { get { return rss.PublishedDate; } }
        public static string LastBuildDate { get { return rss.LastBuildDate; } }
        public static List<string> Titles { get { return rss.Titles; } }
        public static List<string> URLs { get { return rss.URLs; } }
        public static List<string> TitlesURLs { get { return rss.TitlesURLs; } }
    }
}
