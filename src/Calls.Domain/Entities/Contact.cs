namespace Calls.Domain.Entities;

public class Contact
{
    public Guid Id { get; set; }
    public Guid ProfileId { get; set; }
    public Profile Profile { get; set; }
    public decimal ChargePerMinute { get; set; }
    public IList<PhoneNumber> PhoneNumbers { get; set; }
}