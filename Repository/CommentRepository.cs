using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Models;
using Server.Interfaces;
using Microsoft.EntityFrameworkCore;
using Server.Mappers;


namespace Server.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBcontext _context;

        public CommentRepository(ApplicationDBcontext context)
        {
            _context = context;
        }

        public async Task<List<Comments>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }
        
        public async Task<Comments?> GetByIdAsync(int id)
        {
            return await _context.Comments.FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<Comments?> CreateAsync(Comments commentModel)
        {
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<Comments?> UpdateAsync(int id,Comments commentsModel)
        {
            var exsitingComment = await _context.Comments.FindAsync(id);
            if (exsitingComment==null)
            {
                return null;
            }

            exsitingComment.Content = commentsModel.Content;
            exsitingComment.Title = commentsModel.Title;
            await _context.SaveChangesAsync();
            return exsitingComment;
        }

        public async Task<Comments?> DeleteAsync(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(x=>x.ID == id);
            if (comment == null)
            {
                return null;
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;
        }
        
    }
}