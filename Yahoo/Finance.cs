using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;

namespace NetRSSParser.Yahoo
{
    public class Finance
    {
        //Variables
        private const string BASE_URL = "http://finance.yahoo.com/rss/headline?s=";

        public string Title { get; set; }
        public string Link { get; set; }
        public string Language { get; set; }
        public string Description { get; set; }
        public string LastBuildDate { get; set; }
        public string URL { get; set; }

        public List<string> Titles { get; set; }
        public List<string> URLs { get; set; }
        public List<string> Descriptions { get; set; }
        public List<string> PublishedDates { get; set; }
        public List<string> TitlesURLs { get; set; }

        public List<Item> Items { get; set; }

        /// <summary>
        /// Initializes a new instance of a Yahoo Finance RSS requested based on a company's stock name or symbol. For example
        /// Microsoft's symbol would be 'msft'
        /// </summary>
        /// <param name="CompanySymbol">Based on a company's stock name or symbol. For example, Microsoft's symbol would be 'msft'</param>
        public Finance(string CompanySymbol)
        {
            this.URL = BASE_URL + CompanySymbol.Trim().ToLower();
            Titles = new List<string>();
            URLs = new List<string>();
            Descriptions = new List<string>();
            PublishedDates = new List<string>();
            TitlesURLs = new List<string>();

            Items = new List<Item>();
            try
            {
                ParseRSS();
            }
            catch (Exception e) { throw e; }
        }

        //Parse
        private void ParseRSS()
        {
            try
            {
                XmlDocument rssXmlDoc = new XmlDocument();
                // Load the RSS file from the RSS URL
                rssXmlDoc.Load(URL);
                //Store initial values - title and link
                XmlNode TL = rssXmlDoc.SelectSingleNode("rss/channel");
                this.Title = TL.SelectSingleNode("title").InnerText.Trim();
                this.Link = TL.SelectSingleNode("link").InnerText.Trim();
                this.Description = TL.SelectSingleNode("description").InnerText.Trim();
                this.Language = TL.SelectSingleNode("language").InnerText.Trim();
                this.LastBuildDate = TL.SelectSingleNode("lastBuildDate").InnerText.Trim();

                // Parse the Items in the RSS file
                XmlNodeList rssNodes = rssXmlDoc.SelectNodes("rss/channel/item");
                foreach (XmlNode rssNode in rssNodes)
                {
                    //Title
                    XmlNode rssSubNode = rssNode.SelectSingleNode("title");
                    string title = rssSubNode != null ? rssSubNode.InnerText : "";

                    //Link/Url
                    rssSubNode = rssNode.SelectSingleNode("link");
                    string link = rssSubNode != null ? rssSubNode.InnerText : "";
                    link = ParseLink(link);

                    //Description
                    rssSubNode = rssNode.SelectSingleNode("description");
                    string description = rssSubNode != null ? rssSubNode.InnerText : "";

                    //Published date
                    rssSubNode = rssNode.SelectSingleNode("pubDate");
                    string pubDate = rssSubNode != null ? rssSubNode.InnerText : "";

                    Titles.Add(title);
                    URLs.Add(link);
                    Descriptions.Add(description);
                    PublishedDates.Add(pubDate);
                    TitlesURLs.Add(title + " - " + link);

                    Items.Add(new Item(title, link, description, pubDate));
                }
            }
            catch (Exception e) { throw e; }
        }
        private string ParseLink(string Link)
        {
            return Link.Split(new string[] { "*" }, StringSplitOptions.None)[1].Trim();
        }

        //Item
        public class Item
        {
            public string Title { get; set; }
            public string Link { get; set; }
            public string Description { get; set; }
            public string PublishedDate { get; set; }

            public Item(string Title, string Link, string Description, string PublishedDate)
            {
                this.Title = Title;
                this.Link = Link;
                this.Description = Description;
                this.PublishedDate = PublishedDate;
            }
        }
    }
}
