using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NavneVelger.DataContexts;
using Panini.Entities;

namespace NavneVelger.Controllers
{
    public class BokTypesController : Controller
    {
        private readonly BokerDb _context;

        public BokTypesController(BokerDb context)
        {
            _context = context;
        }

        // GET: BokTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.BokTyper.ToListAsync());
        }

        // GET: BokTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bokType = await _context.BokTyper
                .SingleOrDefaultAsync(m => m.Id == id);
            if (bokType == null)
            {
                return NotFound();
            }

            return View(bokType);
        }

        // GET: BokTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BokTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type")] BokType bokType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bokType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bokType);
        }

        // GET: BokTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bokType = await _context.BokTyper.SingleOrDefaultAsync(m => m.Id == id);
            if (bokType == null)
            {
                return NotFound();
            }
            return View(bokType);
        }

        // POST: BokTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type")] BokType bokType)
        {
            if (id != bokType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bokType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BokTypeExists(bokType.Id))
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
            return View(bokType);
        }

        // GET: BokTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bokType = await _context.BokTyper
                .SingleOrDefaultAsync(m => m.Id == id);
            if (bokType == null)
            {
                return NotFound();
            }

            return View(bokType);
        }

        // POST: BokTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bokType = await _context.BokTyper.SingleOrDefaultAsync(m => m.Id == id);
            _context.BokTyper.Remove(bokType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BokTypeExists(int id)
        {
            return _context.BokTyper.Any(e => e.Id == id);
        }
    }
}
