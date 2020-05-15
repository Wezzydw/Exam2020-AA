using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace NewsListener
{
    public class TitleCrawler
    {
        public void GetTitles()
        {
            while (Program.newsLinks.Count > 0)
            {
                if (Program.newsLinks.TryDequeue(out string link))
                {
                    string urlStr = link;
                    UriBuilder ub = new UriBuilder(urlStr);
                    WebClient wc = new WebClient();

                    try
                    {
                        string webPage = wc.DownloadString(ub.Uri.ToString());
                        //Console.WriteLine(link);
                        //Console.WriteLine(webPage);
                        /* if (!webPage.Contains("<title>"))
                        {
                            Console.WriteLine("link = " + link);
                            continue;
                        }*/

                        webPage = webPage.Split("<title")[1];
                        webPage = webPage.Split("</title>")[0];
                        webPage = webPage.Split(">")[1];
                        //Console.WriteLine("webpage title = " + webPage);
                        if (webPage.Contains("|"))
                        {
                            webPage = webPage.Split("|")[0];
                        }
                        if (webPage.Contains("Ekstra Bladet"))
                        {
                            webPage = webPage[0..^15];
                        }
                        if (Program.newsTitlePlusLink.ContainsKey(link))
                        {
                            continue;
                        }
                        Program.newsTitlePlusLink.Add(link, webPage);
                    }
                    catch (Exception)
                    {

                        continue;
                    }
                    
                }
            }
        }
    }
}
