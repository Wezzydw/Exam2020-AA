using Exam2020_AA.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exam2020_AA
{
    static class Program
    {
        public static Queue<string> newsLinks;
        public static Dictionary<string, string> newsTitlePlusLink;
        [STAThread]

        static void Main()
        {
            using (NewsContext db = new NewsContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
                Task taska = Task.Run(() => SocketListener.StartListening());
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CrawlManager());
        }
    }
}
