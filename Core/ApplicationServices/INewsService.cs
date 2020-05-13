using Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ApplicationServices
{
    public interface INewsService
    {
        List<News> GetNews();
    }
}
