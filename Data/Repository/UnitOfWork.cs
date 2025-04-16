using Data.Data;
using Data.Repository.IRepository;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public ILeaveTypesRepository Leavetypes { get ; set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Leavetypes = new LeaveTypesRepository(_context);
        }
    }
}
