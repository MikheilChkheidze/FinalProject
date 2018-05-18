using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LiteraturePortal.Data;
using System.Security.Claims;
using LiteraturePortal.ViewModel;
using LiteraturePortal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace LiteraturePortal.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {

        private readonly ApplicationDbContext _db;

        public BooksController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index(string userId = null)
        {
            if (userId == null)
            {
                //Only called when a customer logs in
                userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            var model = new BookAndWriterViewModel
            {
                Books = _db.Books.Where(c=>c.UserId == userId),
                UserObj = _db.Users.FirstOrDefault(u=>u.Id==userId)
            };

            return View(model);
        }

        //Create GET
        public IActionResult Create(string userId)
        {
            Book bookObj = new Book
            {
                Year = DateTime.Now.Year,
                UserId = userId
            };
            return View(bookObj);
        }


        //Create POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _db.Add(book);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { userId = book.UserId });
            }

            return View(book);
        }

        //Details GET
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _db.Books
                .Include(c => c.ApplicationUser)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (book == null)
            {
                NotFound();
            }

            return View(book);
        }

        //EDIT GET
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _db.Books
                .Include(c => c.ApplicationUser)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (book == null)
            {
                NotFound();
            }

            return View(book);
        }

        //EDIT POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Update(book);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { userId = book.UserId });
            }

            return View(book);
        }



        //Delete GET
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _db.Books
                .Include(c => c.ApplicationUser)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (book == null)
            {
                NotFound();
            }

            return View(book);
        }


        //Delete POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _db.Books.SingleOrDefaultAsync(c => c.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            _db.Books.Remove(book);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { userId = book.UserId });
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