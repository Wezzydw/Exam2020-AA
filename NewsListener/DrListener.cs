using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace NewsListener
{
    public class DrListener
    {
        private static readonly Regex hrefPattern = new Regex("href\\s*=\\s*(?:\"(?<1>[^\"]*)\"|(?<1>\\S+))", RegexOptions.IgnoreCase);
        public void crawl()
        {
            string urlStr = "https://www.dr.dk/"; // does not work on /nyhder
            UriBuilder ub = new UriBuilder(urlStr);
            WebClient wc = new WebClient();

            string webPage = wc.DownloadString(ub.Uri.ToString());
            webPage = webPage.Split("container")[1];
            webPage = webPage.Split("marketing-banner-radio")[0];
            // var urls = urlTagPattern.Matches(webPage);
            //Console.WriteLine(webPage);
            var urls = webPage.Split("<a ");
            foreach (string url in urls)
            {

                //Console.WriteLine(url);
                string newUrl = hrefPattern.Match(url).Groups[1].Value;
                if (newUrl.Equals(""))
                {
                    continue;
                }
                if (Program.newsLinks.Contains(newUrl)) // enten dette eller lave en liste over allerede besøgte links
                {
                    continue;
                }
                if (newUrl.StartsWith("/"))
                {
                    newUrl = "https://www.dr.dk" + newUrl;
                    if (Program.newsLinks.Contains(newUrl))
                    {
                        continue;
                    }
                    if (newUrl.Contains("nyheder") && newUrl.Length > 56)
                    {
                        Console.WriteLine(newUrl);
                        Program.newsLinks.Enqueue(newUrl);
                        continue;
                    }
                    
                }
                if (newUrl.Contains("nyheder") && newUrl.Length > 56)
                {
                    Console.WriteLine(newUrl);
                    Program.newsLinks.Enqueue(newUrl);
                }
            }
        }
    }
}
