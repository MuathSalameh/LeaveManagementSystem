using System.ComponentModel.DataAnnotations;

namespace Models.Models
{
    public class LeaveType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]

        public string? Name { get; set; }
        [Display(Name = "Number Of Days")]
        public int NumberOfDays { get; set; }
    }
}
