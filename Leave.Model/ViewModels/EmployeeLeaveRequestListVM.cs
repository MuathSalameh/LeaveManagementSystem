using Leave.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leave.Model.ViewModels
{
    public class EmployeeLeaveRequestListVM
    {
        [Display(Name = "Total Number Of Requests")]
        public int TotalRequests { get; set; }
        [Display(Name = "Approved Requests")]
        public int ApprovedRequests { get; set; }
        [Display(Name = "Pending Requests")]
        public int PendingRequests { get; set; }
        [Display(Name = "Declined Requests")]
        public int DeclinedRequests { get; set; }
        public List<LeaveRequest>? LeaveRequests { get; set; }

    }
}
