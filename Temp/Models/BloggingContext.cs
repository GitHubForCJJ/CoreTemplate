using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Temp.Data;

namespace Temp
{
    public class BloggingContext : DbContext
    {
        public BloggingContext(DbContextOptions<BloggingContext> options)
            : base(options)
        { }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<LoginToken> LoginTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.EnableAutoHistory(null);
            modelBuilder.Entity<Post>().HasMany(x => x.Comments).WithOne(x => x.Post).HasForeignKey(x => x.Postid);
            modelBuilder.Entity<Blog>().HasMany(x => x.Posts).WithOne(x => x.Blog).HasForeignKey(x => x.BlogId);
            //分表测试
            modelBuilder.Entity<LoginToken>(table =>
            {
                table.HasKey(x => x.KID);
                table.ToTable("LoginToken_yyyymm");
                table.Property(x => x.LoginUserAccount).HasMaxLength(100).HasDefaultValue("").IsRequired();
                table.Property(x => x.Token).HasMaxLength(70).HasDefaultValue("").IsRequired();
                table.Property(x => x.LoginUserId).HasMaxLength(60).HasDefaultValue("").IsRequired();
                table.Property(x => x.IpAddr).HasMaxLength(100).HasDefaultValue("").IsRequired();
                table.Property(x => x.PlatForm).HasDefaultValue(0).IsRequired();
                table.Property(x => x.IsLogOut).HasDefaultValue(0).IsRequired();
                table.Property(x => x.LoginResult).HasMaxLength(100).HasDefaultValue("").IsRequired();
                table.Property(x => x.CreateTime).HasDefaultValue("1970-01-01 00:00:00").IsRequired();
                table.Property(x => x.UpdateTime).HasColumnType("timestamp").IsRequired();//默认就是CURRENT_TIMESTAMP值 不知道是不是mysql版本问题.HasDefaultValue("CURRENT_TIMESTAMP")
            });

            #region 种子数据
            modelBuilder.Entity<Blog>().HasData(new Blog
            {
                Id = 1,
                Url = "http://www.baidu.com",
                Title = "百度blog"
            }, new Blog
            {
                Id = 2,
                Url = "http://www.google.com",
                Title = "谷歌blo"
            });
            modelBuilder.Entity<Post>().HasData(new Post()
            {
                Id = 1,
                Content = "post content 1",
                Title = "post 1",
                BlogId=1
            });
            modelBuilder.Entity<Comment>().HasData(
                new List<Comment>()
                {
                    new Comment
                    {
                        Id=1,
                        Title="comment 1",
                        Content="Comments content1",
                        Postid=1
                    }
                });
            #endregion
        }
    }

    public class Blog
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }

        public virtual List<Post> Posts { get; set; }
    }

    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }

    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int Postid { get; set; }
        public virtual Post Post { get; set; }
    }


}
