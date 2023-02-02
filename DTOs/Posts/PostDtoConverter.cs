using FundRaiser.Models;
namespace FundRaiser.DTOs
{
    public static class PostDtoConverter
    {
        public static GetPostDto Convert(this Post post)
        {
            return new GetPostDto()
            {
                Id = post.Id,
                Title = post.Title,
                Text = post.Text,
                ProjectId = post.Project.Id.ToString(),
                UpdatedAt = post.UpdatetAt,
                CreatedAt= post.CreatedAt
            };
        }
        public static Post Convert(this CreatePostDto post)
        {
            return new Post()
            {
                Title = post.Title,
                Text = post.Text,
                UpdatetAt = DateTime.Now
            };
        }
    }
}
