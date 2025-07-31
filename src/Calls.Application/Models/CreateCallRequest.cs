using System.ComponentModel.DataAnnotations;

namespace Calls.Application.Models;

public class CreateCallRequest
{
    [Required, Phone]
    public string StarterPhoneNumber { get; set; }
    
    [Required, Phone]
    public string ParticipantPhoneNumber { get; set; }
}