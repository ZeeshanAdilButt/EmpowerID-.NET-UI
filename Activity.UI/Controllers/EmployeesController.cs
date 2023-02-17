using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Activity.UI.Data;
using ActivityApp.Domain.Data;
using Employee.Service.Services.Interfaces;
using EmployeeApp.Application.Core.ApplicationContracts.Requests.Example;
using EmployeeApp.Application.Core.ApplicationContracts.Requests.Employee;
using EmployeeApp.Application.Core.ApplicationContracts.Responses.Example;

namespace Activity.UI.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            this._employeeService = employeeService;
        }

        // GET: Employees
        public async Task<IActionResult> Index(GetAllEmployeeRequest request)
        {
            ViewData["searchItem"] = request.searchItem;

            return View(await _employeeService.GetAllEmployeesAsync(request));
        }

        //// GET: Employees/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.Employee == null)
        //    {
        //        return NotFound();
        //    }

        //    var employee = await _context.Employee
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(employee);
        //}

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEmployeeRequest employee)
        {
            if (ModelState.IsValid)
            {

                var response = await _employeeService.CreateEmployeeAsync(employee);

                if (response.IsSuccess)
                {

                    return RedirectToAction(nameof(Index));
                }
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GetEmployeeResponse employe = await _employeeService.GetEmployeeByIdAsync(id.ToString());

            if (employe == null)
            {
                return NotFound();
            }

            return View(employe);
        }
        // POST: Activities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateEmployeeRequest Employee)
        {
            if (id != Employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _employeeService.UpdateEmployeeAsync(Employee);

                }
                catch (Exception ex)
                {

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(Employee);
        }



        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GetEmployeeResponse employee = await _employeeService.GetEmployeeByIdAsync(id.ToString());

            if (employee == null)
                return NotFound();

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            GetEmployeeResponse employee = await _employeeService.GetEmployeeByIdAsync(id.ToString());

            if (employee == null)
                return NotFound();

            await _employeeService.DeleteEmployeeAsync(new DeleteEmployeeRequest { Id = id });

            return RedirectToAction(nameof(Index));
        }
    }
}
