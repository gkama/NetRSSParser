using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetRSSParser.GoogleNews
{
    public static class RSS_URL
    {
        public static Dictionary<string, string> URL = new Dictionary<string, string>()
        {
            {"TopStories", "https://news.google.com/news?cf=all&hl=en&pz=1&ned=us&output=rss"},
            {"World", "https://news.google.com/news?cf=all&hl=en&pz=1&ned=us&topic=w&output=rss"},
            {"US", "https://news.google.com/news?cf=all&hl=en&pz=1&ned=us&topic=n&output=rss"},
            {"Business", "https://news.google.com/news?cf=all&hl=en&pz=1&ned=us&topic=b&output=rss"},
            {"Technology", "https://news.google.com/news?cf=all&hl=en&pz=1&ned=us&topic=tc&output=rss"},
            {"Entertainment", "https://news.google.com/news?cf=all&hl=en&pz=1&ned=us&topic=e&output=rss"},
            {"Sports", "https://news.google.com/news?cf=all&hl=en&pz=1&ned=us&topic=s&output=rss"},
            {"Science", "https://news.google.com/news?cf=all&hl=en&pz=1&ned=us&topic=snc&output=rss"},
            {"Health", "https://news.google.com/news?cf=all&hl=en&pz=1&ned=us&topic=m&output=rss"}
        };
    }
}
