using API.Models;
using Server.Dto.Comment;

namespace Server.Mappers
{
    public static class CommentMappers
    {
        public static CommentDto ToCommentDto (this Comments commentModel)
        {
            return new CommentDto 
            {
                ID = commentModel.ID,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreateOn = commentModel.CreateOn,
                StockID = commentModel.StockID
            };
        }

        public static Comments ToCommentFromCreate(this CreateCommentDto commentDto,int stockId)
        {
            return new Comments
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                StockID = stockId
            };
        }

        public static Comments ToCommentFromUpdate(this UpdateCommentRequestDto commentDto, int id)
        {
            return new Comments
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                ID = id,
            };
        }
    }
}