namespace Calls.Domain.Entities;

public class Profile
{
    public Guid Id { get; set; }
    public Contact Contact { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateOnly DateOfBirth { get; set; }
}