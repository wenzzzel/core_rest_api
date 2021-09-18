using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using core_rest_api.Models;

//TODO: Maybe move this to the configurations folder instead?
namespace core_rest_api
{
    public class MyDbContext : IdentityDbContext
    {
        public DbSet<HighScore> HighScore { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base (options)
        {
            
        }
    }
}