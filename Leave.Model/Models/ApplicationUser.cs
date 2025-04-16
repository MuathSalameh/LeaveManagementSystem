using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Leave.Model.Models
{
    public class ApplicationUser : IdentityUser 
    {
        [Required]
        [Display(Name ="Full Name")]
        [Range(5,150)]
        public string? FullName { get; set; }
        [Display(Name ="Date Of Birth")]
        public DateOnly DateOfBirth { get; set; }
    }
}
