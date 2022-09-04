namespace BlogProject.DTO.DTOs.Comment
{
    public class CommentListDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public DateTime PostedTime { get; set; }
        public Nullable<int> ParentCommentId { get; set; }

        public List<CommentListDto> SubComments { get; set; }

        public int BlogId { get; set; }
    }
}
