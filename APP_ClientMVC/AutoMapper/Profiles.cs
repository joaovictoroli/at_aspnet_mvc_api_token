using APP_BLL.ViewModel;
using APP_BLL.Entities;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using APP_BLL.DTO;

namespace APP_ClientMVC.AutoMapper
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<AddAuthorViewModel, Author>().ReverseMap();
            CreateMap<UpdateAuthorRequest, EditAuthorViewModel>().ReverseMap();
            CreateMap<EditAuthorViewModel, Author>().ReverseMap();
            CreateMap<DetailsAuthorViewModel, DetailsAuthorDTO>().ReverseMap();
            CreateMap<DetailsBookViewModel, DetailsBookDTO>().ReverseMap();
            CreateMap<DetailsBookViewModel, EditBookViewModel>().ReverseMap();
            CreateMap<UpdateBookRequest, EditBookViewModel>().ReverseMap();

        }
    }
}
