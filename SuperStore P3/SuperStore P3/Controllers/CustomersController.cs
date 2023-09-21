using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EcoPower_Logistics.Data;
using EcoPower_Logistics.Models;
using EcoPower_Logistics.Repository_Classes;

namespace Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private readonly ICustomer _customer;

        public CustomersController(ICustomers customer)
        {
            _customer = customer;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            return View(await _customer.GetAll());
                        
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var customer = await _customer.FindById(id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,CustomerTitle,CustomerName,CustomerSurname,CellPhone")] Customer customer)
        {
            customer.CustomerID = Guid.NewGuid();
            customer.DateCreated = DateTime.Now;
            _customer.Add(customer);
            return RedirectToAction(nameof(Index));
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            var customer = await _customer.FindById(id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CustomerId,CustomerTitle,CustomerName,CustomerSurname,CellPhone")] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            try
            {
                _customer.Edit(customer);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_customer.FindById(customer.CustomerId) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            var customer = await _customer.FindById(id);

            if (customer == null) return NotFound();

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var customer = await _customer.FindById(id);

            if (customer == null) return NotFound();

            try
            {
                _customer.Remove(customer);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_customer.FindById(customer.CustomerId) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return (_customer.Customers?.Any(e => e.CustomerId == id)).GetValueOrDefault();
        }
    }
}
