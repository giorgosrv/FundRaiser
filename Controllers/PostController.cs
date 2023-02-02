using FundRaiser.Exceptions;
using Microsoft.AspNetCore.Mvc;
using FundRaiser.Services;
using FundRaiser.DTOs;
using FundRaiser.DTOs.Posts;

namespace FundRaiser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }
        [HttpGet]
        public async Task<ActionResult<List<GetPostDto>>> Get()
        {
            var post = await _postService.GetPosts();
            return Ok(post);
        }
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetPostDto>> Get(Guid id)
        {
            try
            {
                var post = await _postService.GetPost(id);
                return Ok(post);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CreatePostDto>> AddPost(CreatePostDto post)
        {

            var retPost = await _postService.AddPost(post);
            return Ok(retPost);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeletePost(Guid id)
        {
            try
            {
                await _postService.DeletePost(id);
            return Ok("Post deleted successfully");
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPatch, Route("{id}")]
        public async Task<ActionResult> UpdatePost(Guid id, UpdatePostDto post)
        {
            try
            {
                var updatedPost = await _postService.UpdatePost(id, post);
                return Ok(updatedPost);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

    }
}

