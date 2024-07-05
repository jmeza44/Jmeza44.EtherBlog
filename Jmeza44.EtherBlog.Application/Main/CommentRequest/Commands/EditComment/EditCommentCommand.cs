using AutoMapper;
using Jmeza44.EtherBlog.Application.Common.DTOs;
using Jmeza44.EtherBlog.Application.Common.Exceptions;
using Jmeza44.EtherBlog.Application.Common.Interfaces;
using MediatR;

namespace Jmeza44.EtherBlog.Application.Main.CommentRequest.Commands.EditComment
{
    public class EditCommentCommand : IRequest<CommentDto>
    {
        public required int Id { get; set; }
        public required string Content { get; set; }

        public class EditCommentCommandHandler : IRequestHandler<EditCommentCommand, CommentDto>
        {
            private readonly IMapper _mapper;
            private readonly IApplicationDbContext _dbContext;
            private readonly ICurrentUserService _currentUserService;

            public EditCommentCommandHandler(IApplicationDbContext dbContext, IMapper mapper, ICurrentUserService currentUserService)
            {
                _mapper = mapper;
                _dbContext = dbContext;
                _currentUserService = currentUserService;
            }

            public async Task<CommentDto> Handle(EditCommentCommand request, CancellationToken cancellationToken)
            {
                var comment = await _dbContext.Comments.FindAsync(new object[] { request.Id }, cancellationToken);
                if (comment == null)
                {
                    throw new NotFoundException(nameof(comment), request.Id);
                }

                var currentUser = await _currentUserService.GetCurrentUserEmailAsync();
                if (currentUser == null)
                {
                    throw new UnresolvedIdentityException();
                }
                if (!currentUser.Equals(comment.CreatedBy))
                {
                    throw new AccessLevelViolationException();
                }

                comment.Content = request.Content;

                _dbContext.Comments.Update(comment);
                var updated = await _dbContext.SaveChangesAsync(cancellationToken);
                return _mapper.Map<CommentDto>(comment);
            }
        }
    }
}
