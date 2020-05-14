using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsListener
{
    class Program
    {
        public static Queue<string> newsLinks;
        public static Dictionary<string, string> newsTitlePlusLink; // reset once a day
        static void Main(string[] args)
        {
            newsLinks = new Queue<string>();
            newsTitlePlusLink = new Dictionary<string, string>();

            Task task = new Task(() =>
            {
                ExtraBladetListener crawler = new ExtraBladetListener();
                crawler.crawl();
            });

            task.Start();
            task.Wait();

            Task task1 = new Task(() =>
            {
                TitleCrawler titleCrawler = new TitleCrawler();
                titleCrawler.GetTitles();
            });
            task1.Start();
            task1.Wait();
            foreach (var item in newsTitlePlusLink)
            {
                Console.WriteLine(item);
            }
        }
    }
}
