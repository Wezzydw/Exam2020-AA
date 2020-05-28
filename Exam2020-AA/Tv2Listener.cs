using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Exam2020_AA
{
    public class Tv2Listener
    {
        private CrawlManager manager;
        public Tv2Listener(CrawlManager manager)
        {
            this.manager = manager;
        }
        /// <summary>
        /// This method goes to nyheder.tv2.dk, gets the news on the page
        /// and tells the manager to put them into its queue
        /// </summary>
        public void crawl()
        {
            string urlStr = "https://nyheder.tv2.dk/"; 
            UriBuilder ub = new UriBuilder(urlStr);
            WebClient wc = new WebClient();

            string webPage = wc.DownloadString(ub.Uri.ToString());
            webPage = webPage.Split("o-deck g-con g-col g-row_l g-gutter g-colx")[1] + webPage.Split("o-deck g-con g-col g-row_l g-gutter g-colx")[2];
            webPage = webPage.Split("section_load_more_from_term-loadmore")[0];
            var urls = webPage.Split("<a ");
            List<string> links = new List<string>();
            foreach (string url in urls)
            {
                string newUrl = url.Split("\"")[1];
                if (newUrl.Equals("") || newUrl.Contains("div class"))
                {
                    continue;
                }
                if (Program.newsLinks.Contains(newUrl) || links.Contains(newUrl)) 
                {
                    continue;
                }
                if (newUrl.StartsWith("/"))
                {
                    newUrl = "https:" + newUrl;
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
