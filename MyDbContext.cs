using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace core_rest_api{
    public class MyDbContext : DbContext{
        public DbSet<HighScore> HighScore { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options){

        }
    }
}