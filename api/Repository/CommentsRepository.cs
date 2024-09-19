using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Comments;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly ApplicationDBContext _context;
        public CommentsRepository(ApplicationDBContext context)
        {
             _context = context;
        }
        public async Task<List<Comment>> GetAllAsync(CommentQueryObject queryObject)
        {
            var comments = _context.Comment.Include(a => a.AppUser).AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryObject.Symbol))
            {
                comments = comments.Where(s => s.Stock.Symbol == queryObject.Symbol);
            };
            if (queryObject.IsDescending == true)
            {
                comments = comments.OrderByDescending(c => c.CreatedOn);
            }
            return await comments.ToListAsync();
        }


        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comment.Include(a => a.AppUser).FirstOrDefaultAsync(c => c.Id == id);
        }
          public async Task<Comment?> UpdateAsync(int id, Comment commentModle)
        {
           var existingComment = await _context.Comment.FindAsync(id);
           if(existingComment == null)
           {
                return null;
           }
            existingComment.Title = commentModle.Title;
            existingComment.Content = commentModle.Content;          
           
            await _context.SaveChangesAsync();
            return existingComment;
        }
         public async Task<Comment> CreateAsync(Comment commentModel)
        {
            await _context.Comment.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var commentModel = await _context.Comment.FirstOrDefaultAsync(x => x.Id == id);
            if(commentModel == null)
            {
                return null;
            }
            _context.Comment.Remove(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

    }
}