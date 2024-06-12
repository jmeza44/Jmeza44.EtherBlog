namespace Jmeza44.EtherBlog.Domain.Entities.BaseEntities
{
    public class AuditableEntity
    {
        public string? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedByAt { get; set; }
    }
}
