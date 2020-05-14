using Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DomainServices
{
    public interface INewsRepository
    {
        List<News> GetNews();
        void SaveNews(List<News> news);
    }
}
