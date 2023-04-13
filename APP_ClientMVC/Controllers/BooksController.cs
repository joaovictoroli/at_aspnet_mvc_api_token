using APP_BLL.DTO;
using APP_BLL.Entities;
using APP_BLL.ViewModel;
using APP_ClientMVC.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace APP_ClientMVC.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;

        private readonly IMapper _mapper;
        
        private readonly IAuthorService _authorService;
        public BooksController(IBookService bookService, IAuthorService authorService,  IMapper mapper)
        {          
            _bookService = bookService;
            _authorService = authorService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {            
            var booksList = await _bookService.GetAllBooks(HttpContext);
            return View(booksList);
        }

        public async Task<IActionResult> Create()
        {
            var authorList = await _authorService.GetAuthors(HttpContext);
            AddBookViewModel addBook = new AddBookViewModel();
            var authors = new List<AddBookAuthorViewModel>(); 
            foreach ( var author in authorList)
            {
                var authorBookViewModel = new AddBookAuthorViewModel()
                {
                    AuthorId = author.Id,
                    FullName = author.FirstName + " " + author.LastName,
                };
                authors.Add(authorBookViewModel);
                              
            }
            addBook.authorBooks = authors;

            return View(addBook);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddBook addBook, List<string> authorsIds)
        {
            if (authorsIds.Any())
            {
                addBook.AuthorsId = authorsIds.ConvertAll(s => Int32.Parse(s));

                var response = await _bookService.AddBook(HttpContext, addBook);
                return RedirectToAction("Index", "Books");
            }
           
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int bookId)
        {
            await _bookService.DeleteBook(HttpContext, bookId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var bookDetails = await _bookService.GetBookDetails(HttpContext, id);

            return View(bookDetails);
        }

        public async Task<IActionResult> Update(int id)
        {
            var book = await _bookService.GetBookToUpdate(HttpContext, id);

            var editViewModel = new EditBookViewModel()
            {
                Id = id,
                Title = book.Title,
                Isbn= book.Isbn,
                ReleaseDate = book.ReleaseDate
            };

            var authorList = await _authorService.GetAuthors(HttpContext);

            var authors = new List<AddBookAuthorViewModel>();

            foreach (var author in authorList)
            {
                var authorBookViewModel = new AddBookAuthorViewModel()
                {
                    AuthorId = author.Id,
                    FullName = author.FirstName + " " + author.LastName,
                };
                authors.Add(authorBookViewModel);

            }
            editViewModel.AuthorBook = authors;            

            return View(editViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(EditBookViewModel book, List<string> authorsIds)
        {          
            if (authorsIds.Any())
            {
                ViewBag.Result = "Success";
                var listAuthorsIds = new List<int>();
                book.AuthorsId = authorsIds.ConvertAll(s => Int32.Parse(s));

                var updateBookRequest = new UpdateBookRequest()
                {
                    Id = book.Id,
                    Title = book.Title,
                    Isbn = book.Isbn,
                    ReleaseDate = book.ReleaseDate,
                    AuthorsId = book.AuthorsId
                };

                await _bookService.UpdateBookRequest(HttpContext, updateBookRequest);
            } else
            {
                ViewBag.Result = "Failure";
            }

            return View(book);
        }
        #region private methods
        private async Task<AddBookViewModel> GetAuthorsBooks()
        {
            var authorList = await _authorService.GetAuthors(HttpContext);
            AddBookViewModel addBook = new AddBookViewModel();
            var authors = new List<AddBookAuthorViewModel>();
            foreach (var author in authorList)
            {
                var authorBookViewModel = new AddBookAuthorViewModel()
                {
                    AuthorId = author.Id,
                    FullName = author.FirstName + " " + author.LastName,
                };
                authors.Add(authorBookViewModel);

            }
            addBook.authorBooks = authors;
            return addBook;
        }
        #endregion
    }
}
