using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.ServiceModel.Syndication;

namespace NetRSSParser
{
    class RSS
    {
        //Variables
        public string Title { get; set; }
        public string Link { get; set; }
        public string Language { get; set; }
        public string URL { get; set; }
        public string PublishedDate { get; set; }
        public string LastBuildDate { get; set; }

        public List<string> Titles { get; set; }
        public List<string> URLs { get; set; }
        public List<string> TitlesURLs { get; set; }
        public List<string> Summaries { get; set; }

        //Constructors
        public RSS(string URL)
        {
            this.URL = URL;
            Titles = new List<string>();
            URLs = new List<string>();
            TitlesURLs = new List<string>();
            Summaries = new List<string>();

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
                XmlReader reader = XmlReader.Create(URL);
                SyndicationFeed  SF = SyndicationFeed.Load(reader);
                reader.Close();

                foreach (SyndicationItem item in SF.Items)
                {
                    Titles.Add(item.Title.Text.Trim());
                    Summaries.Add(item.Summary.Text);  
                }
            }
            catch (Exception e) { throw e; }
        }
        private string ParseLink(string Link)
        {
            return Link.Split(new string[] { "*" }, StringSplitOptions.None)[1].Trim();
        }

        //Item class
        class Item
        {

        }
    }
}
