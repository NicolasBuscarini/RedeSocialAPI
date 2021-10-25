using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restart.Domain.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Restart.Infrastructure.Data.Context
{
    public class MySQLContext : IdentityDbContext<ApplicationUser>, IQueryable
    {
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options)
        {
        }

        public DbSet<Post> Post { get; set; }
        public DbSet<Like> Like { get; set; }
        public DbSet<Chat> Chat { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<ApplicationUser> User { get; set; }
        public DbSet<ApplicationRole> Role { get; set; }

        public Type ElementType => throw new NotImplementedException();

        public Expression Expression => throw new NotImplementedException();

        public IQueryProvider Provider => throw new NotImplementedException();

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(t => t.Id);

            modelBuilder.Entity<Post>();

            modelBuilder.Entity<Chat>().HasOne<ApplicationUser>(p => p.UserChatReceiver)
            .WithMany(a => a.ChatsReceiver)
            .HasForeignKey(c => c.UserChatReceiverId);

            modelBuilder.Entity<Chat>().HasOne<ApplicationUser>(p => p.UserChatOwner)
            .WithMany(a => a.ChatsSender)
            .HasForeignKey(c => c.UserChatOwnerId);

            modelBuilder.Entity<Message>().HasOne<ApplicationUser>(m => m.UserMessageReceiver)
            .WithMany(u => u.MessagesReceiver)
            .HasForeignKey(c => c.UserMessageReceiverId);

            modelBuilder.Entity<Message>().HasOne<ApplicationUser>(p => p.UserMessageOwner)
            .WithMany(a => a.MessagesSender)
            .HasForeignKey(c => c.UserMessageOwnerId);

        }
    }
}
