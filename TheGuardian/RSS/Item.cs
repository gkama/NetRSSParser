using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetRSSParser.TheGuardian.RSS
{
    //Item
    public class Item
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string PublishedDate { get; set; }
        public string Creator { get; set; }
        public string Date { get; set; }
        public List<string> Categories { get; set; }

        public Item(string Title, string Link, string Description, string PublishedDate, string Creator, string Date, List<string> Categories)
        {
            this.Title = Title;
            this.Link = Link;
            this.Description = Description;
            this.PublishedDate = PublishedDate;
            this.Creator = Creator;
            this.Date = Date;
            this.Categories = Categories;
        }
    }
}
