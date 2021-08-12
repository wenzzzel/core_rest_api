using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace core_rest_api{
    public class MyDbContext : DbContext{
        public DbSet<HighScore> HighScore { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options){

        }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=tcp:wenzzzel.database.windows.net,1433;Initial Catalog=core_rest_api;Persist Security Info=False;User ID=wenzzzel;Password=N3r7$vsNxA;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
 
    }

    }
}