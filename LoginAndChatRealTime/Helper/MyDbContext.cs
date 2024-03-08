﻿using Microsoft.EntityFrameworkCore;
using LoginAndChatRealTime.Entities;

namespace LoginAndChatRealTime.Helper
{
    public class MyDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Message> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Server=localhost;Port=3306;Database=chat_real_time;Uid=root;Pwd=123456";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
}
