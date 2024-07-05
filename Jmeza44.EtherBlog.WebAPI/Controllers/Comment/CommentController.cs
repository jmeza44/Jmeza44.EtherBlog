using Jmeza44.EtherBlog.Application.Common.DTOs;
using Jmeza44.EtherBlog.Application.Main.CommentRequest.Commands.AddComment;
using Jmeza44.EtherBlog.Application.Main.CommentRequest.Commands.DeleteComment;
using Jmeza44.EtherBlog.Application.Main.CommentRequest.Commands.EditComment;
using Jmeza44.EtherBlog.Application.Main.CommentRequest.Queries.GetPostComments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jmeza44.EtherBlog.WebApi.Controllers.Comment
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CommentController : ApiController
    {
        [HttpGet("Post/{postId}")]
        [Authorize(Roles = "Admin, Editor, Viewer")]
        public async Task<ActionResult<List<CommentDto>>> GetPostComments(int postId)
        {
            var query = new GetPostCommentsQuery { PostId = postId };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("Add")]
        [Authorize(Roles = "Editor, Viewer")]
        public async Task<ActionResult<bool>> AddComment([FromBody] AddCommentCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("Edit/{commentId}")]
        [Authorize(Roles = "Editor, Viewer")]
        public async Task<ActionResult<CommentDto>> EditComment(int commentId, [FromBody] EditCommentCommand command)
        {
            command.Id = commentId;
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("Delete/{commentId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> DeleteComment(int commentId)
        {
            var command = new DeleteCommentCommand { Id = commentId };
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}
