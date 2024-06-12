using Jmeza44.EtherBlog.Application.Common.Exceptions;
using Jmeza44.EtherBlog.Application.Common.Interfaces;
using Jmeza44.EtherBlog.Domain.Entities;
using MediatR;

namespace Jmeza44.EtherBlog.Application.Main.PostRequest.Commands.DeletePost
{
    public class DeletePostCommand : IRequest<bool>
    {
        public required int Id { get; set; }

        public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, bool>
        {
            private readonly IApplicationDbContext _dbContext;

            public DeletePostCommandHandler(IApplicationDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<bool> Handle(DeletePostCommand request, CancellationToken cancellationToken)
            {
                var post = await _dbContext.Posts.FindAsync(new object[] { request.Id }, cancellationToken);
                if (post == null)
                {
                    throw new NotFoundException(nameof(Post), request.Id);
                }

                _dbContext.Posts.Remove(post);
                var deleted = await _dbContext.SaveChangesAsync(cancellationToken);
                return deleted > 0;
            }
        }
    }
}
