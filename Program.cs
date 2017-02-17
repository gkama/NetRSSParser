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
            Yahoo.FinanceRSS yf = new Yahoo.FinanceRSS("msft");

            RSS rsss = new RSS("https://feeds.finance.yahoo.com/rss/2.0/headline?s=msft&region=US&lang=en-US");

            Console.WriteLine(GoogleNews.TopStories.Title);
        }
    }
}
