using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Comments;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface ICommentsRepository
    {
        Task<List<Comment>> GetAllAsync(CommentQueryObject queryObject);
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment?> UpdateAsync(int id, Comment commnetModel);
        Task<Comment> CreateAsync(Comment commnetModel);
        Task<Comment?> DeleteAsync(int id);

    }
}