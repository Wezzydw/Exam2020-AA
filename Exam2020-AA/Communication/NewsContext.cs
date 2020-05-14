
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exam2020_AA.Communication
{
    public class NewsContext : DbContext
    {
        public DbSet<News> news { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=news.db");
    }
}
