using Core.DomainServices;
using Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ApplicationServices.Services
{
    public class NewsService : INewsService
    {
        INewsRepository _newsRepository;
        public NewsService(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public List<News> GetNews()
        {
            return _newsRepository.GetNews();
        }
    }
}
