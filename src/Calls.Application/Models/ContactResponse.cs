using Calls.Domain.Entities;

namespace Calls.Application.Models;

public class ContactResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public decimal ChargePerMinute { get; set; }
    public IList<string> PhoneNumbers { get; set; }

    public static ContactResponse FromEntity(Contact contact) => new()
    {
        Id = contact.Id,
        Name = contact.Profile.Name,
        Email = contact.Profile.Email,
        DateOfBirth = contact.Profile.DateOfBirth,
        ChargePerMinute = contact.ChargePerMinute,
        PhoneNumbers = contact.PhoneNumbers.Select(number => number.Value).ToList()
    };
}