using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NetRSSParser.TheGuardian.RSS
{
    public class RSS
    {
        //Variables
        public string Title { get; set; }
        public string Link { get; set; }
        public string Language { get; set; }
        public string Copyright { get; set; }
        public string Description { get; set; }
        public string PublishedDate { get; set; }
        public string URL { get; set; }

        public List<string> Titles { get; set; }
        public List<string> Links { get; set; }
        public List<string> Descriptions { get; set; }
        public List<string> PublishedDates { get; set; }
        public List<string> Creators { get; set; }
        public List<string> Dates { get; set; }
        public List<string> TitlesLinks { get; set; }

        public List<Item> Items { get; set; }


        public RSS() { }
        public RSS(string URL)
        {
            this.URL = URL;
            Titles = new List<string>();
            Links = new List<string>();
            Descriptions = new List<string>();
            PublishedDates = new List<string>();
            Creators = new List<string>();
            Dates = new List<string>();
            TitlesLinks = new List<string>();

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
                this.Copyright = TL.SelectSingleNode("copyright").InnerText.Trim();
                this.PublishedDate = TL.SelectSingleNode("pubDate").InnerText.Trim();

                // Parse the Items in the RSS file
                XmlNodeList rssNodes = rssXmlDoc.SelectNodes("rss/channel/item");
                XmlNamespaceManager dcMgr = new XmlNamespaceManager(rssXmlDoc.NameTable);
                dcMgr.AddNamespace("dc", "http://purl.org/dc/elements/1.1/");
                foreach (XmlNode rssNode in rssNodes)
                {
                    //Title
                    XmlNode rssSubNode = rssNode.SelectSingleNode("title");
                    string title = rssSubNode != null ? rssSubNode.InnerText : "";

                    //Link/Url
                    rssSubNode = rssNode.SelectSingleNode("link");
                    string link = rssSubNode != null ? rssSubNode.InnerText : "";

                    //Description
                    rssSubNode = rssNode.SelectSingleNode("description");
                    string description = rssSubNode != null ? rssSubNode.InnerText : "";

                    //Published date
                    rssSubNode = rssNode.SelectSingleNode("pubDate");
                    string pubDate = rssSubNode != null ? rssSubNode.InnerText : "";

                    //Creator
                    rssSubNode = rssNode.SelectSingleNode("dc:creator", dcMgr);
                    string creator = rssSubNode != null ? rssSubNode.InnerText : "";

                    //Date
                    rssSubNode = rssNode.SelectSingleNode("dc:date", dcMgr);
                    string date = rssSubNode != null ? rssSubNode.InnerText : "";

                    //Categories
                    List<Item.Category> categories = new List<Item.Category>();
                    foreach (XmlNode c in rssNode.SelectNodes("category"))
                        categories.Add(new Item.Category(c.Attributes["domain"].Value, c.InnerText.Trim()));

                    Titles.Add(title);
                    Links.Add(link);
                    Descriptions.Add(description);
                    PublishedDates.Add(pubDate);
                    Creators.Add(creator);
                    Dates.Add(date);
                    TitlesLinks.Add(title + " - " + link);

                    Items.Add(new Item(title, link, description, pubDate, creator, date, categories));
                }
            }
            catch (Exception e) { throw e; }
        }

        /// <summary>
        /// Searches data to find items that match the specified category
        /// </summary>
        /// <param name="Category">Category to be searched for</param>
        /// <returns>Returns a List of Items and their data</returns>
        public List<Item> Search(string Category)
        {
            Category = Category.Trim().ToLower();
            List<Item> toReturn = new List<Item>();
            foreach (Item item in Items)
            {
                foreach (Item.Category category in item.Categories)
                    if (category.Name.Trim().ToLower().Contains(Category) && !toReturn.Contains(item))
                        toReturn.Add(item);
            }
            return toReturn;
        }
        /// <summary>
        /// Searches data to find items that match the specified categories
        /// </summary>
        /// <param name="Category">Category to be searched for</param>
        /// <returns>Returns a List of Items and their data</returns>
        public List<Item> Search(List<string> Categories)
        {
            List<Item> toReturn = new List<Item>();
            foreach (Item item in Items)
            {
                foreach (string Category in Categories)
                {
                    foreach (Item.Category category in item.Categories)
                        if (category.Name.Trim().ToLower().Contains(Category.Trim().ToLower()) && !toReturn.Contains(item))
                            toReturn.Add(item);
                }
            }
            return toReturn;
        }
        /// <summary>
        /// Searches data to find items that match the specified title
        /// </summary>
        /// <param name="Title">Title to be searched for</param>
        /// <returns>Returns a List of Items and their data</returns>
        public List<Item> SearchTitle(string Title)
        {
            Title = Title.Trim().ToLower();
            List<Item> toReturn = new List<Item>();
            foreach (Item item in Items)
            {
                if (item.Title.Trim().ToLower().Contains(Title) && !toReturn.Contains(item))
                    toReturn.Add(item);
            }
            return toReturn;
        }
        /// <summary>
        /// Searches data to find items that match the specified categories
        /// </summary>
        /// <param name="Titles">Titles to be searched for</param>
        /// <returns>Returns a List of Items and their data</returns>
        public List<Item> SearchTitle(List<string> Titles)
        {
            List<Item> toReturn = new List<Item>();
            foreach (Item item in Items)
            {
                foreach (string title in Titles)
                    if (item.Title.Trim().ToLower().Contains(title.Trim().ToLower()) && !toReturn.Contains(item))
                        toReturn.Add(item);
            }
            return toReturn;
        }
    }
}
