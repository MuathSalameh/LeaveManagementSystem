using Leave.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leave.Data.Repository.IRepository
{
    public interface IUnitOfWork 
    {
     ILeaveTypesRepository Leavetypes { get; set; }
     ILeaveAllocationsRepository LeaveAllocations { get; set; }
     IPeriodsRepository Periods { get; set; }
     ILeaveRequestRepository LeaveRequest { get; set; }
     IUserService UserService { get; set; }
    }
}