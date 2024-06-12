using Jmeza44.EtherBlog.Application.Common.Interfaces;
using MediatR;

namespace Jmeza44.EtherBlog.Application.Main.CommentRequest.Commands.EditComment
{
    public class EditCommentCommand : IRequest<bool>
    {
        public required int Id { get; set; }
        public required string Content { get; set; }

        public class EditCommentCommandHandler : IRequestHandler<EditCommentCommand, bool>
        {
            private readonly IApplicationDbContext _dbContext;

            public EditCommentCommandHandler(IApplicationDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<bool> Handle(EditCommentCommand request, CancellationToken cancellationToken)
            {
                var comment = await _dbContext.Comments.FindAsync(new object[] { request.Id }, cancellationToken);
                if (comment == null)
                {
                    return false; // or throw new NotFoundException(nameof(Comment), request.Id);
                }

                comment.Content = request.Content;

                _dbContext.Comments.Update(comment);
                var updated = await _dbContext.SaveChangesAsync(cancellationToken);
                return updated > 0;
            }
        }
    }
}
