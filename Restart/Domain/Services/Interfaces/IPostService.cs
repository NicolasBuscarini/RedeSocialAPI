using Restart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restart.Domain.Services.Interfaces
{
    public interface IPostService
    {
        Task<List<Post>> ListPosts();
        Task<List<Post>> ListMeusPosts();
        Task<Post> GetPost(int postId);
        Task<Post> NovoPost(Post post);
        Task<int> UpdatePost(Post post);
        Task<bool> DeletePostAsync(int postId);
        Task<bool> LikePost(int postId);
        Task<bool> UnlikePost(int postId);
        Task<List<Like>> ListLikesByPost(int postId);
        Task<Like> GetLike(int likeId);
        
    }
}
