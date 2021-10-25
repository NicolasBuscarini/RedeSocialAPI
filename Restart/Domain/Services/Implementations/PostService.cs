using Restart.Domain.Models;
using Restart.Domain.Services.Interfaces;
using Restart.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restart.Domain.Services.Implementations
{
    public class PostService : IPostService
    {
        private readonly PostRepository _postRepository;
        private readonly IAuthService _authService;
        private readonly LikeRepository _likeRepository;


        public PostService(PostRepository postRepository, IAuthService authService, LikeRepository likeRepository)
        {
            _authService = authService;
            _postRepository = postRepository;
            _likeRepository = likeRepository;
        }

        public async Task<List<Post>> ListPosts()
        {
            List<Post> list = await _postRepository.ListPosts();

            return list;
        }

        public async Task<List<Post>> ListMeusPosts()
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();

            List<Post> list = await _postRepository.ListPostsByApplicationUserId(currentUser.Id);

            return list;
        }

        public async Task<Post> GetPost(int postId)
        {
            Post post = await _postRepository.GetPostById(postId);

            if (post == null)
                throw new ArgumentException("Post não existe!");

            return post;
        }

        public async Task<Post> NovoPost(Post post)
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();

            Post novoPost = new Post();

            novoPost.ApplicationUserId = currentUser.Id;
            novoPost.Data = DateTime.Now;
            novoPost.Titulo = post.Titulo;
            novoPost.Conteudo = post.Conteudo;

            novoPost = await _postRepository.CreatePost(novoPost);

            return novoPost;
        }

        public async Task<int> UpdatePost(Post post)
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();

            Post findPost = await _postRepository.GetPostById(post.Id);
            if (findPost == null)
                throw new ArgumentException("Post não existe!");

            if (!findPost.ApplicationUserId.Equals(currentUser.Id))
                throw new ArgumentException("Sem permissão.");

            findPost.Titulo = post.Titulo;
            findPost.Conteudo = post.Conteudo;

            return await _postRepository.UpdatePost(findPost);
        }

        public async Task<bool> DeletePostAsync(int postId)
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();

            Post findPost = await _postRepository.GetPostById(postId);
            if (findPost == null)
                throw new ArgumentException("Post não existe!");

            if (!findPost.ApplicationUserId.Equals(currentUser.Id))
                throw new ArgumentException("Sem permissão.");

            await _postRepository.DeletePostAsync(postId);

            return true;
        }
        public async Task<bool> LikePost(int postId)
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();

            Post post = await _postRepository.GetPostById(postId);

            if (post == null)
                throw new ArgumentException("Post não existe!");

            Like like = await _likeRepository.GetLikeByPostIdAndApplicationUserId(postId, currentUser.Id);

            if (like != null)
                throw new ArgumentException("Já deu like neste post.");

            like = new Like();

            like.PostId = postId;
            like.Data = DateTime.Now;
            like.ApplicationUserId = currentUser.Id;
            await _likeRepository.CreateLike(like);

            post.CountLikes += 1;
            await _postRepository.UpdatePost(post);

            return true;
        }

        public async Task<bool> UnlikePost(int postId)
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();

            Post post = await _postRepository.GetPostById(postId);

            if (post == null)
                throw new ArgumentException("Post não existe!");

            //if (!post.LikesPost.Any(p => p.ApplicationUserId == currentUser.Id))
            //    throw new ArgumentException("Usuário não deu like para ser removido!");

            Like like = await _likeRepository.GetLikeByPostIdAndApplicationUserId(postId, currentUser.Id);
            
            if (like == null)
                throw new ArgumentException("Usuário não deu like para ser removido!"); 

            await _likeRepository.UnlikePost(postId, currentUser.Id);

            post.CountLikes -= 1;
            await _postRepository.UpdatePost(post);

            return true;
        }

        public async Task<List<Like>> ListLikesByPost(int postId)
        {
            List<Like> list = await _likeRepository.ListLikesByPostId(postId);

            return list;
        }

        public async Task<Like> GetLike(int likeId)
        {
            Like like = await _likeRepository.GetLikeById(likeId);

            if (like == null)
                throw new ArgumentException("Like não existe!");

            return like;
        }
    }
}