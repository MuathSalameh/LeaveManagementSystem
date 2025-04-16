using Leave.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leave.Data.Repository.IRepository
{
    public interface ILeaveRequestRepository : IRepository<LeaveRequest>
    {
        void Update(LeaveRequest leaveRequest);
    }
}
