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
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserService( IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _contextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public async Task<ApplicationUser> GetloggedInUserAsync()
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
            return user;
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user;
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsersInRoleAsync(string role)
        {
            var user = await _userManager.GetUsersInRoleAsync(role);
            return user;
        }
    }
}
