using System.ComponentModel.DataAnnotations;

namespace Patuvai.Models.Enums
{
    public enum PolicyType
    {
        [Display(Name = "Общи условия")]
        Terms,
        [Display(Name = "Политика за поверителност")]
        Privacy,
        [Display(Name = "Декларация за шофьори")]
        DriverDeclaration
    }
}
