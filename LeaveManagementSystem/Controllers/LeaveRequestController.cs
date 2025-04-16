using Leave.Data.Migrations;
using Leave.Data.Repository.IRepository;
using Leave.Model.Models;
using Leave.Model.ViewModels;
using Leave.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Controllers
{
    [Authorize]
    public class LeaveRequestController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public LeaveRequestController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //employee view the requests 
        public async Task<IActionResult> Index()
        {
            var user = await _unitOfWork.UserService.GetloggedInUserAsync();
            if (user == null)
            {
                return Unauthorized();
            }
            var LeaveReuests = _unitOfWork.LeaveRequest.GetAll(properties: "LeaveRequestStatus,LeaveType").Where(q => q.EmployeeId == user.Id).ToList();
            if (LeaveReuests == null)
            {
                return NotFound();
            }
            return View(LeaveReuests);
        }
        public IActionResult Create(int? id)
        {
            var leaveTypes = _unitOfWork.Leavetypes.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString(),
                Selected = true
            });
            var model = new CreateLeaveRequestVM
            {
                LeaveTypelist = leaveTypes
            };
            return View(model);
        }
        // emp create a request 
        [HttpPost]
        public async Task<IActionResult> Create(CreateLeaveRequestVM createLeaveRequestVM)
        {
            var user = await _unitOfWork.UserService.GetloggedInUserAsync();
            if (user == null)
            {
                return Unauthorized();
            }
            if (CheckIfDaysExceedOrZero(createLeaveRequestVM, user))
            {
                ModelState.AddModelError(string.Empty, "you have exceeded your allocations  ");
                ModelState.AddModelError(nameof(createLeaveRequestVM.RequestEndtDate), "The number of days requested is invalid . ");
            }
            if (ModelState.IsValid)
            {
                LeaveRequest leaveRequest = new LeaveRequest()
                {
                    RequestComments = createLeaveRequestVM.RequestComments,
                    RequestStartDate = createLeaveRequestVM.RequestStartDate,
                    RequestEndtDate = createLeaveRequestVM.RequestEndtDate,
                    LeaveTypeId = createLeaveRequestVM.LeaveTypeId,
                    leaveRequestStatusId = SD.Request_Status_Pending,
                    EmployeeId = user.Id
                };
                _unitOfWork.LeaveRequest.Add(leaveRequest);
                _unitOfWork.LeaveRequest.Save();
                return RedirectToAction("Index", "LeaveAllocation");
            }
            else
            {
                var leaveTypes = _unitOfWork.Leavetypes.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                createLeaveRequestVM.LeaveTypelist = leaveTypes;
                return View(createLeaveRequestVM);
            }
        }
        public IActionResult Cancel()
        {
            return View();
        }
        // empl cancel a request 
        [HttpPost]
        public IActionResult Cancel(int Id)
        {
            if (Id == 0)
            {
                return NotFound();
            }
            //get the leaverquet and change the status to canceled
            var leaveRequest = _unitOfWork.LeaveRequest.GetSingle(q => q.Id == Id);
            if (leaveRequest == null)
            {
                return NotFound();
            }
            leaveRequest.leaveRequestStatusId = SD.Request_Status_Canceled;
            //return the deleted day to the alloction
            var period = GetCurrentPeriod();
            var numberOfDaysToReturn = leaveRequest.RequestEndtDate.DayNumber - leaveRequest.RequestStartDate.DayNumber;
            var allocation =
                _unitOfWork.LeaveAllocations.
                GetSingle(q => q.LeaveTypeId == leaveRequest.LeaveTypeId && q.EmployeeId == leaveRequest.EmployeeId && q.PeriodId == period.Id);
            allocation.Days += numberOfDaysToReturn;
            _unitOfWork.LeaveRequest.Save();
            _unitOfWork.LeaveAllocations.Save();
            return RedirectToAction(nameof(Index));
        }
        // admin / sup view all the emp requests
        [Authorize(Roles = $"{SD.Role_Admin},{SD.Role_Supervisor}")]
        public IActionResult ListRequest()
        {
            var AllLeaveRequests = _unitOfWork.LeaveRequest.GetAll(properties: "LeaveType,LeaveRequestStatus");
            var leaveRequestVM = new EmployeeLeaveRequestListVM
            {
                ApprovedRequests =AllLeaveRequests.Count(q=>q.leaveRequestStatusId==SD.Request_Status_Approved),
                PendingRequests = AllLeaveRequests.Count(q => q.leaveRequestStatusId == SD.Request_Status_Pending),
                DeclinedRequests = AllLeaveRequests.Count(q => q.leaveRequestStatusId == SD.Request_Status_Declined),
                TotalRequests = AllLeaveRequests.Count(),
                LeaveRequests = AllLeaveRequests.ToList() 
            };
            return View(leaveRequestVM);
        }
        // view single request details 
        [Authorize(Roles = $"{SD.Role_Admin},{SD.Role_Supervisor}")]
        public async Task<IActionResult> Review(int id)
        {
            var leaveRequests = _unitOfWork.LeaveRequest.GetSingle(q => q.Id == id ,properties: "LeaveType");
            var user = await _unitOfWork.UserService.GetUserByIdAsync(leaveRequests.EmployeeId);
            var reviewVM = new EmployeeReviewRequestVM
            {
              // request info 
              LeaveRequests= leaveRequests,
              Employee = new ApplicationUser { 
              Id = leaveRequests.EmployeeId,
              FullName = user.FullName,
              Email = user.Email,
              }
            };
            return View(reviewVM);
        }
        
        [HttpPost, ActionName("Review")]
        [Authorize(Roles = $"{SD.Role_Admin},{SD.Role_Supervisor}")]
        public async Task<IActionResult> Review(int id , bool approved)
        {
            if(id == 0)
            {
                return NotFound();
            }
            var reviewer = await _unitOfWork.UserService.GetloggedInUserAsync();
            var leaveRequest = _unitOfWork.LeaveRequest.GetSingle(q => q.Id == id);
            if(leaveRequest == null)
            {
                return NotFound();
            }
            leaveRequest.leaveRequestStatusId= approved ? SD.Request_Status_Approved : SD.Request_Status_Declined;
            leaveRequest.ReviewerId = reviewer.Id;
            if (!approved)
            {
            var period = GetCurrentPeriod();
            var numberOfDaysToReturn = leaveRequest.RequestEndtDate.DayNumber - leaveRequest.RequestStartDate.DayNumber;
            var allocation =
                _unitOfWork.LeaveAllocations.
                GetSingle(q => q.LeaveTypeId == leaveRequest.LeaveTypeId && q.EmployeeId == leaveRequest.EmployeeId && q.PeriodId==period.Id);
            allocation.Days += numberOfDaysToReturn;
            }
            _unitOfWork.LeaveRequest.Save();
            _unitOfWork.LeaveAllocations.Save();

            return RedirectToAction(nameof(ListRequest));
        }
        public bool CheckIfDaysExceedOrZero (CreateLeaveRequestVM entity , ApplicationUser user)
        {
            var currentDate = DateTime.Now;
            var period = _unitOfWork.Periods.GetSingle(q => q.EndADate.Year == currentDate.Year);
            var RequestedDays = entity.RequestEndtDate.DayNumber - entity.RequestStartDate.DayNumber;
            var Allocations = _unitOfWork.LeaveAllocations.GetSingle(q => q.LeaveTypeId == entity.LeaveTypeId && q.EmployeeId == user.Id && q.PeriodId==period.Id);
            if (RequestedDays > Allocations.Days)
                return true;
            else if (RequestedDays <= 0)
                return true; 
            else return false;
        }
        public Periods GetCurrentPeriod()
        {
            var currentDate = DateTime.Now;
            return _unitOfWork.Periods.GetSingle(q => q.EndADate.Year == currentDate.Year);
        }
    }
}
//
// (case) if current allocation is 0 and the requested allocation is 0 (fix bug)
//