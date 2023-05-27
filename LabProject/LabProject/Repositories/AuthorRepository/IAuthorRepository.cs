namespace LabProject
{
    public interface IAuthorRepository
    {
        IEnumerable<Author> Get();
        Author Get(int id);
        void Create(Author item);
        void Update(Author item);
        Author Delete(int id);
    }
}