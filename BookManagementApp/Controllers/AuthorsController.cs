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
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Controller;
using MVC.Controllers.Bases;
using Microsoft.AspNetCore.Authorization;
using DataAccess.Results.Bases;
using System.Security.Policy;

//Generated from Custom Template.
namespace BookManagementApp.Controllers
{
    //[Authorize(Roles = "admin")]
    public class AuthorsController : MvcControllerBase
    {
        // TODO: Add service injections here
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        // GET: Authors
        public IActionResult Index()
        {
            List<AuthorModel> authorList = _authorService.Query().OrderBy(a => a.Name).ToList(); 
            return View(authorList);
        }

        // GET: Authors/Details/5
        public IActionResult Details(int id)
        {
            AuthorModel author = _authorService.Query().SingleOrDefault(a => a.Id == id); 
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AuthorModel author)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add insert service logic here
                Result result = _authorService.Add(author);
                if (result.IsSuccessful)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", result.Message);
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View(author);
        }

        // GET: Authors/Edit/5
        public IActionResult Edit(int id)
        {
            AuthorModel author = _authorService.Query().SingleOrDefault(a => a.Id == id); // TODO: Add get item service logic here
            if (author == null)
            {
                return NotFound();
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View(author);
        }

        // POST: Authors/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AuthorModel author)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add update service logic here
                Result result = _authorService.Update(author);
                if (result.IsSuccessful)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", result.Message);
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View(author);
        }

        // GET: Authors/Delete/5
        public IActionResult Delete(int id)
        {
            AuthorModel author = _authorService.Query().SingleOrDefault(a => a.Id == id); // TODO: Add get item service logic here
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        // POST: Authors/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // TODO: Add delete service logic here
            Result result = _authorService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
