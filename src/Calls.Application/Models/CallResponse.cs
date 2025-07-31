using System.ComponentModel.DataAnnotations;
using Calls.Domain.Entities;

namespace Calls.Application.Models;

public class CallResponse
{
    public Guid Id { get; set; }
    public string StarterPhoneNumber { get; set; }
    public IList<string> ParticipantPhoneNumbers { get; set; }
    public int TotalPhoneNumbersCount => ParticipantPhoneNumbers.Count + 1;
    public DateTime Since { get; set; }
    public DateTime? Until { get; set; }
    
    [DisplayFormat(DataFormatString = "{0:hh}:{0:mm}:{0:ss}")]
    public TimeSpan Duration { get; set; }
    
    public decimal ChargePerMinute { get; set; }
    
    [DisplayFormat(DataFormatString = "{0:N2} руб.")]
    public decimal? Charge { get; set; }

    public static CallResponse FromEntity(Call call) => new()
    {
        Id = call.Id,
        StarterPhoneNumber = call.StarterPhoneNumber.Value,
        ParticipantPhoneNumbers = call.ParticipantPhoneNumbers.Select(number => number.Value).ToList(),
        Since = call.Since,
        Until = call.Until,
        Duration = call.DurationForNow,
        ChargePerMinute = call.ChargePerMinute,
        Charge = call.ChargeForNow
    };
}