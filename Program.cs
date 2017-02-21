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
            Yahoo.Finance yf = new Yahoo.Finance("msft");

            RSS rsss = new RSS("https://feeds.finance.yahoo.com/rss/2.0/headline?s=msft&region=US&lang=en-US");

            DateTime FromDate = new DateTime(2017, 2, 1);
            DateTime ToDate = new DateTime(2017, 2, 10);
            Yahoo.StockQuote sq = new Yahoo.StockQuote("yhoo", FromDate, ToDate, "w");

            TheGuardian.RSS.RSS tgrss = new TheGuardian.RSS.RSS("https://www.theguardian.com/uk/rss");
            TheGuardian.ByGategory rewrow = new TheGuardian.ByGategory("UK");

            Console.WriteLine(GoogleNews.Business.Language);
        }
    }
}
