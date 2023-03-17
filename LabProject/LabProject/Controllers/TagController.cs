using Microsoft.AspNetCore.Mvc;

namespace LabProject
{
    [ApiController]
    [Route("[controller]s")]
    public class TagController : ControllerBase
    {
        private readonly TagService _tagService;


    public TagController(TagService tagService)
        {
            _tagService = tagService;
        }
        [HttpGet(Name = "GetAllTags")]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTags()
        {
            var tags = await _tagService.GetTags();
            return tags;
        }
        [HttpGet("{id}", Name = "GetTag")]
        public async Task<ActionResult<Tag>> GetTag(int id)
        {
            var tag = await _tagService.GetTag(id);
            return tag;
        }

        [HttpPost(Name = "CreateTag")]
        public async Task<ActionResult<Tag>> PostTag(Tag tag)
        {
            await _tagService.PostTag(tag);
            return CreatedAtAction(nameof(GetTag), new { id = tag.Id }, tag);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Tag>> PutTag(int id, Tag tag)
        {
            var existingTag = await _tagService.PutTag(id, tag);
            return await _tagService.GetTag(id);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            await _tagService.DeleteTag(id);
            return NoContent();
        }
    }
}
