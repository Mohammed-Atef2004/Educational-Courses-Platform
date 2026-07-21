using EducationalPlatform.Domain.Courses;
using EducationalPlatform.Domain.Courses.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationalPlatform.Infrastructure.Persistence.Configurations;

public sealed class EpisodeConfiguration : IEntityTypeConfiguration<Episode>
{
    public void Configure(EntityTypeBuilder<Episode> builder)
    {
        builder.ToTable("Episodes");

        //  Primary Key (EpisodeId.Id -> Guid) 
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasConversion(
                id => id.Id,
                value => EpisodeId.From(value))
            .HasColumnName("Id")
            .ValueGeneratedNever();

        // Foreign Key (CourseId -> Guid)
        builder.Property(e => e.CourseId)
            .HasConversion(
                id => id.Value,
                value => CourseId.From(value))
            .HasColumnName("CourseId")
            .IsRequired();

        // Name
        builder.Property(e => e.Name)
            .HasConversion(
                name => name.Value,
                value => EpisodeName.Create(value).Value)
            .HasColumnName("Name")
            .HasMaxLength(EpisodeName.MaxLength)
            .IsRequired();

        // Simple properties
        builder.Property(e => e.Description)
            .HasMaxLength(2000);

        builder.Property(e => e.ImageUrl)
            .HasMaxLength(500);

        builder.Property(e => e.VideoLink)
            .HasMaxLength(500);

        builder.Property(e => e.IsFreePreview)
            .IsRequired();

        builder.Property(e => e.DurationInSeconds)
            .IsRequired();

        builder.Property(e => e.Order)
            .IsRequired();
    }
}