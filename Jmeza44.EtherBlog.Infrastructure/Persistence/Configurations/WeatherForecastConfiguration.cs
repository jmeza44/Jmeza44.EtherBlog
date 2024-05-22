using Jmeza44.EtherBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jmeza44.EtherBlog.Infrastructure.Persistence.Configurations
{
    public class WeatherForecastConfiguration : IEntityTypeConfiguration<WeatherForecast>
    {
        public void Configure(EntityTypeBuilder<WeatherForecast> builder)
        {
            builder.Property(e => e.Date)
                .IsRequired();

            builder.Property(e => e.TemperatureC)
                .IsRequired();

            builder.Property(e => e.City)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.Summary)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
