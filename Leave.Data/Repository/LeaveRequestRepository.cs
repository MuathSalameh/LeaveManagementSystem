using Leave.Data.Data;
using Leave.Data.Migrations;
using Leave.Data.Repository.IRepository;
using Leave.Model.Models;
using Microsoft.CodeAnalysis.Elfie.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leave.Data.Repository
{
    public class LeaveRequestRepository : Repository<LeaveRequest>, ILeaveRequestRepository
    {
        public ApplicationDbContext _context { get; set; }
        public LeaveRequestRepository(ApplicationDbContext context) : base(context)
        {
            _context  = context;
        }
        public void Update(LeaveRequest leaveRequest)
        {

        }
        void IRepository<LeaveRequest>.Add(LeaveRequest entity)
        {
            // get the mapped value 
            // remove some allocation from user 
            var RequestedDays = entity.RequestEndtDate.DayNumber - entity.RequestStartDate.DayNumber;
            var currentDate = DateTime.Now;
            var period = _context.Periods.Single(q => q.EndADate.Year == currentDate.Year);
            var Allocations = 
                _context.LeaveAllocations.First(q=>q.LeaveTypeId == entity.LeaveTypeId && q.EmployeeId == entity.EmployeeId && q.PeriodId==period.Id);
            Allocations.Days -= RequestedDays; 
            _context.SaveChanges();
            // add to leave request
            _context.LeaveRequests.Add(entity);
        }
    }
}
