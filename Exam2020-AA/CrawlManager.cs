using Exam2020_AA.Communication;
using NewsListener;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exam2020_AA
{
    public partial class CrawlManager : Form
    {
        private CancellationTokenSource ts;
        private Object theLinkLock = new object();
        private Boolean started = false;
        private Boolean invokedByTimer = false;
        private Boolean stoppedByUser = false;
        private int sleepTimer = 1;
        private int minuteInMilis = 60000;

        public CrawlManager()
        {
            InitializeComponent();
            listView1.View = View.Details;
            checkedListBox1.Items.Add("Extra Bladet");
            checkedListBox1.Items.Add("DR dk");
            checkedListBox1.Items.Add("TV2");
            checkedListBox1.Items.Add("BT");
            checkedListBox1.Items.Add("Dagens");
        }

        private void TimerThread()
        {

            Task task = Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(sleepTimer* minuteInMilis);
                    if(stoppedByUser == true)
                    {
                        break; 
                    }
                    invokedByTimer = true;
                    button1.Invoke((MethodInvoker)delegate
                    {
                        button1.PerformClick();
                    });
                }
            });
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            if(textBox2.Text.Length != 0)
            {
                sleepTimer = Int32.Parse(textBox2.Text);
                if (sleepTimer == 0)
                {
                    sleepTimer = 1;
                }
            }
            else
            {
                sleepTimer = 1;
            }
            
        }

        private void Start(object sender, EventArgs e)
        {
            
            if(!started)
            {
                TimerThread();
                started = true;
                stoppedByUser = false;
            }
            CommunicationRepository.EmptyDatabase();
            theLinkLock = new object();
            
            if (button1.Text == "Stop" && invokedByTimer == false)
            {
                ts.Cancel();
                button1.Text = "Start";
                stoppedByUser = true;
                started = false;
                return;
            }
            if(invokedByTimer == true)
            {
                invokedByTimer = false;
            }
            listView1.Clear();

            Program.newsLinks = new Queue<string>();
            Program.newsTitlePlusLink = new Dictionary<string, string>();
            ts = new CancellationTokenSource();

            var column = listView1.Columns.Add("NEWS");
            column.Width = 400;

            if (checkedListBox1.CheckedItems.Contains("Extra Bladet"))
            {
                Task task = Task.Run(() =>
                {
                    ExtraBladetListener crawler = new ExtraBladetListener(this);
                    crawler.crawl();
                });
            }
            if (checkedListBox1.CheckedItems.Contains("DR dk"))
            {
                Task task = Task.Run(() =>
                {
                    DrListener crawler = new DrListener(this);
                    crawler.crawl();
                });
            }
            if (checkedListBox1.CheckedItems.Contains("TV2"))
            {
                Task task = Task.Run(() =>
                {
                    Tv2Listener crawler = new Tv2Listener(this);
                    crawler.crawl();
                });
            }
            if (checkedListBox1.CheckedItems.Contains("BT"))
            {
                Task task = Task.Run(() =>
                {
                    BtListener crawler = new BtListener(this);
                    crawler.crawl();
                });
            }
            if (checkedListBox1.CheckedItems.Contains("Dagens"))
            {
                Task task = Task.Run(() =>
                {
                    DagensListener crawler = new DagensListener(this);
                    crawler.crawl();
                });
            }
            for (int i = 10 - 1; i >= 0; i--)
            {
                Task task1 = Task.Run(() =>
                {
                    string searchWord;
                    if (textBox1 != null)
                    {
                        searchWord = textBox1.Text;
                    }
                    else
                    {
                        searchWord = "";
                    }
                    TitleCrawler titleCrawler = new TitleCrawler(this);
                    titleCrawler.GetTitles(ts.Token, searchWord);
                });
            }
            
            button1.Text = "Stop";

        }
        public void UpdateGui(string title)
        {
            MethodInvoker addToList = delegate
            { listView1.Items.Add(title); };
            listView1.BeginInvoke(addToList);
            MethodInvoker lab = delegate
            { label3.Text = "Result amount: " + Program.newsTitlePlusLink.Count; };
            label3.BeginInvoke(lab);
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
                News news = new News() { Title = title, Url = url};
                CommunicationRepository.CreateNews(news);
            }
        }
    }
}
