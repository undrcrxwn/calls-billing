using System.ComponentModel;

namespace Calls.Application.Models;

public class ContactStatisticsRequest
{
    public Guid Id { get; set; }
    [DisplayName("Начиная с")]
    public DateTime? Since { get; set; }
    [DisplayName("Заканчивая")]
    public DateTime? Until { get; set; }
    public StatisticsResponse? Statistics { get; set; }
}