using Exam2020_AA;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;

namespace NewsListener
{
    public class TitleCrawler
    {
        private CrawlManager manager;
        public TitleCrawler(CrawlManager manager)
        {
            this.manager = manager;
        }
        public void GetTitles(CancellationToken token, string searchWord)
        {
            while (!token.IsCancellationRequested)
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
                        string body = "";
                        if (link.Contains("ekstrabladet"))
                        {
                            body = webPage.Split("fnBodytextTracking")[1];
                            if (!body.Equals("") && body.Contains(searchWord))
                            {
                                webPage = webPage.Split("<title")[1];
                                webPage = webPage.Split("</title>")[0];
                                webPage = webPage.Split(">")[1];
                                //Console.WriteLine("webpage title = " + webPage);
                                
                                if (webPage.Contains("Ekstra Bladet"))
                                {
                                    webPage = webPage[0..^15];
                                }
                                if (Program.newsTitlePlusLink.ContainsKey(link))
                                {
                                    continue;
                                }
                                manager.addLinkToResult(link, webPage);
                                manager.UpdateGui(webPage);
                            }
                        }
                        if (link.Contains("dr.dk/nyheder"))
                        {
                            body = webPage.Split("articleBody")[1];
                            body = body.Split("dre-text-selection-toolbar")[0];
                            if (!body.Equals("") && body.Contains(searchWord))
                            {
                                webPage = webPage.Split("<title")[1];
                                webPage = webPage.Split("</title>")[0];
                                webPage = webPage.Split(">")[1];
                                //Console.WriteLine("webpage title = " + webPage);
                                if (webPage.Contains("|"))
                                {
                                    webPage = webPage.Split("|")[0];
                                }
                                if (Program.newsTitlePlusLink.ContainsKey(link))
                                {
                                    continue;
                                }
                                manager.addLinkToResult(link, webPage);
                                manager.UpdateGui(webPage);
                            }
                        }
                        if (link.Contains("nyheder.tv2.dk"))
                        {
                            body = webPage.Split("article-body")[1];
                            body = body.Split("tc_page__footer")[0];
                            if (!body.Equals("") && body.Contains(searchWord))
                            {
                                webPage = webPage.Split("<title")[1];
                                webPage = webPage.Split("</title>")[0];
                                webPage = webPage.Split(">")[1];
                                //Console.WriteLine("webpage title = " + webPage);
                                /*if (webPage.Contains("|"))
                                {
                                    webPage = webPage.Split("|")[0];
                                }*/
                                if (Program.newsTitlePlusLink.ContainsKey(link))
                                {
                                    continue;
                                }
                                manager.addLinkToResult(link, webPage);
                                manager.UpdateGui(webPage);
                            }
                        }
                        if (link.Contains("bt.dk"))
                        {
                            body = webPage.Split("article-content")[1];
                            body = body.Split("col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12")[0];
                            if (!body.Equals("") && body.Contains(searchWord))
                            {
                                webPage = webPage.Split("<title")[1];
                                webPage = webPage.Split("</title>")[0];
                                webPage = webPage.Split(">")[1];
                                //Console.WriteLine("webpage title = " + webPage);
                                if (webPage.Contains("|"))
                                {
                                    webPage = webPage.Split("|")[0];
                                }
                                if (Program.newsTitlePlusLink.ContainsKey(link))
                                {
                                    continue;
                                }
                                manager.addLinkToResult(link, webPage);
                                manager.UpdateGui(webPage);
                            }
                        }
                        if (link.Contains("dagens.dk"))
                        {
                            body = webPage.Split("article-content")[1];
                            body = body.Split("social-share")[0];
                            if (!body.Equals("") && body.Contains(searchWord))
                            {
                                webPage = webPage.Split("<title")[1];
                                webPage = webPage.Split("</title>")[0];
                                webPage = webPage.Split(">")[1];
                                //Console.WriteLine("webpage title = " + webPage);
                                
                                if (Program.newsTitlePlusLink.ContainsKey(link))
                                {
                                    continue;
                                }
                                manager.addLinkToResult(link, webPage);
                                manager.UpdateGui(webPage);
                            }
                        }

                    }
                    catch (Exception)
                    {

                        continue;
                    }
                } else
                {
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
