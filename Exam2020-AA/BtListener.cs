using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Exam2020_AA
{
    public class BtListener
    {
        private CrawlManager manager;
        public BtListener(CrawlManager manager)
        {
            this.manager = manager;
        }
        /// <summary>
        /// This method goes to bt.dk/nyheder, gets the news on the page
        /// and tells the manager to put them into its queue
        /// </summary>
        public void crawl()
        {
            string urlStr = "https://www.bt.dk/nyheder";
            UriBuilder ub = new UriBuilder(urlStr);
            WebClient wc = new WebClient();

            string webPage = wc.DownloadString(ub.Uri.ToString());
            webPage = webPage.Split("container bg ")[1];
            webPage = webPage.Split("site-footer")[0];
            var urls = webPage.Split("<a ");
            List<string> links = new List<string>();
            foreach (string url in urls)
            {
                string newUrl = url.Split("\"")[1];
                if (newUrl.Equals("") || newUrl.Contains("id="))
                {
                    continue;
                }
                if (Program.newsLinks.Contains(newUrl) || links.Contains(newUrl))
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
                    links.Add(newUrl);
                    continue;
                }
                links.Add(newUrl);
            }
            manager.addLinksToQueue(links);
        }
    }
}
