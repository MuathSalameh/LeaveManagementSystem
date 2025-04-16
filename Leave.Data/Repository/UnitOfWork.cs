using Leave.Data.Data;
using Leave.Data.Migrations;
using Leave.Data.Repository.IRepository;
using Leave.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leave.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public ILeaveTypesRepository Leavetypes { get ; set; }
        public ILeaveAllocationsRepository LeaveAllocations { get; set ; }
        public IPeriodsRepository Periods { get; set; }
        public ILeaveRequestRepository LeaveRequest { get; set; }
        public IUserService UserService { get; set; }

        public UnitOfWork(ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _contextAccessor = httpContextAccessor;
            Leavetypes = new LeaveTypesRepository(_context);
            LeaveAllocations = new LeaveAllocationsRepository(_context);
            Periods = new PeriodsRepository(_context);
            LeaveRequest= new LeaveRequestRepository(_context);
            UserService = new UserService(_contextAccessor, _userManager);
        }
    }
}
