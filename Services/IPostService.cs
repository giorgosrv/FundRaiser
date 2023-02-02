using FundRaiser.DTOs;
using FundRaiser.DTOs.Posts;

namespace FundRaiser.Services

{
public interface IPostService
    {
        public Task<GetPostDto> GetPost(Guid id);
        public Task<List<GetPostDto>> GetPosts();
        public Task<GetPostDto> AddPost(CreatePostDto post);
        public Task<bool> DeletePost(Guid id);
        public Task<GetPostDto> UpdatePost(Guid id, UpdatePostDto post);
    }
}
