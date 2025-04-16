using Leave.Data.Data;
using Leave.Data.Repository.IRepository;
using Leave.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Leave.Data.Repository
{
    public class PeriodsRepository : Repository<Periods>, IPeriodsRepository
    {
        private readonly ApplicationDbContext _context;
        public PeriodsRepository(ApplicationDbContext context) :base (context) 
        {
            _context  = context;  
        }
        public void Update(Periods periods)
        {
            var obj = _context.Periods.FirstOrDefault(u => u.Id == periods.Id);

            if (obj != null)
            {
                obj.Name = periods.Name;
                obj.StartDate = periods.StartDate;
                obj.EndADate = periods.EndADate;
            }
        }
    }
}
