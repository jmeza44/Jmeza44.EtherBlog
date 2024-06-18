using Jmeza44.EtherBlog.Application.Common.Mapping;
using Jmeza44.EtherBlog.Domain.Entities;

namespace Jmeza44.EtherBlog.Application.Common.DTOs
{
public class PostDto : IMapFrom<Post>
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedByAt { get; set; }
}
}
