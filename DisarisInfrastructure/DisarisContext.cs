using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace DisarisInfrastructure
{
    public class DisarisContext : DbContext
    {
        public DbSet<Tag> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseMySql("server=localhost;user=root;password=W1@inter;database=disarisdb;port=3306;Connect Timeout=5;");
    }
}
