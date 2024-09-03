using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface IPostRepository
    {
        void Add(Post post);
        void Delete(int id); // Method to delete a post by its ID
        List<Post> GetAllPublishedPosts();
        Post GetPublishedPostById(int id);
        List<Post> GetUserPostsByUserProfileId(int userProfileId);
        void Update(Post post);
    }
}