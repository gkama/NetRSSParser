using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetRSSParser.TheGuardian
{
    public class ByGategory
    {
        private string Category { get; set; }
        private RSS.RSS rss;
        /// <summary>
        /// Search a sub Category of TheGuardian such as UK/US/World/Technology/MobilePhones/ etc. and get its relative data.
        /// </summary>
        /// <param name="Category">Category is the parameter needed to create the url to access the RSS: Examples: "Audio", "US", "MobilePhones", etc.</param>
        public ByGategory(string Category)
        {
            this.Category = Category;
            try
            {
                rss = new RSS.RSS(RSS.RSS_URL.URL(Category));
            }
            catch (Exception e) { throw e; }
        }
        
        //Variables
        public string Title { get { return rss.Title; } }
        public string Link { get { return rss.Link; } }
        public string Language { get { return rss.Language; } }
        public string Copyright { get { return rss.Copyright; } }
        public string Description { get { return rss.Description; } }
        public string PublishedDate { get { return rss.PublishedDate; } }
        public string URL { get { return rss.URL; } }
        public List<string> Titles { get { return rss.Titles; } }
        public List<string> Links { get { return rss.Links; } }
        public List<string> Descriptions { get { return rss.Descriptions; } }
        public List<string> PublishedDates { get { return rss.PublishedDates; } }
        public List<string> Creators { get { return rss.Creators; } }
        public List<string> Dates { get { return rss.Dates; } }
        public List<string> TitlesLinks { get { return rss.TitlesLinks; } }
        public List<RSS.Item> Items { get { return rss.Items; } }
        public List<RSS.Item> Search(string Category) { return rss.Search(Category); }
        public List<RSS.Item> Search(List<string> Categories) { return rss.Search(Categories); }
        public List<RSS.Item> SearchTitle(string Title) { return rss.SearchTitle(Title); }
        public List<RSS.Item> SearchTitle(List<string> Titles) { return rss.SearchTitle(Titles); }
    }
}
