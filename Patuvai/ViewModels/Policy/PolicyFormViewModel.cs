using System.ComponentModel.DataAnnotations;
using Patuvai.Models.Enums;

namespace Patuvai.ViewModels.Policy
{
    public class PolicyFormViewModel
    {
        public PolicyType Type { get; set; }

        public double Version { get; set; }

        [Required(ErrorMessage = "Съдържанието е задължително.")]
        [Display(Name = "Съдържание")]
        public string Content { get; set; }
    }
}
