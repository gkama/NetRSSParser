using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;

namespace NetRSSParser
{
    class Program
    {
        static void Main(string[] args)
        {
            GoogleNews.RSS c = new GoogleNews.RSS("https://news.google.com/news?cf=all&hl=en&pz=1&ned=us&output=rss");
            Console.WriteLine(GoogleNews.TopStories.Title);
        }
    }
}
