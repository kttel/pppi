﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace LabProject.Intefraces
{
    public interface ITagService
    {
        Task<ActionResult<IEnumerable<Tag>>> GetTags();
        Task<ActionResult<Tag>> GetTag(int id);
        Task<ActionResult<Tag>> PostTag(Tag tag);
        Task<ActionResult<Tag>> PutTag(int id, Tag tag);
        Task<IActionResult> DeleteTag(int id);
    }
}
