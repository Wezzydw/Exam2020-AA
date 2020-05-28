using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewsListenerForLinux
{
    public class CommunicationRepository
    {
        public static void CreateNewsList(List<News> news)
        {
            using (NewsContext db = new NewsContext())
            {
                db.AddRange(news);
                db.SaveChanges();
            }
        }
        public static void CreateNews(News news)
        {
            using (NewsContext db = new NewsContext())
            {
                db.Add(news);
                db.SaveChanges();
            }
        }

        public static List<News> GetAllNews()
        {
            using (NewsContext db = new NewsContext())
            {
                List<News> list = db.News.ToList();
                return list;
            }
        }

        public static void EmptyDatabase()
        {
            using (NewsContext db = new NewsContext())
            {
                List<News> list = db.News.ToList();
                db.News.RemoveRange(list);
                db.SaveChanges();
            }
        }
    }
}
