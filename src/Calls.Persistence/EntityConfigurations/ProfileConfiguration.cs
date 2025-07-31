using Calls.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Calls.Persistence.EntityConfigurations;

public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        builder.HasKey(profile => profile.Id);
        builder.HasIndex(profile => profile.Email).IsUnique();
        builder.HasOne(profile => profile.Contact).WithOne(contact => contact.Profile);
        builder.HasOne(profile => profile.Contact)
            .WithOne(contact => contact.Profile)
            .HasForeignKey<Contact>(contact => contact.ProfileId);
    }
}