using EducationalPlatform.Domain.Courses;
using EducationalPlatform.Domain.Courses.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationalPlatform.Infrastructure.Persistence.Configurations;

public sealed class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Courses");

        //  Primary Key (CourseId -> Guid)
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasConversion(
                id => id.Value,
                value => CourseId.From(value))
            .HasColumnName("Id")
            .ValueGeneratedNever();

        //  Name
        builder.Property(c => c.Name)
            .HasConversion(
                name => name.Value,
                value => CourseName.Create(value).Value)
            .HasColumnName("Name")
            .HasMaxLength(CourseName.MaxLength)
            .IsRequired();

        //  Description
        builder.Property(c => c.Description)
            .HasConversion(
                description => description.Value,
                value => CourseDescription.Create(value).Value)
            .HasColumnName("Description")
            .HasMaxLength(CourseDescription.MaxLength)
            .IsRequired();

        //  Price (Money) - Owned Type
        builder.OwnsOne(c => c.Price, price =>
        {
            price.Property(p => p.Amount)
                .HasColumnName("Price_Amount")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            price.Property(p => p.Currency)
                .HasColumnName("Price_Currency")
                .HasMaxLength(3)
                .IsRequired();
        });

        // Simple properties
        builder.Property(c => c.InstructorId)
            .IsRequired();

        builder.Property(c => c.ImageUrl)
            .HasMaxLength(500);

        builder.Property(c => c.VideoLink)
            .HasMaxLength(500);

        builder.Property(c => c.CourseStatus)
            .HasConversion<string>() 
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(c => c.PublishedAt);
        builder.Property(c => c.ArchivedAt);

        //  Relationship: Course (1) -> Episodes (Many)
        builder.HasMany(c => c.Episodes)
            .WithOne()
            .HasForeignKey(e => e.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        
        builder.Navigation(c => c.Episodes)
            .HasField("_episodes")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}