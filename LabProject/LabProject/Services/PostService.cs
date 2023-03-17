using Azure;
using Microsoft.AspNetCore.Mvc;

namespace LabProject
{
    public class PostService : IPostService
    {
        private static readonly User testUser = new User(1, "TestUser", "test@example.com");
        private static readonly List<Post> posts = new List<Post>
        {
            new Post(1, "Title", "Sample body", new List<Tag>(), testUser),
            new Post(2, "Second", "Sample description", new List<Tag>(), testUser),
            new Post(3, "Third", "Sample body!!", new List<Tag>(), testUser),
            new Post(4, "Example 4...", "Sample-example", new List<Tag>(), testUser),
            new Post(5, "Python Tutorial", "Get along with Django", new List<Tag>(), testUser),
            new Post(6, "Another tutorial", "Tutorial", new List<Tag>(), testUser),
            new Post(7, "Python lovers club", "Body example", new List<Tag>(), testUser),
            new Post(8, "Almost done", "Description!", new List<Tag>(), testUser),
            new Post(9, "C# vs Python", "Some battle?", new List<Tag>(), testUser),
            new Post(10, "Too much work", "I want to sleep", new List<Tag>(), testUser)
        };
        public async Task<IActionResult> DeletePost(int id)
        {
            var result = await Task.Run(() => posts.RemoveAll(x => x.Id == id));
            return new OkObjectResult(result);
        }

        public async Task<ActionResult<Post>> GetPost(int id)
        {
            var post = posts.Find(x => x.Id == id);
            await Task.FromResult(post);
            return post;
        }

        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            await Task.FromResult(posts);
            return posts;
        }

        public async Task<ActionResult<Post>> PostPost(Post post)
        {
            await Task.Run(() =>
            {
                posts.Add(post);
            });
            return post;
        }

        public async Task<ActionResult<Post>> PutPost(int id, Post post)
        {
            var existingPost = posts.FirstOrDefault(x => x.Id == id);

            await Task.Run(() =>
            {
                existingPost.Title = post.Title;
                existingPost.Body = post.Body;
            });
            return new NoContentResult();
        }
    }
}
