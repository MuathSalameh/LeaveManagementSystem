using Leave.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leave.Data.Repository.IRepository
{
    public interface IUserService
    {
        public  Task<ApplicationUser> GetloggedInUserAsync();
        public  Task<ApplicationUser> GetUserByIdAsync(string userId);
        public Task<IEnumerable<ApplicationUser>> GetUsersInRoleAsync(string role);
    }
}
