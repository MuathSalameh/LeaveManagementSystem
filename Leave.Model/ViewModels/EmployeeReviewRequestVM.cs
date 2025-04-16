using Leave.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leave.Model.ViewModels
{
    public class EmployeeReviewRequestVM
    {
        public ApplicationUser? Employee { get; set; }
        public LeaveRequest? LeaveRequests { get; set; }
    }
}
