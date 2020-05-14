using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exam2020_AA.Communication
{
    public class CommunicationRepository
    {
        public static void CreateNews(List<News> news)
        {
            using (NewsContext db = new NewsContext())
            {
                db.AddRange(news);
                db.SaveChanges();
            }
        }

        public static List<News> GetAllNews()
        {
            using (NewsContext db = new NewsContext())
            {
                return db.news.ToList();
            }
        }
    }
}
