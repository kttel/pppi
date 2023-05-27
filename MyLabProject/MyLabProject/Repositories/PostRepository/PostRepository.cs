namespace MyLabProject
{
    public class PostRepository : IPostRepository
    {
        private ApplicationDbContext Context;
        public IEnumerable<Post> Get()
        {
            return Context.Posts;
        }
        public Post Get(int Id)
        {
            return Context.Posts.Find(Id);
        }
        public PostRepository(ApplicationDbContext context)
        {
            Context = context;
        }
        public void Create(Post post)
        {
            Context.Posts.Add(post);
            Context.SaveChanges();
        }
        public void Update(Post updatedPost)
        {
            Post currentPost = Get(updatedPost.Id);
            currentPost.Title = currentPost.Title;
            currentPost.Body = currentPost.Body;

            Context.Posts.Update(currentPost);
            Context.SaveChanges();
        }

        public Post Delete(int Id)
        {
            Post post = Get(Id);

            if (post != null)
            {
                Context.Posts.Remove(post);
                Context.SaveChanges();
            }

            return post;
        }
    }
}
