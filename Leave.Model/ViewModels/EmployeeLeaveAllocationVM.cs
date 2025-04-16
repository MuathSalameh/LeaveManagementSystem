using Leave.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leave.Model.ViewModels
{
    public class EmployeeLeaveAllocationVM
    {
        public List<LeaveAllocation>? LeaveAllocation { get; set; }
        public LeaveAllocation? SingleLeaveAllocation { get; set; } = null;
        public ApplicationUser? ApplicationUser { get; set; }    
    }
}
