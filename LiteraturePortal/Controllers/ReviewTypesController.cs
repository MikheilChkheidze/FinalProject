using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LiteraturePortal.Data;
using LiteraturePortal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using LiteraturePortal.Utility;

namespace LiteraturePortal.Controllers
{
    [Authorize(Roles =SD.AdminEndUser)]
    public class ReviewTypesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ReviewTypesController(ApplicationDbContext db)
        {
            _db = db;
        }

        //GET : ReviewTypes
        public IActionResult Index()
        {
            return View(_db.ReviewTypes.ToList());
        }


        //GET: ReviewTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        //POST : Reviews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReviewType serviceType)
        {
            if (ModelState.IsValid)
            {
                _db.Add(serviceType);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(serviceType);
        }

        //Details : ReviewTypes/Details/1
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var serviceType = await _db.ReviewTypes.SingleOrDefaultAsync(m => m.Id == id);
            if (serviceType == null)
            {
                return NotFound();
            }
            return View(serviceType);
        }

        //Edit : ReviewTypes/Edit/1
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var serviceType = await _db.ReviewTypes.SingleOrDefaultAsync(m => m.Id == id);
            if (serviceType == null)
            {
                return NotFound();
            }
            return View(serviceType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,ReviewType serviceType)
        {
            if (id != serviceType.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Update(serviceType);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(serviceType);
        }


        //Delete : ReviewTypes/Delete/1
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var serviceType = await _db.ReviewTypes.SingleOrDefaultAsync(m => m.Id == id);
            if (serviceType == null)
            {
                return NotFound();
            }
            return View(serviceType);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveReviewType(int id)
        {
            var serviceType = await _db.ReviewTypes.SingleOrDefaultAsync(m => m.Id == id);
            _db.ReviewTypes.Remove(serviceType);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
        }
    }
}