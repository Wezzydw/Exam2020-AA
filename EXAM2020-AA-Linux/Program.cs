using Exam2020_AA;
using Exam2020_AA.Communication;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EXAM2020_AA_Linux
{
    static class Program
    {
        public static Queue<string> newsLinks;
        public static Dictionary<string, string> newsTitlePlusLink; // reset once a day
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]

        static void Main()
        {
            using (NewsContext db = new NewsContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }

            Task taska = Task.Run(() => SocketListener.StartListening());
            new CrawlManager()


        }
    }
}
