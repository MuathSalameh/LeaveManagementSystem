using Leave.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leave.Data.Repository.IRepository
{
    public interface IPeriodsRepository : IRepository<Periods>
    {
        void Update(Periods periods);
    }
}
