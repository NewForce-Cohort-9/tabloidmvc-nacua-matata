using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ICommentRepository
    {
        List<Comment> GetAllPostComments(int postId);
        void Add(Comment comment);

        void Delete(int id);
        Comment GetUserCommentById(int id, int userProfileId);
    }
}
