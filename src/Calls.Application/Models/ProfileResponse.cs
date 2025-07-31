using Calls.Domain.Entities;

namespace Calls.Application.Models;

public class ProfileResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateOnly DateOfBirth { get; set; }
    
    public static ProfileResponse FromEntity(Profile profile) => new()
    {
        Id = profile.Id,
        Name = profile.Name,
        Email = profile.Email,
        DateOfBirth = profile.DateOfBirth
    };
}