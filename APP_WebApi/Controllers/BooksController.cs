using APP_BLL.DTO;
using APP_BLL.Entities;
using APP_DAL.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics.Metrics;

namespace APP_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : Controller
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorBookRepository _authorBookRepository;
        private readonly IMapper _mapper;

        public BooksController(IAuthorRepository authorRepository, IBookRepository bookRepository, IAuthorBookRepository authorBookRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
            _authorBookRepository = authorBookRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks()
        {
            var books = await _bookRepository.GetAllAsync();

            var booksDTO = _mapper.Map<List<BookDTO>>(books);
            return Ok(booksDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DetailsBookDTO>> GetBook(int id)
        {
            var book = await _bookRepository.GetAsync(id);

            var authorBook = await _authorBookRepository.GetAllByBookIdAsync(id);

            var listAuthorBookDTO = new List<AuthorBookDTO>();

            foreach (var item in authorBook)
            {
                var authorBookDTO = new AuthorBookDTO()
                {
                    AuthorId = item.AuthorId,
                    AuthorName = await _authorRepository.GetFullNameById(item.AuthorId)
                };

                listAuthorBookDTO.Add(authorBookDTO);
            }
            var detailsBookDTO = new DetailsBookDTO()
            {
                Id = id,
                Title = book.Title,
                Isbn = book.Isbn,
                ReleaseDate = book.ReleaseDate,
                AuthorBook = listAuthorBookDTO
            };

            if (detailsBookDTO == null)
            {
                return NotFound();
            }

            return detailsBookDTO;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(AddBook addbook)
        {
            var book = new Book()
            {
                Title = addbook.Title,
                Isbn = addbook.Isbn,
                ReleaseDate = addbook.ReleaseDate
            };

            book = await _bookRepository.AddAsync(book);

            var authorsBookList = new List<AuthorBook>();
            foreach (var item in addbook.AuthorsId)
            {
                AuthorBook authorBook = new AuthorBook(item, book.Id);
                authorsBookList.Add(authorBook);
            };

            var response = await _authorBookRepository.AddAsync(authorsBookList);
            book.AuthorBook = response;
            //book = await _bookRepository.AddAsync(book);          

            return Ok(book);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _bookRepository.DeleteAsync(id);
            await _authorBookRepository.DeleteAllLinkedAuthorBooks(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, UpdateBookRequest updatedBook)
        {
            var book = _mapper.Map<UpdateBookRequest, Book>(updatedBook);

            await _authorBookRepository.DeleteAllLinkedAuthorBooks(id);

            var existingBook = await _bookRepository.UpdateAsync(id, book);

            var authorsBookList = new List<AuthorBook>();
            foreach (var item in updatedBook.AuthorsId)
            {
                AuthorBook authorBook = new AuthorBook(item, book.Id);
                authorsBookList.Add(authorBook);
            };

            var response = await _authorBookRepository.AddAsync(authorsBookList);
            book.AuthorBook = response;

            if (existingBook == null) { return NotFound(); }

            var bookDTO = _mapper.Map<Book, BookDTO>(existingBook);

            return CreatedAtAction("GetBook", new { id = id }, bookDTO);
        }
    }
}
