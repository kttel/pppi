namespace MyLabProject
{
    public interface IAuthorRepository
    {
        IEnumerable<Author> Get();
        Author Get(int id);
        void Create(Author author);
        void Update(Author author);
        Author Delete(int id);
    }
}
