using Jmeza44.EtherBlog.Application.Common.Interfaces;
using Jmeza44.EtherBlog.Domain.Entities;
using MediatR;

namespace Jmeza44.EtherBlog.Application.Main.PostRequest.Commands.CreatePost
{
    public class CreatePostCommand : IRequest<int>
    {
        public required string Title { get; set; }
        public required string Content { get; set; }

        public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, int>
        {
            private readonly IApplicationDbContext _dbContext;

            public CreatePostCommandHandler(IApplicationDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<int> Handle(CreatePostCommand request, CancellationToken cancellationToken)
            {
                var post = new Post
                {
                    Title = request.Title,
                    Content = request.Content,
                };

                _dbContext.Posts.Add(post);
                var created = await _dbContext.SaveChangesAsync(cancellationToken);
                return post.Id;
            }
        }
    }
}
