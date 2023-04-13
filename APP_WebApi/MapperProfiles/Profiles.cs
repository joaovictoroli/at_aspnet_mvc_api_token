using APP_BLL.DTO;
using APP_BLL.Entities;
using APP_BLL.ViewModel;
using AutoMapper;

namespace APP_WebApi.MapperProfiles
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<AddAuthor, Author>().ReverseMap();
            CreateMap<UpdateAuthorRequest, Author>().ReverseMap();
            CreateMap<Author, AuthorDTO>().ReverseMap();

            CreateMap<AddBook, Book>().ReverseMap();

            CreateMap<BookDTO, Book>().ReverseMap();
            CreateMap<UpdateBookRequest, Book>().ReverseMap();
            //CreateMap<BookDTO, Book>()
              //  .ForMember(dest => dest.AuthorBook, opt => opt.MapFrom(x => x.AuthorBook)).ReverseMap();

            //CreateMap<AuthorBookDTO, AuthorBook>().ReverseMap();
        }        
    }
}
