using IntelligentStore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared;
using Shared.ValueObjects.Common.CreatedAt;

namespace IntelligentStore.Infrastructure;

public class AdminConfiguration : IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> builder)
    {
        builder.HasIndex(x => x.Email).IsUnique();

        builder
            .Property<Email>("Email")
            .IsRequired()
            .HasConversion(x => x.Value, x => new Email(x));

        builder
            .Property<Password>("Password")
            .IsRequired()
            .HasConversion(x => x.Value, x => new Password(x));

        builder
            .Property<CreatedAt>("CreatedAt")
            .IsRequired()
            .HasConversion(x => x.Value, x => new CreatedAt(x));

        builder.ToTable("Admins");
    }
}
