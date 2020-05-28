using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Exam2020_AA
{
    public class DagensListener
    {
        private CrawlManager manager;
        public DagensListener(CrawlManager manager)
        {
            this.manager = manager;
        }
        private static readonly Regex hrefPattern = new Regex("href\\s*=\\s*(?:\"(?<1>[^\"]*)\"|(?<1>\\S+))", RegexOptions.IgnoreCase);
        public void crawl()
        {
            string urlStr = "https://www.dagens.dk/nyheder";
            UriBuilder ub = new UriBuilder(urlStr);
            WebClient wc = new WebClient();

            string webPage = wc.DownloadString(ub.Uri.ToString());
            webPage = webPage.Split("block-system-main")[1];
            webPage = webPage.Split("footer clearfix")[0];
            var urls = webPage.Split("<a ");
            List<string> links = new List<string>();
            foreach (string url in urls)
            {
                string newUrl = url.Split("\"")[1];
                if (newUrl.Equals("") || newUrl.Length < 35)
                {
                    continue;
                }
                if (Program.newsLinks.Contains(newUrl) || links.Contains(newUrl))
                {
                    continue;
                }
                
                links.Add(newUrl);
            }
            manager.addLinksToQueue(links);
        }
    }
}
