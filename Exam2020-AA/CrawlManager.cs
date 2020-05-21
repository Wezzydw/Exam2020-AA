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
            checkedListBox1.Items.Add("Extra Bladet");
            checkedListBox1.Items.Add("DR dk");
            checkedListBox1.Items.Add("TV2");
            checkedListBox1.Items.Add("BT");
        }

        private void Start(object sender, EventArgs e)
        {
            if (button1.Text == "Stop")
            {
                ts.Cancel();
                button1.Text = "Start";
                return;
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
                    ExtraBladetListener crawler = new ExtraBladetListener();
                    crawler.crawl();
                });
            }
            if (checkedListBox1.CheckedItems.Contains("DR dk"))
            {
                Task task = Task.Run(() =>
                {
                    DrListener crawler = new DrListener();
                    crawler.crawl();
                });
            }
            if (checkedListBox1.CheckedItems.Contains("TV2"))
            {
                Task task = Task.Run(() =>
                {
                    Tv2Listener crawler = new Tv2Listener();
                    crawler.crawl();
                });
            }
            if (checkedListBox1.CheckedItems.Contains("BT"))
            {
                Task task = Task.Run(() =>
                {
                    BtListener crawler = new BtListener();
                    crawler.crawl();
                });
            }


            Task task1 = Task.Run(() =>
            {
                string searchWord;
                if (textBox1 != null)
                {
                    searchWord = textBox1.Text;
                } else
                {
                    searchWord = "";
                }
                TitleCrawler titleCrawler = new TitleCrawler(this);
                titleCrawler.GetTitles(ts.Token, searchWord);
            });
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
    }
}
