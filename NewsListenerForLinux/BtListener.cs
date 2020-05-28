using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace NewsListenerForLinux
{
    public class BtListener
    {
        private CrawlManager manager;
        public BtListener(CrawlManager manager)
        {
            this.manager = manager;
        }
        private static readonly Regex hrefPattern = new Regex("href\\s*=\\s*(?:\"(?<1>[^\"]*)\"|(?<1>\\S+))", RegexOptions.IgnoreCase);
        public void crawl()
        {
            string urlStr = "https://www.bt.dk/nyheder";
            UriBuilder ub = new UriBuilder(urlStr);
            WebClient wc = new WebClient();

            string webPage = wc.DownloadString(ub.Uri.ToString());
            webPage = webPage.Split("container bg ")[1];
            webPage = webPage.Split("site-footer")[0];
            // var urls = urlTagPattern.Matches(webPage);
            //Console.WriteLine(webPage);
            var urls = webPage.Split("<a ");
            List<string> links = new List<string>();
            foreach (string url in urls)
            {
                //Console.WriteLine(url);
                string newUrl = url.Split("\"")[1];
                if (newUrl.Equals("") || newUrl.Contains("id="))
                {
                    continue;
                }
                if (Program.newsLinks.Contains(newUrl) || links.Contains(newUrl)) // enten dette eller lave en liste over allerede besøgte links
                {
                    continue;
                }
                if (newUrl.StartsWith("/"))
                {
                    newUrl = "https://www.bt.dk" + newUrl;
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
