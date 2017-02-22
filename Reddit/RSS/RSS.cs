using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.ServiceModel.Syndication;

namespace NetRSSParser.Reddit.RSS
{
    public class RSS
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string ID { get; set; }
        public string Updated { get; set; }
        public Entry._Category Category { get; set; }
        public string URL { get; set; }

        public List<string> Titles { get; set; }
        public List<string> Contents { get; set; }
        public List<string> IDs { get; set; }
        public List<string> Updateds { get; set; }
        public Dictionary<string, List<string>> TitlesLinks { get; set; }

        public List<Entry> Entries { get; set; }


        public RSS(string URL)
        {
            this.URL = URL;
            Titles = new List<string>();
            Contents = new List<string>();
            IDs = new List<string>();
            Updateds = new List<string>();
            TitlesLinks = new Dictionary<string, List<string>>();

            Entries = new List<Entry>();
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
                var feed = SyndicationFeed.Load(XmlReader.Create(URL));

                this.Updated = feed.LastUpdatedTime.ToString();
                this.Title = feed.Title.ToString();
                this.Link = feed.Links.First(l => l.RelationshipType.Trim().ToLower() == "alternate").Uri.ToString();
                this.Category = new Entry._Category(feed.Categories.FirstOrDefault(c => c.Name != "").Name, feed.Categories.FirstOrDefault(c => c.Name != "").Label);
                this.ID = feed.Id;

                //Items
                foreach (var item in feed.Items)
                {
                    //Authors
                    List<Entry._Author> authors = new List<Entry._Author>();
                    foreach (SyndicationPerson author in item.Authors)
                        authors.Add(new Entry._Author(author.Name, author.Uri));

                    //Categories
                    List<Entry._Category> categories = new List<Entry._Category>();
                    foreach (SyndicationCategory category in item.Categories)
                        categories.Add(new Entry._Category(category.Name, category.Label));

                    //Links
                    List<string> Links = new List<string>();
                    foreach (SyndicationLink link in item.Links)
                        Links.Add(link.Uri.ToString());

                    //ID
                    IDs.Add(item.Id.ToString());

                    //Updated
                    Updateds.Add(item.LastUpdatedTime.ToString());

                    //Content
                    Contents.Add(item.Content.ToString());

                    //Titles
                    Titles.Add(item.Title.Text.ToString());
                    //Titles & Links
                    if (!TitlesLinks.ContainsKey(item.Title.ToString().Trim()))
                        TitlesLinks.Add(item.Title.Text.ToString().Trim(), Links);

                    //Add entry
                    Entries.Add(new Entry(authors, categories, item.Content.ToString(), item.Id.ToString(), Links, item.LastUpdatedTime.ToString(), item.Title.ToString()));
                }
            }
            catch (Exception e) { throw e; }
        }
    }
}
