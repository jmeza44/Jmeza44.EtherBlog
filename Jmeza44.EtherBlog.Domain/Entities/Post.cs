using Jmeza44.EtherBlog.Domain.Entities.BaseEntities;

namespace Jmeza44.EtherBlog.Domain.Entities
{
    public class Post : AuditableEntity, IApplicationBaseEntity
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}
