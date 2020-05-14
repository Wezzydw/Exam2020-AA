using Core.DomainServices;
using Core.Entity;
using Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class NewsRepository : INewsRepository
    {
        public List<News> GetNews()
        {
            List<News> news = new List<News>();
            return AsyncClient.StartClient(true, news);
        }
        public void SaveNews(List<News> news)
        {
            AsyncClient.StartClient(false, news);
        }
    }
}
