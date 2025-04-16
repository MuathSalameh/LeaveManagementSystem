using System.ComponentModel.DataAnnotations;

namespace Leave.Model.Models
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

        public List<LeaveAllocation>? LeaveAllocations { get; set; }
    }
}
