using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface IPostRepository
    {
        void Add(Post post);
        void Update(Post post);
        void Delete(int id);
        List<Post> GetAllPublishedPosts();
        Post GetPostById(int id);
        Post GetPublishedPostById(int id);
        List<Post> GetUserPostsByUserProfileId(int userProfileId);
        Post? GetUserPostById(int id, int userId);
    }
}