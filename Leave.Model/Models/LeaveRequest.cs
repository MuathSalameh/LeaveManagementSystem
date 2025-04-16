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
    public class LeaveRequest
    {
        public int Id { get; set; }
        [Display(Name ="Start Date")]
        public DateOnly RequestStartDate { get; set; }
        [Display(Name = "End Date")]
        public DateOnly RequestEndtDate { get; set; }
        public int LeaveTypeId { get; set; }

        [ForeignKey(nameof(LeaveTypeId))]
        public LeaveType? LeaveType { get; set; }

        public int leaveRequestStatusId { get; set; }
        [ForeignKey(nameof(leaveRequestStatusId))]
        [Display(Name = "Request Status")]
        public LeaveRequestStatus? LeaveRequestStatus { get; set; }

        public string EmployeeId { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public ApplicationUser? Employee { get; set; }

        public string? ReviewerId { get; set; }
        [ForeignKey(nameof(ReviewerId))]
        public ApplicationUser? Reviewer { get; set; }
        [Display(Name ="Additional Information")]
        public string? RequestComments  { get; set; }
    }
}
