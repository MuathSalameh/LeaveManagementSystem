using Leave.Model.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

namespace Leave.Data.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        {
            
        }
        public DbSet<LeaveType> Leavetypes { get; set; }
        public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
        public DbSet<Periods> Periods { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<LeaveRequestStatus> LeaveRequestStatus { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<LeaveRequestStatus>().HasData(
                    new LeaveRequestStatus { 
                    Id = 1,
                    Name = "Pending"
                    } , 
                    new LeaveRequestStatus
                    {
                        Id = 2,
                        Name = "Approved" 
                    } ,
                    new LeaveRequestStatus
                    {
                        Id = 3,
                        Name = "Declined"
                    }, 
                    new LeaveRequestStatus
                    {
                        Id = 4,
                        Name="Canceled"
                    });
        }
    }
}
