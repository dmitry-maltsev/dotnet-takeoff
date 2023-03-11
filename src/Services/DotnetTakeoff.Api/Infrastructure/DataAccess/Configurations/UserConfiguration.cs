using DotnetTakeoff.Api.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotnetTakeoff.Api.Infrastructure.DataAccess.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("ID")
            .IsRequired();

        builder.Property(x => x.Email)
            .HasMaxLength(60)
            .IsRequired();

        builder.Property(x => x.FirstName)
            .HasMaxLength(30);

        builder.Property(x => x.LastName)
            .HasMaxLength(30);

        builder.Property(x => x.IsActive);

        builder.Property(x => x.LastLogin);

        builder.HasIndex(x => x.Email)
            .IsUnique();
    }
}
