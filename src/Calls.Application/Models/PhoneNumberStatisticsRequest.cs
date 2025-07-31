namespace Calls.Application.Models;

public class PhoneNumberStatisticsRequest
{
    public string PhoneNumber { get; set; }
    public DateTime Since { get; set; }
    public DateTime Until { get; set; }
    public StatisticsResponse? Statistics { get; set; }
}