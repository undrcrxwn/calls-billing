namespace Calls.Domain.Entities;

public class PhoneNumber
{
    public Guid? ContactId { get; set; }
    public Contact? Contact { get; set; }
    public string Value { get; set; }

    public PhoneNumber(string value)
    {
        Value = value;
    }
}