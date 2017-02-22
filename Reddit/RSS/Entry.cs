using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetRSSParser.Reddit.RSS
{
    public class Entry
    {
        public List<_Author> Authors { get; set; }
        public List<_Category> Categories { get; set; }
        public string Content { get; set; }
        public string ID { get; set; }
        public List<string> Links { get; set; }
        public string Updated { get; set; }
        public string Title { get; set; }

        public Entry(List<_Author> Authors, List<_Category> Categories, string Content, string ID, List<string> Links, string Updated, string Title)
        {
            this.Authors = Authors;
            this.Categories = Categories;
            this.Content = Content;
            this.ID = ID;
            this.Links = Links;
            this.Updated = Updated;
            this.Title = Title;
        }

        //Author
        public class _Author
        {
            public string Name { get; set; }
            public string URI { get; set; }

            public _Author(string Name, string URI)
            {
                this.Name = Name;
                this.URI = URI;
            }
        }
        //Category
        public class _Category
        {
            public string Name { get; set; }
            public string Label { get; set; }

            public _Category(string Name, string Label)
            {
                this.Name = Name;
                this.Label = Label;
            }
        }
    }
}
