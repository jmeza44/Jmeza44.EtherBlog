using Jmeza44.EtherBlog.Domain.Entities.BaseEntities;

namespace Jmeza44.EtherBlog.Domain.Entities
{
    public class WeatherForecast : AuditableEntity, IApplicationBaseEntity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public string City { get; set; }
        public string Summary { get; set; }
    }
}