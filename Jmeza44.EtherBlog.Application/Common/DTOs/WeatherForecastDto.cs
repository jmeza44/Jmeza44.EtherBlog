using Jmeza44.EtherBlog.Application.Common.Mapping;
using Jmeza44.EtherBlog.Domain.Entities;

namespace Jmeza44.EtherBlog.Application.Common.DTOs
{
    public class WeatherForecastDto : IMapFrom<WeatherForecast>
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public string City { get; set; }
        public string Summary { get; set; }
    }
}
