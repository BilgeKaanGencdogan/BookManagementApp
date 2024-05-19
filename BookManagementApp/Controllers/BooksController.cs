#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Contexts;
using DataAccess.Entities;
using Business.Services;
using Business.Model;
using MVC.Controllers.Bases;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

//Generated from Custom Template.
namespace BookManagementApp.Controllers
{
    
    public class BooksController : MvcControllerBase
    {
        // TODO: Add service injections here
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly IGenreService  _genreService;

        public BooksController(IBookService bookService, IAuthorService authorService, IGenreService genreService)
        {
            _bookService = bookService;
            _authorService = authorService;
            _genreService = genreService;
        }

        // GET: Books
        public IActionResult Index()
        {
            List<BookModel> bookList = _bookService.Query().OrderBy(b => b.Name).ToList();
            return View(bookList);
        }

        // GET: Books/Details/5
        public IActionResult Details(int id)
        {
			BookModel book = _bookService.GetItem(id); // TODO: Add get item service logic here
			if (book == null)
			{
				
				return View("Error", $"Book with ID {id} not found!");
			}
			return View(book);
		}

        // GET: Books/Create
       
        public IActionResult Create()
        {
			// TODO: Add get related items service logic here to set ViewData if necessary
			ViewData["AuthorId"] = new SelectList(_authorService.Query().ToList(), "Id", "Name");
			ViewBag.Genres = new MultiSelectList(_genreService.GetList(), "Id", "Name");
			return View();
		
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
 

        public IActionResult Create(BookModel book)
        {
			if (ModelState.IsValid)
			{
				var result = _bookService.Add(book);
				if (result.IsSuccessful)
					return RedirectToAction(nameof(Details), new { id = book.Id });
			}
			ViewData["AuthorId"] = new SelectList(_authorService.Query().ToList(), "Id", "Name");
			ViewBag.Genres = new MultiSelectList(_genreService.GetList(), "Id", "Name");
			return View(book);
		}

        // GET: Books/Edit/5
        
        public IActionResult Edit(int id)
        {
			BookModel book = _bookService.GetItem(id);
			if (book == null)
			{
				return View("Error", $"Book with ID {id} not found!");
			}
			ViewData["AuthorId"] = new SelectList(_authorService.Query().ToList(), "Id", "Name");
			ViewBag.Genres = new MultiSelectList(_genreService.GetList(), "Id", "Name");
			return View(book);
		}

        // POST: Books/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       

        public IActionResult Edit(BookModel book)
        {
			if (ModelState.IsValid)
			{
				var result = _bookService.Update(book);
				if (result.IsSuccessful)
					return RedirectToAction(nameof(Details), new { id = book.Id });
			}
			ViewData["AuthorId"] = new SelectList(_authorService.Query().ToList(), "Id", "Name");
			ViewBag.Genres = new MultiSelectList(_genreService.GetList(), "Id", "Name");
			return View(book);
		}

        // GET: Books/Delete/5
        
        public IActionResult Delete(int id)
        {
            BookModel book = _bookService.Query().SingleOrDefault(b => b.Id == id); // TODO: Add get item service logic here
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
       

        public IActionResult DeleteConfirmed(int id)
        {
            var result = _bookService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
