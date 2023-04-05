using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

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
            var resTags = await _tagService.GetTags();
            var tags = resTags.Value.ToList();
            var response = new BaseResponse<Tag>()
            {
                Description = "Success",
                StatusCode = 200,
                Values = tags
            };
            return Ok(response);
        }

        [HttpGet("{id}", Name = "GetTag")]
        public async Task<ActionResult<Tag>> GetTag(int id)
        {
            var resTag = await _tagService.GetTag(id);
            var tag = resTag.Value;
            var response = new BaseResponse<Tag>()
            {
                Description = "Success",
                StatusCode = 200,
                Values = new List<Tag> { tag }
            };
            return Ok(response);
        }

        [Authorize]
        [HttpPost(Name = "CreateTag")]
        public async Task<ActionResult<Tag>> PostTag(Tag tag)
        {
            await _tagService.PostTag(tag);
            var response = new BaseResponse<Tag>()
            {
                Description = "Created",
                StatusCode = 201,
                Values = new List<Tag> { tag }
            };
            return Ok(response);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<Tag>> PutTag(int id, Tag tag)
        {
            var existingTag = await _tagService.PutTag(id, tag);
            var response = new BaseResponse<Tag>()
            {
                Description = "Success",
                StatusCode = 200,
                Values = new List<Tag> { tag }
            };
            return Ok(response);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            await _tagService.DeleteTag(id);
            var response = new BaseResponse<Tag>()
            {
                Description = "No content",
                StatusCode = 204
            };
            return Ok(response);
        }
    }
}
