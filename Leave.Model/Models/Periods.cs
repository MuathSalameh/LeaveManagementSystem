using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leave.Model.Models
{
    public class Periods
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100 , ErrorMessage ="Can't be more than 100 characters")]
        public string? Name { get; set; }


        public string StartDateFormatted => StartDate.ToString("yyyy/MM/dd");
        public string EndDateFormatted => EndADate.ToString("yyyy/MM/dd");

        [Required]
        [Display(Name = "Start Date")]
        public DateOnly StartDate { get; set; }
        [Required]
        [Display(Name = "End Date")]
        public DateOnly EndADate { get; set; }
    }
}