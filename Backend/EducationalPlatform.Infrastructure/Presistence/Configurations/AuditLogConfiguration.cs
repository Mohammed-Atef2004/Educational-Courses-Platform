using EducationalPlatform.Infrastructure.Presistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Infrastructure.Presistence.Configurations
{
    public sealed class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.ToTable("AuditLogs");

            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).UseIdentityColumn();

            builder.Property(a => a.ActorId).IsRequired();

            builder.Property(a => a.Action)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.EntityType)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.EntityId).IsRequired();

            builder.Property(a => a.IpAddress)
                .HasMaxLength(45);   // IPv6 max length

            builder.Property(a => a.Succeeded).IsRequired();

            builder.Property(a => a.FailureReason)
                .HasMaxLength(500);

            builder.Property(a => a.OccurredAt).IsRequired();

            builder.HasIndex(a => a.ActorId);
            builder.HasIndex(a => a.EntityId);
            builder.HasIndex(a => a.Action);
            builder.HasIndex(a => a.OccurredAt);
        }
    }

}
