#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CORE.Services;
using APP.Models;

// Generated from Custom MVC Template.

namespace MVC.Controllers
{
    public class BooksController : Controller
    {
        // Service injections:
        private readonly IService<BookRequest, BookResponse> _bookService;
        private readonly IService<AuthorRequest, AuthorResponse> _authorService;

        /* Can be uncommented and used for many to many relationships, "entity" may be replaced with the related entity name in the controller and views. */
        private readonly IService<GenreRequest, GenreResponse> _genreService;

        public BooksController(
			IService<BookRequest, BookResponse> bookService
            , IService<AuthorRequest, AuthorResponse> authorService

            /* Can be uncommented and used for many to many relationships, "entity" may be replaced with the related entity name in the controller and views. */
            , IService<GenreRequest, GenreResponse> genreService
        )
        {
            _bookService = bookService;
            _authorService = authorService;

            /* Can be uncommented and used for many to many relationships, "entity" may be replaced with the related entity name in the controller and views. */
            _genreService = genreService;
        }

        private void SetViewData()
        {
            /*
            ViewBag and ViewData are the same collection (dictionary).
            They carry extra data other than the model from a controller action to its view, or between views.
            */

            // Related items service logic to set ViewData (Id and Name parameters may need to be changed in the SelectList constructor according to the model):
            ViewData["AuthorId"] = new SelectList(_authorService.List(), "Id", "FullNameR");

            /* Can be uncommented and used for many to many relationships, "entity" may be replaced with the related entity name in the controller and views. */
            ViewBag.GenreIds = new MultiSelectList(_genreService.List(), "Id", "Name");
        }

        private void SetTempData(string message, string key = "Message")
        {
            /*
            TempData is used to carry extra data to the redirected controller action's view.
            */

            TempData[key] = message;
        }

        // GET: Books
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _bookService.List();
            return View(list); // return response collection as model to the Index view
        }

        // GET: Books/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _bookService.Single(id);
            return View(item); // return response item as model to the Details view
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            SetViewData(); // set ViewData dictionary to carry extra data other than the model to the view
            return View(); // return Create view with no model
        }

        // POST: Books/Create
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(BookRequest book)
        {
            if (ModelState.IsValid) // check data annotation validation errors in the request
            {
                // Insert item service logic:
                var response = _bookService.Add(book);
                if (response.IsSuccessful)
                {
                    SetTempData(response.Message); // set TempData dictionary to carry the message to the redirected action's view
                    return RedirectToAction(nameof(Details), new { id = response.Id }); // redirect to Details action with id parameter as response.Id route value
                }
                ModelState.AddModelError("", response.Message); // to display service error message in the validation summary of the view
            }
            SetViewData(); // set ViewData dictionary to carry extra data other than the model to the view
            return View(book); // return request as model to the Create view
        }

        // GET: Books/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _bookService.Edit(id);
            SetViewData(); // set ViewData dictionary to carry extra data other than the model to the view
            return View(item); // return request as model to the Edit view
        }

        // POST: Books/Edit
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(BookRequest book)
        {
            if (ModelState.IsValid) // check data annotation validation errors in the request
            {
                // Update item service logic:
                var response = _bookService.Update(book);
                if (response.IsSuccessful)
                {
                    SetTempData(response.Message); // set TempData dictionary to carry the message to the redirected action's view
                    return RedirectToAction(nameof(Details), new { id = response.Id }); // redirect to Details action with id parameter as response.Id route value
                }
                ModelState.AddModelError("", response.Message); // to display service error message in the validation summary of the view
            }
            SetViewData(); // set ViewData dictionary to carry extra data other than the model to the view
            return View(book); // return request as model to the Edit view
        }

        // GET: Books/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _bookService.Single(id);
            return View(item); // return response item as model to the Delete view
        }

        // POST: Books/Delete
        [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var response = _bookService.Remove(id);
            SetTempData(response.Message); // set TempData dictionary to carry the message to the redirected action's view
            return RedirectToAction(nameof(Index)); // redirect to the Index action
        }
    }
}
