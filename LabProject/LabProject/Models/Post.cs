namespace LabProject
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public IList<Tag> Tags { get; set; } = new List<Tag>();
        public DateTime dateofCreation { get; set; } = DateTime.Now;
        public Author Author { get; set; }
        public int AuthorId { get; set; } = 0;
        public Post(int id, string title, string body, int authorId = 0)
        {
            Id = id;
            Title = title;
            Body = body;
            dateofCreation = DateTime.Now;
            AuthorId = authorId;
        }
    }
}
