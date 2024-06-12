using Jmeza44.EtherBlog.Application.Common.Exceptions;
using Jmeza44.EtherBlog.Application.Common.Interfaces;
using Jmeza44.EtherBlog.Domain.Entities;
using MediatR;

namespace Jmeza44.EtherBlog.Application.Main.CommentRequest.Commands.DeleteComment
{
    public class DeleteCommentCommand : IRequest<bool>
    {
        public required int Id { get; set; }

        public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, bool>
        {
            private readonly IApplicationDbContext _dbContext;

            public DeleteCommentCommandHandler(IApplicationDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<bool> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
            {
                var comment = await _dbContext.Comments.FindAsync(new object[] { request.Id }, cancellationToken);
                if (comment == null)
                {
                    throw new NotFoundException(nameof(Comment), request.Id);
                }

                _dbContext.Comments.Remove(comment);
                var deleted = await _dbContext.SaveChangesAsync(cancellationToken);
                return deleted > 0;
            }
        }
    }
}
