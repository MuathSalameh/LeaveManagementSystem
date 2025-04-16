using Leave.Data.Data;
using Leave.Data.Repository.IRepository;
using Leave.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leave.Data.Repository
{
    public class LeaveTypesRepository : Repository<LeaveType> , ILeaveTypesRepository
    {
        private readonly ApplicationDbContext _context; 
        public LeaveTypesRepository(ApplicationDbContext context) :base(context) 
        {
            _context = context;
        }
        public void Update(LeaveType leaveType)
        {
            var obj = _context.Leavetypes.FirstOrDefault(u => u.Id == leaveType.Id);

            if (obj != null)
            {  
                obj.Name = leaveType.Name;
                obj.NumberOfDays = leaveType.NumberOfDays;
            }
        }
    }
}
