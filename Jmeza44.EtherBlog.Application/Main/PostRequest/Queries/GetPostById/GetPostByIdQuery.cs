using AutoMapper;
using Jmeza44.EtherBlog.Application.Common.DTOs;
using Jmeza44.EtherBlog.Application.Common.Exceptions;
using Jmeza44.EtherBlog.Application.Common.Interfaces;
using Jmeza44.EtherBlog.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jmeza44.EtherBlog.Application.Main.PostRequest.Queries.GetPostById
{
    public class GetPostByIdQuery : IRequest<PostDto>
    {
        public int Id { get; set; }
    }

    public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, PostDto>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetPostByIdQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PostDto> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (post == null)
            {
                throw new NotFoundException(nameof(Post), request.Id);
            }

            return _mapper.Map<PostDto>(post);
        }
    }
}
