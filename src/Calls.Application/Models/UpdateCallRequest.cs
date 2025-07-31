using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Calls.Application.Models;

public class UpdateCallRequest
{
    public Guid Id { get; set; }
    
    [DisplayName("Номер инициатора")]
    [Required(ErrorMessage = "Укажите номер телефона")]
    [Phone(ErrorMessage = "Укажите номер телефона")]
    public string StarterPhoneNumber { get; set; }
    
    public IList<string> ParticipantPhoneNumbers { get; set; }
    
    [DisplayName("Начало")]
    [Required(ErrorMessage = "Укажите время начала звонка")]
    public DateTime Since { get; set; }
    
    [DisplayName("Конец")]
    public DateTime? Until { get; set; }
    
    [DisplayName("Тариф")]
    [Required(ErrorMessage = "Введите значение")]
    [Range(typeof(decimal), "0", "1000000", ErrorMessage = "Тариф должен лежать в диапазоне от 0 руб. до 1 млн. руб. в минуту")]
    public decimal ChargePerMinute { get; set; }
}