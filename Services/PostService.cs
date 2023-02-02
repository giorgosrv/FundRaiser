using Microsoft.EntityFrameworkCore;
using FundRaiser.DTOs;
using FundRaiser.Exceptions;
using FundRaiser.Data;
using FundRaiser.DTOs.Posts;

namespace FundRaiser.Services
{
    public class PostService : IPostService
    {
        private readonly FundRaiserContext _context;
        public PostService(FundRaiserContext context)
        {
            _context = context;
        }
        public async Task<GetPostDto> AddPost(CreatePostDto post)
        {
            var projectFound = await _context.Projects.SingleOrDefaultAsync(p => p.Id == post.ProjectId);
            if (projectFound is null) throw new NotFoundException("Project not found.");

            var createdPost = post.Convert();
            createdPost.Project = projectFound;
            projectFound.Posts.Add(createdPost);
            projectFound.UpdatetAt = DateTime.Now;

            var addedPost = await _context.Posts!.AddAsync(createdPost);
            await _context.SaveChangesAsync();

            return addedPost.Entity.Convert();
        }

        public async Task<bool> DeletePost(Guid id)
        {
            var postExists = await _context
                .Posts!
                .SingleOrDefaultAsync(b => b.Id.Equals(id));
            if (postExists == null)
            {
                throw new NotFoundException("Post not found.");
            }
            _context.Posts!.Remove(postExists);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<GetPostDto> GetPost(Guid id)
        {
            var post = await _context.Posts!
                .Include(p => p.Project)
                .SingleOrDefaultAsync(b => b.Id.Equals(id));
            if (post == null) throw new NotFoundException("Post not found.");
            return post.Convert();
        }

        public async Task<List<GetPostDto>> GetPosts()
        {
            return await _context.Posts!
                .Include(p => p.Project)
                .Select(b => b.Convert())
                .ToListAsync();
        }

        public async Task<GetPostDto> UpdatePost(Guid id, UpdatePostDto updatedPost)
        {
            var existingPost = await _context.Posts!
                .SingleOrDefaultAsync(b => b.Id == id);
            if (existingPost == null) throw new NotFoundException("Post not found.");

            if (updatedPost.Title is not null) existingPost.Title = updatedPost.Title;
            if (updatedPost.Text is not null) existingPost.Text = updatedPost.Text;

            existingPost.UpdatetAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return existingPost.Convert();

        }
    }
}
