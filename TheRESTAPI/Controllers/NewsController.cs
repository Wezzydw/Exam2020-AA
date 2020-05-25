using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.ApplicationServices;
using Core.Entity;
using Microsoft.AspNetCore.Mvc;

namespace TheRESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : Controller
    {
        private INewsService _newsService;
        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }


        public ActionResult<List<News>> Get()
        {
            List<News> news = new List<News>();
            News a = new News() { NewsText = "News a" };
            News b = new News() { NewsText = "News b" };
            News c = new News() { NewsText = "News c" };
            news.Add(a);
            news.Add(b);
            news.Add(c);
            //_newsService.SaveNews(news);
            Console.WriteLine("Reached get");
            return news;
            //return _newsService.GetNews();
        }
    }
}