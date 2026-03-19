using EducationalPlatform.Domain.Users;
using EducationalPlatform.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        // ========================
        // Email (Value Converter)
        // ========================
        builder.Property(u => u.Email)
            .HasConversion(
                email => email.Value,
                value => Email.Create(value).Value
            )
            .HasColumnName("Email")
            .IsRequired();

        // ========================
        // Username (Value Converter)
        // ========================
        builder.Property(u => u.Username)
            .HasConversion(
                username => username.Value,
                value => Username.Create(value).Value
            )
            .HasColumnName("Username")
            .IsRequired();

        builder.HasIndex(u => u.Username).IsUnique();

        // ========================
        // PhoneNumber (Value Converter)
        // ========================
        builder.Property(u => u.PhoneNumber)
            .HasConversion(
                phone => phone.Value,
                value => PhoneNumber.Create(value).Value
            )
            .HasColumnName("PhoneNumber");

        // ========================
        // FullName (Owned Type)
        // ========================
        builder.OwnsOne(u => u.FullName, name =>
        {
            name.Property(n => n.FirstName)
                .HasColumnName("FirstName")
                .IsRequired()
                .HasMaxLength(100);

            name.Property(n => n.LastName)
                .HasColumnName("LastName")
                .IsRequired()
                .HasMaxLength(100);
        });

        builder.Navigation(u => u.FullName).IsRequired();
    }
}