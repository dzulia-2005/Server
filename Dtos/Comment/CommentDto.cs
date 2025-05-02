namespace Server.Dto.Comment
{
    public class CommentDto
    {
        public int ID { get; set; }
        public string Title {get;set;}=string.Empty;
        public string Content {get;set;} = string.Empty;
        public DateTime CreateOn {get;set;} = DateTime.Now;
        public int? StockID { get; set; }
        
        public string CreaateBy { get; set; } = string.Empty;

    }
}