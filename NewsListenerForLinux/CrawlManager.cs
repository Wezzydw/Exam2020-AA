using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NewsListenerForLinux
{
    public partial class CrawlManager { 
        private CancellationTokenSource ts;
        private Object theLinkLock = new object();
        private Boolean started = false;
        private Boolean invokedByTimer = false;
        private Boolean stoppedByUser = false;
        private int sleepTimer = 1;
        private int minuteInMilis = 60000;
        private int sixHours = 21600000;
        public CrawlManager()
        {
            TimerThread();
        }

        private void TimerThread()
        {
            Start();
            Task task = Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(sixHours);
                    Start();
                }
            });
        }

        //start
        private void Start()
        {
            CommunicationRepository.EmptyDatabase();
            theLinkLock = new object();

            Program.newsLinks = new Queue<string>();
            Program.newsTitlePlusLink = new Dictionary<string, string>();
            ts = new CancellationTokenSource();

            Task task1 = Task.Run(() =>
            {
                ExtraBladetListener crawler = new ExtraBladetListener(this);
                crawler.crawl();
            });


            Task task2 = Task.Run(() =>
            {
                DrListener crawler = new DrListener(this);
                crawler.crawl();
            });

            Task task3 = Task.Run(() =>
            {
                Tv2Listener crawler = new Tv2Listener(this);
                crawler.crawl();
            });


            Task task4 = Task.Run(() =>
            {
                BtListener crawler = new BtListener(this);
                crawler.crawl();

            });
            Task task5 = Task.Run(() =>
            {
                DagensListener crawler = new DagensListener(this);
                crawler.crawl();
            });

            for (int i = 10 - 1; i >= 0; i--)
            {
                Task task11 = Task.Run(() =>
                {
                    string searchWord;
                    searchWord = "";
                    TitleCrawler titleCrawler = new TitleCrawler(this);
                    titleCrawler.GetTitles(ts.Token, searchWord);
                });
            }
        }
                
        public void UpdateGui(string title)
        {
            Console.WriteLine("Title"+title);
        }
        public string GetSite()
        {
            lock (theLinkLock)
            {
                Program.newsLinks.TryDequeue(out string link);
                return link;
            }
        }
        public void addLinksToQueue(List<string> links)
        {
            lock (theLinkLock)
            {
                foreach (string link in links)
                {
                    Program.newsLinks.Enqueue(link);
                }
            }
        }
        public void addLinkToResult(string url, string title)
        {
            lock (theLinkLock)
            {
                Program.newsTitlePlusLink.Add(url, title);
                News news = new News() { Title = title, Url = url };
                CommunicationRepository.CreateNews(news);
            }
        }
    }
}
