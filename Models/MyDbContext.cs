using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

//TODO: Maybe move this to the configurations folder instead?
namespace core_rest_api{
    public class MyDbContext : IdentityDbContext{
        public DbSet<HighScore> HighScore { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base (options){
            
        }
    }
}