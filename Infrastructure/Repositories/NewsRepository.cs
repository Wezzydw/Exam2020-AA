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
            return AsyncClient.StartClient();
        }
    }
}
