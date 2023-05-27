namespace MyLabProject
{
    public class AuthorRepository : IAuthorRepository
    {
        private ApplicationDbContext Context;
        public IEnumerable<Author> Get()
        {
            return Context.Authors;
        }
        public Author Get(int Id)
        {
            return Context.Authors.Find(Id);
        }
        public AuthorRepository(ApplicationDbContext context)
        {
            Context = context;
        }
        public void Create(Author author)
        {
            Context.Authors.Add(author);
            Context.SaveChanges();
        }
        public void Update(Author updatedAuthor)
        {
            Author currentAuthor = Get(updatedAuthor.Id);
            currentAuthor.Username = updatedAuthor.Username;
            currentAuthor.Email = updatedAuthor.Email;
            currentAuthor.IsActive = updatedAuthor.IsActive;
            currentAuthor.IsAdmin = updatedAuthor.IsAdmin;

            Context.Authors.Update(currentAuthor);
            Context.SaveChanges();
        }

        public Author Delete(int Id)
        {
            Author author = Get(Id);

            if (author != null)
            {
                Context.Authors.Remove(author);
                Context.SaveChanges();
            }

            return author;
        }
    }
}
