using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leave.Model.Models
{
    public class LeaveAllocation
    {
        [Key]
        public int LeaveAllocationId { get; set; }

        [ValidateNever]
        public int LeaveTypeId { get; set; }

        [ValidateNever]

        [ForeignKey(nameof(LeaveTypeId))]  
        public LeaveType? LeaveType { get; set; }

        [ValidateNever]
        [ForeignKey("EmployeeId")]
        public ApplicationUser Employee { get; set; }
        [ValidateNever]
        public string EmployeeId { get; set; }

        [ValidateNever]
        [ForeignKey("PeriodId")]
        public Periods Period { get; set; }
        [ValidateNever]
        public int PeriodId { get; set; }

        [Range(1,int.MaxValue , ErrorMessage ="This field must be at least 1")]
        public int Days { get; set; }   
    }
}
