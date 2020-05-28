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
      
            List<News> news = _newsService.GetNews();
            if (news != null)
            {
                return news;
            }
            else return NoContent();
         
        }
    }
}