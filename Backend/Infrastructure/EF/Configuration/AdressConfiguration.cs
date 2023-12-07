using System;
using IntelligentStore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared;

namespace IntelligentStore.Infrastructure.EF.Configuration
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(a => a.Id);

            builder
                .Property<Street>("Street")
                .IsRequired()
                .HasConversion(x => x.Value, x => new Street(x));

            builder
                .Property<HouseNumber>("HouseNumber")
                .IsRequired()
                .HasConversion(x => x.Value, x => new HouseNumber(x));

            builder
                .Property<City>("City")
                .IsRequired()
                .HasConversion(x => x.Value, x => new City(x));

            builder
                .Property<PostalCode>("PostalCode")
                .IsRequired()
                .HasConversion(x => x.Value, x => new PostalCode(x));

            builder.ToTable("Addresses");
        }
    }
}
