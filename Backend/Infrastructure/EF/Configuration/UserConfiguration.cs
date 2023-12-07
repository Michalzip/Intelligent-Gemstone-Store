namespace IntelligentStore.Infrastructure;

using IntelligentStore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared;
using Shared.ValueObjects.Common.CreatedAt;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(a => a.Id);

        builder.HasIndex(x => x.Email).IsUnique();

        builder
            .Property<Email>("Email")
            .IsRequired()
            .HasConversion(x => x.Value, x => new Email(x));

        builder
            .Property<PhoneNumber>("PhoneNumber")
            .IsRequired()
            .HasConversion(x => x.Value, x => new PhoneNumber(x));

        builder.HasOne(u => u.Address).WithOne(x => x.User).HasForeignKey<Address>(u => u.UserId);

        builder
            .Property<CreatedAt>("CreatedAt")
            .IsRequired()
            .HasConversion(x => x.Value, x => new CreatedAt(x));

        builder.ToTable("Users");
    }
}
