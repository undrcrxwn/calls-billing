namespace Calls.Domain.Entities;

public class Call
{
    public Guid Id { get; set; }
    public PhoneNumber StarterPhoneNumber { get; set; }
    public IList<PhoneNumber> ParticipantPhoneNumbers { get; set; }
    public DateTime Since { get; set; }
    public DateTime? Until { get; set; }
    public double DurationInMinutes { get; set; }
    public decimal ChargePerMinute { get; set; }
    public decimal Charge { get; set; }
    public TimeSpan DurationForNow => (Until ?? DateTime.Now) - Since;
    public decimal ChargeForNow => (decimal)DurationForNow.TotalMinutes * ChargePerMinute;
}