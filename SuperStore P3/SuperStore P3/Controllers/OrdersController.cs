using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EcoPower_Logistics.Models;
using EcoPower_Logistics.Data;
using EcoPower_Logistics.Repository_Classes;

namespace EcoPower_Logistics.Controllers
{
   
    public class OrdersController : Controller
    {
        private readonly SuperStoreContext _context;
        private readonly IOrders _orders;

        public OrdersController(SuperStoreContext context, IOrders orders)
        {
            _context = context;
            _orders = orders;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View(await _orders.GetAll());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            var orders = await _orders.FindById(id);
            if (orders == null)
            {
                return NotFound();
            }
            return View(orders);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,OrderDate,CustomerId,DeliveryAddress")] Order order)
        {
            order.OrderId = Guid.NewGuid();
            _orders.Add(order);
            return RedirectToAction(nameof(Index));
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            var order = await _orders.FindById(id);
            if (order = null) return NotFound();

            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", order.CustomerId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("OrderId,OrderDate,CustomerId,DeliveryAddress")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }
            try
            {
                _orders.Edit(order);
            }
            catch (DBUpdateConcurrencyException)
            {
                if (_orders.FindById(order.OrderId) == null)
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

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            var order = await _orders.FindById(id);

            if (order == null) return NotFound();

            return View(order);
        }
    }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
        var order = await _orders.FindById(id);
        if (order == null) return NotFound();
        try
        {
            _orders.Remove(device);
        }
        catch (DbUpdateConcurrencyException)
        {
            if(_orders.FindById(order.ProductId) == null)
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

        private bool OrderExists(int id)
        {
            return (_context.Orders?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    
}
