using Leave.Model.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leave.Data.Repository.IRepository
{
    public interface ILeaveAllocationsRepository : IRepository<LeaveAllocation>
    {
        void Update(LeaveAllocation leaveAllocation);
        void Add(String UserId); 
    }
}
