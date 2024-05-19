namespace BlogApp.Data
{
    public class BlogData : BaseEntity
    {
        public int AuthorId { get; set; } = default!;
        public string Content { get; set; } = default!;
        public string Title { get; set; } = default!;
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime DatePublished { get; set; }
    }
}
