using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Exam2020_AA
{
    public class Tv2Listener
    {
        private static readonly Regex hrefPattern = new Regex("href\\s*=\\s*(?:\"(?<1>[^\"]*)\"|(?<1>\\S+))", RegexOptions.IgnoreCase);
        public void crawl()
        {
            string urlStr = "https://nyheder.tv2.dk/"; 
            UriBuilder ub = new UriBuilder(urlStr);
            WebClient wc = new WebClient();

            string webPage = wc.DownloadString(ub.Uri.ToString());
            webPage = webPage.Split("o-deck g-con g-col g-row_l g-gutter g-colx")[1] + webPage.Split("o-deck g-con g-col g-row_l g-gutter g-colx")[2];
            webPage = webPage.Split("section_load_more_from_term-loadmore")[0];
            // var urls = urlTagPattern.Matches(webPage);
            //Console.WriteLine(webPage);
            var urls = webPage.Split("<a ");
            foreach (string url in urls)
            {
                //Console.WriteLine(url);
                string newUrl = url.Split("\"")[1];
                if (newUrl.Equals("") || newUrl.Contains("div class"))
                {
                    continue;
                }
                if (Program.newsLinks.Contains(newUrl)) // enten dette eller lave en liste over allerede besøgte links
                {
                    continue;
                }
                if (newUrl.StartsWith("/"))
                {
                    newUrl = "https:" + newUrl;
                    if (Program.newsLinks.Contains(newUrl))
                    {
                        continue;
                    }
                    Console.WriteLine(newUrl);
                    Program.newsLinks.Enqueue(newUrl);
                    continue;
                }
                Console.WriteLine(newUrl);
                Program.newsLinks.Enqueue(newUrl);
            }
        }
    }
}
