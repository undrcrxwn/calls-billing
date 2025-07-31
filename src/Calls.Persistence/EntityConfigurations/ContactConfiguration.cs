using Calls.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Calls.Persistence.EntityConfigurations;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.HasKey(contact => contact.Id);
        builder.HasMany(contact => contact.PhoneNumbers)
            .WithOne(phone => phone.Contact)
            .HasForeignKey(phone => phone.ContactId);
    }
}