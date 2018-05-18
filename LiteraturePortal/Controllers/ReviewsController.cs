using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LiteraturePortal.Data;
using LiteraturePortal.ViewModel;
using Microsoft.EntityFrameworkCore;
using LiteraturePortal.Models;
using Microsoft.AspNetCore.Authorization;
using LiteraturePortal.Utility;

namespace LiteraturePortal.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ReviewsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Authorize]
        public IActionResult Index(int bookId)
        {
            var book = _db.Books.FirstOrDefault(c => c.Id == bookId);
            var model = new BookAndReviewsViewModel
            {
                bookId = book.Id,
                Author = book.Author,
                Title = book.Title,
                Genre = book.Genre,
                ISBN = book.ISBN,
                Year = book.Year,
                UserId = book.UserId,
                ReviewTypesObj = _db.ReviewTypes.ToList(),
                PastReviewsObj = _db.Reviews.Where(s => s.BookId == bookId).OrderByDescending(s => s.DateAdded)
            };

            return View(model);
        }

        [Authorize(Roles =SD.AdminEndUser)]
        //GET : Reviews/Create
        public IActionResult Create(int bookId)
        {
            var book = _db.Books.FirstOrDefault(c => c.Id == bookId);
            var model = new BookAndReviewsViewModel
            {
                bookId=book.Id,
                Author=book.Author,
                Title=book.Title,
                Genre = book.Genre,
                ISBN=book.ISBN,
                Year=book.Year,
                UserId=book.UserId,
                ReviewTypesObj = _db.ReviewTypes.ToList(),
                PastReviewsObj = _db.Reviews.Where(s => s.BookId == bookId).OrderByDescending(s => s.DateAdded).Take(5)
            };

            return View(model);
        }

        [Authorize(Roles = SD.AdminEndUser)]
        //POST : Reviews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookAndReviewsViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.NewReviewObj.BookId = model.bookId;
                model.NewReviewObj.DateAdded = DateTime.Now;
                _db.Add(model.NewReviewObj);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Create), new { bookId = model.bookId });
            }
            var book = _db.Books.FirstOrDefault(c => c.Id == model.bookId);
            var newModel = new BookAndReviewsViewModel
            {
                bookId = book.Id,
                Author = book.Author,
                Title = book.Title,
                Genre = book.Genre,
                ISBN = book.ISBN,
                Year = book.Year,
                UserId = book.UserId,
                ReviewTypesObj = _db.ReviewTypes.ToList(),
                PastReviewsObj = _db.Reviews.Where(s => s.BookId == model.bookId).OrderByDescending(s => s.DateAdded).Take(5)
            };
            return View(newModel);
        }

        [Authorize(Roles = SD.AdminEndUser)]
        //DELETE GET
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _db.Reviews.Include(s => s.Book).Include(s => s.ReviewType)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        [Authorize(Roles = SD.AdminEndUser)]
        //POST Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Review model)
        {
            var serviceId = model.Id;
            var bookId = model.BookId;
            var service = await _db.Reviews.SingleOrDefaultAsync(m => m.Id == serviceId);
            if (service == null)
            {
                return NotFound();
            }
            _db.Reviews.Remove(service);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Create), new { bookId = bookId });
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