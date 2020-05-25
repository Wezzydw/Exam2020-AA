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
            // var urls = urlTagPattern.Matches(webPage);
            //Console.WriteLine(webPage);
            var urls = webPage.Split("<a ");
            List<string> links = new List<string>();
            foreach (string url in urls)
            {
                //Console.WriteLine(url);
                string newUrl = hrefPattern.Match(url).Groups[1].Value;
                if (newUrl.Equals(""))
                {
                    continue;
                }
                if (Program.newsLinks.Contains(newUrl) || links.Contains(newUrl)) // enten dette eller lave en liste over allerede besøgte links
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
                    Console.WriteLine(newUrl);
                    links.Add(newUrl);
                    continue;
                }
                Console.WriteLine(newUrl);
                links.Add(newUrl);
            }
            manager.addLinksToQueue(links);
        }
    }
}
