using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsListenerForLinux
{
    static class Program
    {
        public static Queue<string> newsLinks;
        public static Dictionary<string, string> newsTitlePlusLink; // reset once a day

        static void Main()
        {
            using (NewsContext db = new NewsContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
            Task taska = Task.Run(() => SocketListener.StartListening());
            new CrawlManager();
            Console.ReadLine();
        }
    }
}
