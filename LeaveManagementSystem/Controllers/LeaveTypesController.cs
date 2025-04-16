using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Leave.Model.Models;
using Leave.Data.Data;
using Leave.Data.Repository.IRepository;
using Leave.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Leave.Utility;

namespace LeaveManagementSystem.Controllers
{
    [Authorize(Roles =SD.Role_Admin)]
    public class LeaveTypesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public LeaveTypesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork; 
        }

        // GET: LeaveTypes
        public  IActionResult Index()
        {
            IEnumerable<LeaveType> data = _unitOfWork.Leavetypes.GetAll().ToList();
            return View(data);
        }

        // GET: LeaveTypes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveType = _unitOfWork.Leavetypes.GetSingle(m => m.Id == id);
            if (leaveType == null)
            {
                return NotFound();
            }

            return View(leaveType);
        }

        // GET: LeaveTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LeaveType leaveType)
        {
            //var UserExist = _unitOfWork.Leavetypes.FirstOrDefault(u=>u.Name== leaveType.Name);
            //if (UserExist != null) {
            //    ModelState.AddModelError(nameof(leaveType.Name), $"The name {leaveType.Name} already Exists!");
            //}
            //LeaveTypeNameExists(leaveType);
            if (ModelState.IsValid)
            {
                _unitOfWork.Leavetypes.Add(leaveType);
                _unitOfWork.Leavetypes.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(leaveType);
        }

        // GET: LeaveTypes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveType = _unitOfWork.Leavetypes.GetSingle(u=>u.Id==id);
            if (leaveType == null)
            {
                return NotFound();
            }
            return View(leaveType);
        }

        // POST: LeaveTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(LeaveType leaveType)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Leavetypes.Update(leaveType);
                _unitOfWork.Leavetypes.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(leaveType);
        }

        // GET: LeaveTypes/Delete/5
        public  IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveType =  _unitOfWork.Leavetypes.GetSingle(m => m.Id == id);
            if (leaveType == null)
            {
                return NotFound();
            }

            return View(leaveType);
        }

        // POST: LeaveTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public  ActionResult DeleteConfirmed(int id)
        {
            var leaveType =  _unitOfWork.Leavetypes.GetSingle(u=>u.Id==id);
            if (leaveType != null)
            {
                _unitOfWork.Leavetypes.Remove(leaveType);
            }

            _unitOfWork.Leavetypes.Save();
            return RedirectToAction(nameof(Index));
        }

        //private bool LeaveTypeExists(int id)
        //{
        //    return _unitOfWork.Leavetypes.GetSingle(e => e.Id == id).Id > 0;
        //}
        //private void LeaveTypeNameExists(LeaveType leaveType)
        //{
        //    var UserExist = _unitOfWork.Leavetypes.GetSingle(u => u.Name.ToLower() == leaveType.Name.ToLower());
        //    if (UserExist != null)
        //    {
        //      ModelState.AddModelError(nameof(leaveType.Name), $"The name {leaveType.Name} already Exists!");
        //    }
        //}
    }
}
