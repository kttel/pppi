namespace LabProject
{
    public class Tag
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IList<Post> Posts { get; set; } = new List<Post>();
        public Tag(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
