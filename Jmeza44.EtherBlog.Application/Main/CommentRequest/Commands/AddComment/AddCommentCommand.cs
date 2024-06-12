using Jmeza44.EtherBlog.Application.Common.Exceptions;
using Jmeza44.EtherBlog.Application.Common.Interfaces;
using Jmeza44.EtherBlog.Domain.Entities;
using MediatR;

namespace Jmeza44.EtherBlog.Application.Main.CommentRequest.Commands.AddComment
{
    public class AddCommentCommand : IRequest<bool>
    {
        public required string Content { get; set; }
        public required int PostId { get; set; }

        public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand, bool>
        {
            private readonly IApplicationDbContext _dbContext;

            public AddCommentCommandHandler(IApplicationDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<bool> Handle(AddCommentCommand request, CancellationToken cancellationToken)
            {
                var post = await _dbContext.Posts.FindAsync(new object[] { request.PostId }, cancellationToken);
                if (post == null)
                {
                    throw new NotFoundException(nameof(Post), request.PostId);
                }

                var comment = new Comment
                {
                    Content = request.Content,
                    PostId = request.PostId,
                };

                _dbContext.Comments.Add(comment);
                var created = await _dbContext.SaveChangesAsync(cancellationToken);
                return created > 0;
            }
        }
    }
}
