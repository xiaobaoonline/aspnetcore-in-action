
using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore;

namespace MyNetCoreAPI.Models
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options)
              : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        //自定义DbContext实体属性名与数据库表对应名称（默认 表名与属性名对应是 User与Users）
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
        }
    }
}

