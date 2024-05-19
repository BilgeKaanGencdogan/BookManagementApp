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
using DataAccess.Results.Bases;
using Microsoft.AspNetCore.Authorization;

//Generated from Custom Template.
namespace BookManagementApp.Controllers
{
   
    public class GenresController : Controller
    {
        // TODO: Add service injections here
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        // GET: Genres
        public IActionResult Index()
        {
            List<GenreModel> genreList = _genreService.Query().OrderBy(g => g.Name).ToList();
            return View(genreList);
        }

        // GET: Genres/Details/5
        public IActionResult Details(int id)
        {
            GenreModel genre = _genreService.Query().SingleOrDefault(g => g.Id == id);
            if (genre == null)
            {
                return NotFound();
            }
            return View(genre);
        }

        // GET: Genres/Create
       
        public IActionResult Create()
        {
            // TODO: Add get related items service logic here to set ViewData if necessary
            			ViewBag.Genres = new MultiSelectList(_genreService.GetList(), "Id", "Name");

            return View();
        }

        // POST: Genres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       

        public IActionResult Create(GenreModel genre)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add insert service logic here
                Result result = _genreService.Add(genre);
                if (result.IsSuccessful)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", result.Message);
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View(genre);
        }

        // GET: Genres/Edit/5
       
        public IActionResult Edit(int id)
        {
            GenreModel genre = _genreService.Query().SingleOrDefault(g => g.Id == id); // TODO: Add get item service logic here
            if (genre == null)
            {
                return NotFound();
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View(genre);
        }

        // POST: Genres/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       

        public IActionResult Edit(GenreModel genre)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add update service logic here
                Result result = _genreService.Update(genre);
                if (result.IsSuccessful)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", result.Message);
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View(genre);
        }

        // GET: Genres/Delete/5
       
        public IActionResult Delete(int id)
        {
            GenreModel genre = _genreService.Query().SingleOrDefault(g => g.Id == id); // TODO: Add get item service logic here
            if (genre == null)
            {
                return NotFound();
            }
            return View(genre);
        }

        // POST: Genres/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
      

        public IActionResult DeleteConfirmed(int id)
        {
            // TODO: Add delete service logic here
            Result result = _genreService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
