using Jmeza44.EtherBlog.Application.Common.DTOs;
using Jmeza44.EtherBlog.Application.Main.PostRequest.Commands.CreatePost;
using Jmeza44.EtherBlog.Application.Main.PostRequest.Commands.DeletePost;
using Jmeza44.EtherBlog.Application.Main.PostRequest.Commands.EditPost;
using Jmeza44.EtherBlog.Application.Main.PostRequest.Queries.GetAllPost;
using Jmeza44.EtherBlog.Application.Main.PostRequest.Queries.GetPostById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jmeza44.EtherBlog.WebApi.Controllers.Post
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PostController : ApiController
    {
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Editor, Viewer")]
        public async Task<ActionResult<PostDto>> GetById(int id)
        {
            var query = new GetPostByIdQuery { Id = id };
            var result = await Mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("GetAll")]
        [Authorize(Roles = "Admin, Editor, Viewer")]
        public async Task<ActionResult<PaginatedData<PostDto>>> GetAll(
            [FromQuery] GetAllPostsQuery request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        [HttpPost("Create")]
        [Authorize(Roles = "Editor")]
        public async Task<ActionResult<bool>> Create(
            [FromBody] CreatePostCommand request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        [HttpPut("Edit")]
        [Authorize(Roles = "Admin, Editor")]
        public async Task<ActionResult<bool>> Edit(
            [FromBody] EditPostCommand request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> Delete(
            [FromQuery] DeletePostCommand request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }
    }
}
