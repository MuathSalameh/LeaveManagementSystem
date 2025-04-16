using Leave.Data.Data;
using Leave.Data.Repository.IRepository;
using Leave.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Leave.Data.Repository
{
    public class LeaveAllocationsRepository : Repository<LeaveAllocation>, ILeaveAllocationsRepository
    {
        private readonly ApplicationDbContext _context;
        public LeaveAllocationsRepository(ApplicationDbContext context) : base(context) 
        {
            _context = context;

        }
        public void Add(string empId)
        {
            var leaveTypes = _context.Leavetypes.Where(q=> !q.LeaveAllocations.Any(x=>x.EmployeeId==empId)).ToList();
            var currentDate = DateTime.Now;
            var period = _context.Periods.Single(q => q.EndADate.Year == currentDate.Year);
            if (period != null) {
                var monthsRemaining = period.EndADate.Month - currentDate.Month;

                foreach (var leave in leaveTypes)
                {
                    var periodRate = decimal.Divide(leave.NumberOfDays, 12);
                    var leaveAllocationToAdd = new LeaveAllocation
                    {
                        EmployeeId = empId,
                        LeaveTypeId=leave.Id,
                        PeriodId = period.Id,
                        Days = (int)Math.Ceiling(periodRate * monthsRemaining)
                    };
                    _context.LeaveAllocations.Add(leaveAllocationToAdd);

                }
            }
            _context.SaveChanges();

        }
        public void Update(LeaveAllocation leaveAllocation)
        {
            var obj = _context.LeaveAllocations.Where(q=> q.LeaveAllocationId== leaveAllocation.LeaveAllocationId).FirstOrDefault();
            if (obj != null)
            {
                obj.Days = leaveAllocation.Days;
            }
        }
    }
}
