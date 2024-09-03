using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface IPostRepository
    {
        void Add(Post post);
        void Delete(int id);
        List<Post> GetAllPublishedPosts();
        Post GetPublishedPostById(int id);
        Post GetUserPostById(int id, int userProfileId);
<<<<<<< HEAD

        void Delete(int id); // Method to delete a post by its ID
        List<Post> GetPostsByUser(int userId); // Method to get all posts by a specific user
=======
        List<Post> GetUserPostsByUserProfileId(int userProfileId);
>>>>>>> main
    }
}