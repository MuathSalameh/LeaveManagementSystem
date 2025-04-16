using Leave.Data.Repository.IRepository;
using Leave.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.ObjectModelRemoting;

namespace LeaveManagementSystem.Controllers
{
    [Authorize(Roles =SD.Role_Admin)]
    public class PeriodController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public PeriodController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public  IActionResult Index()
        {
            var PeriodsData = _unitOfWork.Periods.GetAll();
            return View(PeriodsData);
        }
        //create 
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Periods periods)
        {
            if (periods == null) {
                return NotFound();
            }
            if (ModelState.IsValid) {
                _unitOfWork.Periods.Add(periods);
                _unitOfWork.Periods.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(periods);
        }
        //edit 
        public IActionResult Edit(int periodId)
        {
            if (periodId == 0) {
                return NotFound();
            }
            var Period = _unitOfWork.Periods.GetSingle(q=>q.Id==periodId);
            if (Period == null)
            {
                return NotFound();
            }
            return View(Period);
        }
        [HttpPost]
        public IActionResult Edit(int id , Periods periods)
        {
            if (periods == null || id ==0)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Periods.Update(periods);
                _unitOfWork.Periods.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(periods);
        }
        //delete 
        public IActionResult Delete(int periodId)
        {
            if (periodId == 0)
            {
                return NotFound();
            }
            var Period = _unitOfWork.Periods.GetSingle(q => q.Id == periodId);
            if (Period == null)
            {
                return NotFound();
            }
            return View(Period);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePeriod(int Id)
        {
            if (Id == 0)
            {
                return NotFound();
            }
            var Period = _unitOfWork.Periods.GetSingle(q => q.Id == Id);
            if (Period == null)
            {
                return NotFound();
            }
            _unitOfWork.Periods.Remove(Period);
            _unitOfWork.Periods.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
