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

namespace EcoPower_Logistics.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProducts _products;
        private readonly SuperStoreContext _products;

        public ProductsController(SuperStoreContext products)
        {
            _products = products;
        }

        // GET: Products
       
        public async Task<IActionResult> Index()
        {
            return View(await _products.GetAll());
        }

        // GET:Products/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            var products = await _products.FindById(id);

            if (products == null) return NotFound();

            return View(products);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ProductsId,ProductsName,ProductsDescription,DateCreated")] Products products)
        {
            products.ProductsId = Guid.NewGuid();

            _products.Add(category);

            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            var products = await _products.FindById(id);

            if (products == null) return NotFound();

            return View(products);
        }

        // POST: Productss/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("ProductsId,ProductsName,ProductsDescription,DateCreated")] Products products)
        {
            if (id != products.ProductsId)
            {
                return NotFound();
            }

            try
            {
                _products.Edit(products);
            }
            catch (DbUpdateConcurrencyException)
            {
                // Check if the Id even exists
                if (_products.FindById(products.ProductsId) == null)
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

        public async Task<IActionResult> Delete(Guid? id)
        {
            var products = await _products.FindById(id);

            if (products == null) return NotFound();

            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var products = await _products.FindById(id);

            if (products == null) return NotFound();

            try
            {
                _products.Remove(products);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_products.FindById(products.ProductsId) == null)
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

        private bool ProductExists(int id)
        {
            return (_products.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
