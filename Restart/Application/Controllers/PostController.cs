
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restart.Domain.Models;
using Restart.Domain.Services.Implementations;
using Restart.Domain.Services.Interfaces;
using Restart.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restart.Application.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("list-posts")]
        public async Task<ActionResult> ListPosts()
        {
            try
            {
                List<Post> list = await _postService.ListPosts();

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("list-meus-posts")]
        public async Task<ActionResult> ListMeusPosts()
        {
            try
            {
                List<Post> list = await _postService.ListMeusPosts();

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("get-post")]
        public async Task<ActionResult> GetPost([FromQuery] int postId)
        {
            try
            {
                Post post = await _postService.GetPost(postId);

                return Ok(post);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("novo-post")]
        public async Task<ActionResult> NovoPost([FromBody] Post post)
        {
            try
            {
                post = await _postService.NovoPost(post);

                return Ok(post);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("update-post")]
        public async Task<ActionResult> UpdatePost([FromBody] Post post)
        {
            try
            {
                return Ok(await _postService.UpdatePost(post));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("delete-post")]
        public async Task<ActionResult> DeletePost([FromBody] int postId)
        {
            try
            {
                return Ok(await _postService.DeletePostAsync(postId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("like-post")]
        public async Task<ActionResult> LikePost([FromBody] int postId)
        {
            try
            {
                return Ok(await _postService.LikePost(postId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("unlike-post")]
        public async Task<ActionResult> UnlikePost([FromBody] int postId)
        {
            try
            {
                return Ok(await _postService.UnlikePost(postId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("list-likes-post")]
        public async Task<ActionResult> ListMeusLike([FromBody] int postId)
        {
            try
            {
                List<Like> list = await _postService.ListLikesByPost(postId);

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-like")]
        public async Task<ActionResult> GetLike([FromQuery] int likeId)
        {
            try
            {
                Like like = await _postService.GetLike(likeId);

                return Ok(like);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}