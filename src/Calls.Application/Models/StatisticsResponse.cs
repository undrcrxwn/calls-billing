using System.ComponentModel.DataAnnotations;

namespace Calls.Application.Models;

public class StatisticsResponse
{
    public int CallsStarted { get; set; }
    public int CallsJoined { get; set; }
    [DisplayFormat(DataFormatString = "{0:N2}")]
    public double StartedCallDuration { get; set; }
    [DisplayFormat(DataFormatString = "{0:N2}")]
    public double AverageCallDuration { get; set; }
    [DisplayFormat(DataFormatString = "{0:N2}")]
    public decimal AverageCharge { get; set; }
    [DisplayFormat(DataFormatString = "{0:N2}")]
    public decimal TotalCharge { get; set; }
}