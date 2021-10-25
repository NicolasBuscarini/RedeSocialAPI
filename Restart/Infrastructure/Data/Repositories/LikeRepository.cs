using Microsoft.EntityFrameworkCore;
using Restart.Domain.Models;
using Restart.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restart.Infrastructure.Data.Repositories
{
    public class LikeRepository
    {
        private readonly MySQLContext _context;
        public LikeRepository(MySQLContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Like>> ListLikes()
        {
            List<Like> list = await _context.Like.OrderBy(p => p.Data).Include(l => l.ApplicationUser).Include(l => l.Post).ToListAsync();

            return list;
        }

        public async Task<List<Like>> ListLikesByPostId(int likePostId)
        {
            List<Like> list = await _context.Like.Where(l => l.PostId.Equals(likePostId)).OrderBy(l => l.Data).ToListAsync();


            return list;
        }

        public async Task<Like> GetLikeById(int likeId)
        {
            Like like = await _context.Like.Include(l => l.ApplicationUser).Include(l => l.Post).FirstOrDefaultAsync((l => l.Id == likeId));

            return like;
        }

        public async Task<Like> CreateLike(Like like)
        {
            var ret = await _context.Like.AddAsync(like);

            await _context.SaveChangesAsync();

            ret.State = EntityState.Detached;

            return ret.Entity;
        }

        public async Task<int> UpdateLike(Like like)
        {
            _context.Entry(like).State = EntityState.Modified;

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteLikeAsync(int likeId)
        {
            var item = await _context.Post.FindAsync(likeId);
            _context.Post.Remove(item);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UnlikePost(int postId, string currentUser)
        {
            var post = await _context.Post.FindAsync(postId);

            var like = post.LikesPost.Find(l => l.PostId == postId && l.ApplicationUserId == currentUser);
            _context.Like.Remove(like);

            await _context.SaveChangesAsync();

            return true;
        }
        
        public async Task<Like> GetLikeByPostIdAndApplicationUserId(int postId, string currentUserId)
        {
            Like like = await _context.Like.FirstOrDefaultAsync(l => l.PostId == postId && l.ApplicationUserId == currentUserId);

            return like;
        }

    }
}
