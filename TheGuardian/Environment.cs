using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetRSSParser.TheGuardian
{
    public static class Environment
    {
        private static RSS.RSS rss = new RSS.RSS(RSS.RSS_URL.URL(typeof(Environment).Name));

        //Variables
        public static string Title { get { return rss.Title; } }
        public static string Link { get { return rss.Link; } }
        public static string Language { get { return rss.Language; } }
        public static string Copyright { get { return rss.Copyright; } }
        public static string Description { get { return rss.Description; } }
        public static string PublishedDate { get { return rss.PublishedDate; } }
        public static string URL { get { return rss.URL; } }
        public static List<string> Titles { get { return rss.Titles; } }
        public static List<string> Links { get { return rss.Links; } }
        public static List<string> Descriptions { get { return rss.Descriptions; } }
        public static List<string> PublishedDates { get { return rss.PublishedDates; } }
        public static List<string> Creators { get { return rss.Creators; } }
        public static List<string> Dates { get { return rss.Dates; } }
        public static List<string> TitlesLinks { get { return rss.TitlesLinks; } }
        public static List<RSS.Item> Items { get { return rss.Items; } }
        public static List<RSS.Item> Search(string Category) { return rss.Search(Category); }
        public static List<RSS.Item> Search(List<string> Categories) { return rss.Search(Categories); }
        public static List<RSS.Item> SearchTitle(string Title) { return rss.SearchTitle(Title); }
        public static List<RSS.Item> SearchTitle(List<string> Titles) { return rss.SearchTitle(Titles); }
    }
}
