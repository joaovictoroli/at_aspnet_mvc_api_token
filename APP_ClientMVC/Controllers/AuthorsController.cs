using APP_BLL.DTO;
using APP_BLL.Entities;
using APP_BLL.ViewModel;
using APP_ClientMVC.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace APP_ClientMVC.Controllers
{
    public class AuthorsController : Controller
    {
        const string apiurl = "https://localhost:7230";

        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public AuthorsController(IMapper mapper, IAuthorService authorService)
        {
            _mapper = mapper;
            _authorService = authorService;
        }

        public async Task<IActionResult> Index()
        {
            var authorsList = await _authorService.GetAuthors(HttpContext);
            return View(authorsList);
        }

        public ViewResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(AddAuthorViewModel addAuthor)
        {
            if (!ValidateAddAuthor(addAuthor))
            {
                return View(addAuthor);
            }

            Author author = _mapper.Map<Author>(addAuthor);

            await _authorService.AddAuthor(HttpContext, author);

            return RedirectToAction("Index", "Authors");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int authorId)
        {
            await _authorService.DeleteAuthor(HttpContext, authorId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            var author = await _authorService.GetAuthorToUpdate(HttpContext, id);

            EditAuthorViewModel editViewModel = _mapper.Map<EditAuthorViewModel>(author);
            return View(editViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(EditAuthorViewModel author)
        {
            if (!ValidadeEditAuthor(author))
            {
                return View(author);
            }

            var receivedAuthor = _mapper.Map<UpdateAuthorRequest>(author);

            var result = await _authorService.UpdateAuthor(HttpContext, receivedAuthor, author.Id);

            if (result)
            {
                ViewBag.Result = "Success";
            }

            return View(author);
        }

        public async Task<IActionResult> Details(int id)
        {
            var authorDetails = await _authorService.GetAuthorDetails(HttpContext, id);

            return View(authorDetails);
        }

        #region Private Methods

        private bool ValidadeEditAuthor(EditAuthorViewModel author)
        {

            if (author.BirthDate > DateTime.Today)
            {
                ModelState.AddModelError(nameof(author.BirthDate),
                    $"{nameof(author.BirthDate)} cannot be hight than current date.");
            }

            if (string.IsNullOrWhiteSpace(author.FirstName))
            {
                ModelState.AddModelError(nameof(author.BirthDate),
                    $"{nameof(author.FirstName)} cannot have no value.");
            }

            if (string.IsNullOrWhiteSpace(author.LastName))
            {
                ModelState.AddModelError(nameof(author.LastName),
                    $"{nameof(author.LastName)} cannot have no value.");
            }

            if (string.IsNullOrWhiteSpace(author.Email))
            {
                ModelState.AddModelError(nameof(author.Email),
                    $"{nameof(author.Email)} cannot have no value.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        private bool ValidateAddAuthor(AddAuthorViewModel author)
        {

            if (author.BirthDate > DateTime.Today)
            {
                ModelState.AddModelError(nameof(author.BirthDate),
                    $"{nameof(author.BirthDate)} cannot be hight than current date.");
            }

            if (string.IsNullOrWhiteSpace(author.FirstName))
            {
                ModelState.AddModelError(nameof(author.BirthDate),
                    $"{nameof(author.FirstName)} cannot have no value.");
            }

            if (string.IsNullOrWhiteSpace(author.LastName))
            {
                ModelState.AddModelError(nameof(author.LastName),
                    $"{nameof(author.LastName)} cannot have no value.");
            }

            if (string.IsNullOrWhiteSpace(author.Email))
            {
                ModelState.AddModelError(nameof(author.Email),
                    $"{nameof(author.Email)} cannot have no value.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
