namespace MyLabProject
{
    public interface IPostRepository
    {
        IEnumerable<Post> Get();
        Post Get(int id);
        void Create(Post post);
        void Update(Post post);
        Post Delete(int id);
    }
}
