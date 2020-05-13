using Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ApplicationServices.Services
{
    public class NewsService : INewsService
    {
        public List<News> GetNews()
        {
            List<News> a = new List<News>();
            a.Add(new News { NewsText = "Test" });
            return a;
        }
    }
}
