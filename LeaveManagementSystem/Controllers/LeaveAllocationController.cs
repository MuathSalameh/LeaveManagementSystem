using Leave.Data.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Leave.Model.Models;
using Leave.Model.ViewModels;
using Leave.Utility;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
namespace LeaveManagementSystem.Controllers
{
    [Authorize]
    public class LeaveAllocationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public LeaveAllocationController(IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize(Roles = $"{SD.Role_Admin},{SD.Role_Supervisor}")]
        public async Task<IActionResult> Details(string Id)
        {
            // show all users for the admin
            var AllEmployee = await _unitOfWork.UserService.GetUsersInRoleAsync(SD.Role_Employee);
            if (AllEmployee == null) {
            return NotFound();
            }
            return View(AllEmployee);
        }
        [Authorize(Roles = SD.Role_Admin)]
        [HttpPost]
        public IActionResult AdminAllocateLeave(EmployeeLeaveAllocationVM employeeVM)
        {
            if (string.IsNullOrEmpty(employeeVM.ApplicationUser.Id))
            {
                return NotFound();
            }
            string EmpId = employeeVM.ApplicationUser.Id;
            _unitOfWork.LeaveAllocations.Add(employeeVM.ApplicationUser.Id);
            return RedirectToAction(nameof(Index), new { EmpId } ); 
        }
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult AdminEditAllocation(int id)
        {
            if (id == 0)
            {
                return NotFound();  
            }
            var allocation = _unitOfWork.LeaveAllocations.GetSingle(q=>q.LeaveAllocationId==id,properties: "LeaveType,Period,Employee");
            var user = new ApplicationUser(); 
            if (allocation == null)
            {
                return NotFound();
            }
            return View(allocation);
        }
        [HttpPost , ActionName("AdminEditAllocation")]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult AdminEditAllocation(LeaveAllocation leave) 
        {
            if (leave == null)
            {
                return NotFound();
            }
            if (DaysExceedMaximum(leave.LeaveType.Id, leave.Days))
            {
                ModelState.AddModelError("Days", "The Allocation must not exceed the maximum leave type value");
            }
            if (ModelState.IsValid)
            { 
                _unitOfWork.LeaveAllocations.Update(leave);
                _unitOfWork.LeaveAllocations.Save();
                return RedirectToAction(nameof(Index), new { EmpId = leave.Employee.Id });
            }
            leave = _unitOfWork.LeaveAllocations.GetSingle(q => q.LeaveAllocationId == leave.LeaveAllocationId,properties : "LeaveType,Period,Employee");
            return View(leave);
        }
        public async Task<IActionResult> Index(string? EmpId)
        {
            if (!string.IsNullOrEmpty(EmpId)) {
                var user = await _unitOfWork.UserService.GetUserByIdAsync(EmpId);
                if (user == null)
                {
                    return Unauthorized();
                }
                var leaveAllocationList = _unitOfWork.LeaveAllocations.GetAll(properties: "LeaveType,Period").Where(q => q.EmployeeId == EmpId && q.Period.EndADate.Year==DateTime.Now.Year).ToList();
                if (leaveAllocationList == null)
                {
                    return NotFound();
                }
                var EmpVM = new EmployeeLeaveAllocationVM
                {
                    LeaveAllocation = leaveAllocationList,
                    ApplicationUser = user
                };
                return View(EmpVM);
            }
            else
            {
                var user = await _unitOfWork.UserService.GetloggedInUserAsync();
                if (user == null)
                {
                    return Unauthorized();
                }
                var leaveAllocationList = _unitOfWork.LeaveAllocations.GetAll(properties: "LeaveType,Period").Where(q => q.EmployeeId == user.Id).ToList();
                if (leaveAllocationList == null)
                {
                    return NotFound();
                }
                var EmpVM = new EmployeeLeaveAllocationVM
                {
                    LeaveAllocation = leaveAllocationList,
                    ApplicationUser = user
                };
                return View(EmpVM);
            }
        }
        public bool DaysExceedMaximum(int LeaveTypeId , int NewDays)
        {
            var leaveType = _unitOfWork.Leavetypes.GetSingle(q => q.Id == LeaveTypeId);
            return   leaveType.NumberOfDays < NewDays;
        }
    }
}
