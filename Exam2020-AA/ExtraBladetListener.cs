using Exam2020_AA;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace NewsListener
{
    public class ExtraBladetListener
    {
        private CrawlManager manager;
        public ExtraBladetListener(CrawlManager manager)
        {
            this.manager = manager;
        }
        private static readonly Regex hrefPattern = new Regex("href\\s*=\\s*(?:\"(?<1>[^\"]*)\"|(?<1>\\S+))", RegexOptions.IgnoreCase);
        public void crawl()
        {
            string urlStr = "https://ekstrabladet.dk/nyheder"; // also works on /nyhder
            UriBuilder ub = new UriBuilder(urlStr);
            WebClient wc = new WebClient();

            string webPage = wc.DownloadString(ub.Uri.ToString());
            webPage = webPage.Split("sitecontent")[2];
            webPage = webPage.Split("footer")[0];
            var urls = webPage.Split("<a ");
            List<string> links = new List<string>();
            foreach (string url in urls)
            {
                string newUrl = hrefPattern.Match(url).Groups[1].Value;
                if (newUrl.Equals(""))
                {
                    continue;
                }
                if (Program.newsLinks.Contains(newUrl) || links.Contains(newUrl))
                {
                    continue;
                }
                if (newUrl.StartsWith("/"))
                {
                    newUrl = "https://ekstrabladet.dk" + newUrl;
                    if (Program.newsLinks.Contains(newUrl) || links.Contains(newUrl))
                    {
                        continue;
                    }
                    links.Add(newUrl);
                    continue;
                }
                links.Add(newUrl);
            }
            manager.addLinksToQueue(links);
        }
    }
}
