using Jmeza44.EtherBlog.Domain.Entities.BaseEntities;

namespace Jmeza44.EtherBlog.Domain.Entities
{
    public class Comment : AuditableEntity, IApplicationBaseEntity
    {
        public int Id { get; set; }
        public required string Content { get; set; }
        public int PostId { get; set; }
        public Post? Post { get; set; }
    }
}
