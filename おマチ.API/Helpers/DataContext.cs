﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using おマチ.API.Entities;

namespace おマチ.API.Helpers
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql db
            // options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
            options.UseMySql(Configuration.GetConnectionString("WebApiDatabase"), ServerVersion.AutoDetect(Configuration.GetConnectionString("WebApiDatabase")));
        }

        public DbSet<User> Users { get; set; }
    }
}
