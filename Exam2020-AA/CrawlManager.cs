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
        public CrawlManager()
        {
            InitializeComponent();
            listView1.View = View.Details;
        }

        private void Start(object sender, EventArgs e)
        {
            Program.newsLinks = new Queue<string>();
            Program.newsTitlePlusLink = new Dictionary<string, string>();
            ts = new CancellationTokenSource();

            var column = listView1.Columns.Add("NEWS");
            column.Width = 400;
            Task task = new Task(() =>
            {
                ExtraBladetListener crawler = new ExtraBladetListener();
                crawler.crawl();
            });

            task.Start();
            task.Wait();

            Task task1 = Task.Run(() =>
            {
                TitleCrawler titleCrawler = new TitleCrawler(this);
                titleCrawler.GetTitles(ts.Token);
            });

        }
        public void UpdateGui(string title)
        {
            
                MethodInvoker addToList = delegate
                { listView1.Items.Add(title); };
                listView1.BeginInvoke(addToList);
            
        }
    }
}
