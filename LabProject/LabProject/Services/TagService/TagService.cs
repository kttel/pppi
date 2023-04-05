using Microsoft.AspNetCore.Mvc;
using LabProject;

namespace LabProject
{
    public class TagService : ITagService
    {
        private static readonly List<Tag> tags = new List<Tag>
        {
            new Tag(1, "Python", "Hyperfixation language"),
            new Tag(2, "SQL", "Sample description"),
            new Tag(3, "C#", "Simple text example"),
            new Tag(4, "Django", "Favourite one!!"),
            new Tag(5, "ElasticSearch", "Why you're so hard..."),
            new Tag(6, "Redis", "Just install Ubuntu"),
            new Tag(7, "WebSockets", "One of the osi protocols"),
            new Tag(8, "JavaScript", "Simple description"),
            new Tag(9, "PostgreSQL", "SQL RDBMS"),
            new Tag(10, "MongoDB", "NoSQL time")
        };
        public async Task<IActionResult> DeleteTag(int id)
        {
            var result = await Task.Run(() => tags.RemoveAll(x => x.Id == id));
            return new OkObjectResult(result);
        }

        public async Task<ActionResult<Tag>> GetTag(int id)
        {
            var tag = tags.Find(x => x.Id == id);
            await Task.FromResult(tag);
            return tag;
        }

        public async Task<ActionResult<IEnumerable<Tag>>> GetTags()
        {
            await Task.FromResult(tags);
            return tags;
        }

        public async Task<ActionResult<Tag>> PostTag(Tag tag)
        {
            await Task.Run(() =>
            {
                tags.Add(tag);
            });
            return tag;
        }

        public async Task<ActionResult<Tag>> PutTag(int id, Tag tag)
        {
            var existingTag = tags.FirstOrDefault(x => x.Id == id);

            await Task.Run(() =>
            {
                existingTag.Name = tag.Name;
                existingTag.Description = tag.Description;
            });
            return new NoContentResult();
        }
    }
}
