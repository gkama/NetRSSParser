using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;

namespace NetRSSParser.GoogleNews
{
    public class RSS
    {
        //Variables
        public string Title { get; set; }
        public string Link { get; set; }
        public string URL { get; set; }
        public string PublishedDate { get; set; }
        public string LastBuildDate { get; set; }

        public List<string> Titles { get; set; }
        public List<string> URLs{ get; set; }
        public List<string> TitlesURLs { get; set; }

        //Constructors
        public RSS(string URL)
        {
            this.URL = URL;
            Titles = new List<string>();
            URLs = new List<string>();
            TitlesURLs = new List<string>();

            ParseRSS();
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
                this.PublishedDate = TL.SelectSingleNode("pubDate").InnerText.Trim();
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

                    Titles.Add(title);
                    URLs.Add(link);
                    TitlesURLs.Add(title + " - " + link);
                }
            }
            catch (Exception e) { throw e; }
        }
        private string ParseLink(string Link)
        {
            foreach (string s in Link.Split(new string[] { "url=" }, StringSplitOptions.None))
            {
                if (!s.Contains("news.google.com"))
                    return s;
            }
            return "";
        }
    }
}
