using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Comments;
using api.Models;

namespace api.Mappers
{
    public static class CommentsMapper
    {
         public static CommentsDto toCommentsDto(this Comment commentModel)
        {
            return new CommentsDto
            {
                Id = commentModel.Id,
                StockId = commentModel.StockId,
                Title = commentModel.Title,
                CreatedOn = commentModel.CreatedOn,
                Content = commentModel.Content,
                CreatedBy = commentModel.AppUser.UserName
                
            };
        }
      public static Comment toCommentFromCreate(this CreateCommentsRequestDto commentDto, int stockId )
        {
            return new Comment
            {
               
                StockId = stockId,
                Title = commentDto.Title,
                Content = commentDto.Content
                
            };
        }
        public static Comment toCommentFromCUpdate(this UpdateCommentRequestDto commentDto )
        {
            return new Comment
            {
              
                Title = commentDto.Title,
                Content = commentDto.Content
                
            };
        }
    }
}