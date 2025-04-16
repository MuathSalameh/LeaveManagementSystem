using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Leave.Model.ViewModels
{
    public class CreateLeaveRequestVM :  IValidatableObject
    {
        [DisplayName("Start Date")]
        [Required]
        public DateOnly RequestStartDate { get; set; }
        [DisplayName("End Date")]
        [Required]
        public DateOnly RequestEndtDate { get; set; }
        [DisplayName("Additional Information")]
        [StringLength(250)]  
        public string? RequestComments { get; set; }
        [DisplayName("Leave Type")]
        [Required]
        public int LeaveTypeId { get; set; }

        public IEnumerable<SelectListItem>? LeaveTypelist { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(RequestEndtDate < RequestStartDate)
            {
                yield return new ValidationResult("The start date must be before the end date ! " , [nameof(RequestStartDate)]);
            }
        }
    }
}
