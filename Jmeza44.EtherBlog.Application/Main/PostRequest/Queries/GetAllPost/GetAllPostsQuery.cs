using AutoMapper;
using Jmeza44.EtherBlog.Application.Common.DTOs;
using Jmeza44.EtherBlog.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Jmeza44.EtherBlog.Application.Main.PostRequest.Queries.GetAllPost
{
    public class GetAllPostsQuery : IRequest<PaginatedData<PostDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? CreatedBy { get; set; }
    }

    public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, PaginatedData<PostDto>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllPostsQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PaginatedData<PostDto>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Posts.AsQueryable();

            if (!request.CreatedBy.IsNullOrEmpty()) query = query.Where(p => request.CreatedBy.Equals(p.CreatedBy));

            // Calculate total count
            var totalCount = await query.CountAsync(cancellationToken);

            // Apply pagination
            query = query.Skip((request.PageNumber - 1) * request.PageSize)
                         .Take(request.PageSize);

            var posts = await query.ToListAsync(cancellationToken);

            var paginatedData = new PaginatedData<PostDto>
            {
                TotalCount = totalCount,
                Data = _mapper.Map<List<PostDto>>(posts)
            };

            return paginatedData;
        }
    }
}
