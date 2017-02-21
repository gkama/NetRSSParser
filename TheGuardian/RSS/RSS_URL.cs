using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetRSSParser.TheGuardian.RSS
{
    public static class RSS_URL
    {
        public static string URL(string Category) { return "https://www.theguardian.com/" + Category.Trim().ToLower() + "/rss"; }
    }
}
