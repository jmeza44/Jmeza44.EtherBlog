using Jmeza44.EtherBlog.Application.Common.Exceptions;
using Jmeza44.EtherBlog.Application.Common.Interfaces;
using Jmeza44.EtherBlog.Domain.Entities;
using MediatR;

namespace Jmeza44.EtherBlog.Application.Main.PostRequest.Commands.EditPost
{
    public class EditPostCommand : IRequest<bool>
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }

        public class EditPostCommandHandler : IRequestHandler<EditPostCommand, bool>
        {
            private readonly IApplicationDbContext _dbContext;

            public EditPostCommandHandler(IApplicationDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<bool> Handle(EditPostCommand request, CancellationToken cancellationToken)
            {
                var post = await _dbContext.Posts.FindAsync(new object[] { request.Id }, cancellationToken);
                if (post == null)
                {
                    throw new NotFoundException(nameof(Post), request.Id);
                }

                post.Title = request.Title;
                post.Content = request.Content;

                _dbContext.Posts.Update(post);
                var updated = await _dbContext.SaveChangesAsync(cancellationToken);
                return updated > 0;
            }
        }
    }
}
