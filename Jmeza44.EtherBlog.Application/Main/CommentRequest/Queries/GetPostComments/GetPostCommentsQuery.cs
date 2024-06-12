using AutoMapper;
using Jmeza44.EtherBlog.Application.Common.DTOs;
using Jmeza44.EtherBlog.Application.Common.Exceptions;
using Jmeza44.EtherBlog.Application.Common.Interfaces;
using Jmeza44.EtherBlog.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jmeza44.EtherBlog.Application.Main.CommentRequest.Queries.GetPostComments
{
    public class GetPostCommentsQuery : IRequest<List<CommentDto>>
    {
        public int PostId { get; set; }

        public class GetPostCommentsQueryHandler : IRequestHandler<GetPostCommentsQuery, List<CommentDto>>
        {
            private readonly IApplicationDbContext _dbContext;
            private readonly IMapper _mapper;

            public GetPostCommentsQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }

            public async Task<List<CommentDto>> Handle(GetPostCommentsQuery request, CancellationToken cancellationToken)
            {
                var post = await _dbContext.Posts.FindAsync(new object[] { request.PostId }, cancellationToken);
                if (post == null)
                {
                    throw new NotFoundException(nameof(Post), request.PostId);
                }

                var comments = await _dbContext.Comments
                    .Where(c => c.PostId == request.PostId)
                    .ToListAsync(cancellationToken);

                return _mapper.Map<List<CommentDto>>(comments);
            }
        }
    }
}
