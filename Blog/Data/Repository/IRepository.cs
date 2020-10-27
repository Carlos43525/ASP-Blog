using Blog.Models;
using Blog.Models.Comments;
using Blog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Repository
{
    // Interface for the Repository actions.
    public interface IRepository
    {
        Post GetPost(int Id);
        List<Post> GetAllPosts();
        IndexViewModel GetAllPosts(int pageNumber, string category);
        void AddPost(Post post);
        void UpdatePost(Post post);
        void RemovePost(int Id);

        void AddSubComment(SubComment comment);

        Task<bool> SaveChangesAsync();

    }
}
