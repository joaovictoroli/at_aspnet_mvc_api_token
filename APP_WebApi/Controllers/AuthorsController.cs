using APP_BLL.DTO;
using APP_BLL.Entities;
using APP_DAL.Interfaces;
using APP_DAL.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;

namespace APP_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : Controller
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IAuthorBookRepository _authorBookRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public AuthorsController(IAuthorRepository authorRepository, IAuthorBookRepository authorBookRepository, IBookRepository bookRepository ,IMapper mapper)
        {
            _authorRepository = authorRepository;
            _authorBookRepository = authorBookRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAuthors()
        {
            var authors = await _authorRepository.GetAllAsync();
            var authorsDTO = _mapper.Map<List<AuthorDTO>>(authors);
            return Ok(authorsDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DetailsAuthorDTO>> GetAuthor(int id)
        {
            var author = await _authorRepository.GetAsync(id);

            var authorBooks = await _authorBookRepository.GetAllByAuthorIdAsync(id);

            var listAuthorBookDTO = new List<BookToAuthorDetails>();

            foreach (var authorBook in authorBooks)
            {
                var authorBookDTO = new BookToAuthorDetails()
                {
                    BookId = authorBook.BookId,
                    BookIsbn = await _bookRepository.GetIsbnById(authorBook.BookId)
                };

                listAuthorBookDTO.Add(authorBookDTO);
            }

            var detailsBookDTO = new DetailsAuthorDTO()
            {
                Id = id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Email= author.Email,
                BirthDate = author.BirthDate,
                AuthorBook = listAuthorBookDTO
            };

            if (author == null)
            {
                return NotFound();
            }

            return detailsBookDTO;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(AddAuthor addAuthor)
        {
            var author = _mapper.Map<AddAuthor, Author>(addAuthor);

            author = await _authorRepository.AddAsync(author);

            return CreatedAtAction("GetAuthor", new { id = author.Id }, author);
        }

        [Authorize]
        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteAuthor(int id)
        {
            if (await _authorRepository.HasAuthorBook(id))
            {
                return BadRequest("Author has books linked");
            }

            var author = await _authorRepository.DeleteAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<AuthorDTO>> PutAuthor(int id, UpdateAuthorRequest updatedAuthor)
        {
            var author = _mapper.Map<UpdateAuthorRequest, Author>(updatedAuthor);

            var existingAuthor = await _authorRepository.UpdateAsync(id, author);

            if (existingAuthor == null) { return NotFound(); }

            var authorDTO = _mapper.Map<Author, AuthorDTO>(existingAuthor);

            return Ok(authorDTO);
        }
    }
}
