namespace LabProject
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public IList<Tag> Tags { get; set; } = new List<Tag>();
        public DateTime dateofCreation { get; set; } = DateTime.Now;
        public User User { get; set; }
        public Post(int id, string title, string body, IList<Tag> tags, User user)
        {
            Id = id;
            Title = title;
            Body = body;
            Tags = tags;
            dateofCreation = DateTime.Now;
            User = user;
        }
    }
}
