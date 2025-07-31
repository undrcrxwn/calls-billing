using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Calls.Application.Models;

public class UpdateContactRequest
{
    public Guid Id { get; set; }
    
    [DisplayName("Имя")]
    [Required(ErrorMessage = "Введите имя")]
    [MaxLength(80, ErrorMessage = "Слишком длинное имя")]
    public string Name { get; set; }

    [DisplayName("Почта")]
    [Required(ErrorMessage = "Введите почту")]
    [EmailAddress(ErrorMessage = "Невалидный почтовый адрес")]
    [MaxLength(80, ErrorMessage = "Слишком длинное имя")]
    public string Email { get; set; }

    [DisplayName("Дата рождения")]
    [Required(ErrorMessage = "Введите дату рождения")]
    public DateOnly DateOfBirth { get; set; }
    
    [DisplayName("Тариф")]
    [Required(ErrorMessage = "Введите значение")]
    [Range(typeof(decimal), "0", "1000000", ErrorMessage = "Тариф должен лежать в диапазоне от 0 руб. до 1 млн. руб. в минуту")]
    public decimal ChargePerMinute { get; set; }

    public IList<string> PhoneNumbers { get; set; } = [];
}