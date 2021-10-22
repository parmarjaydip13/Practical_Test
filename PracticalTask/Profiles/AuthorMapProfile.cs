using AutoMapper;
using Domain;
using PracticalTask.Models;
using System.Collections.Generic;

namespace PracticalTask.Profiles
{
    public class AuthorMapProfile : Profile
    {
        public AuthorMapProfile()
        {
            CreateMap<AuthorViewModel, Author>().ForMember(dest => dest.Books, opts => opts.Ignore());
            CreateMap<Author, AuthorViewModel>();
            //.ForMember(dest => dest.Books, opts => opts.MapFrom(src => new List<Book>()))
            //.ReverseMap();

            CreateMap<AuthorEditViewModel, Author>().ForMember(dest => dest.Books, opts => opts.Ignore());
            CreateMap<Author, AuthorEditViewModel>()
            .ForMember(dest => dest.BookViewModel, opts => opts.MapFrom(src =>
                new BookViewModel { AuthorId = src.AuthorId }
            ));
        }
    }
}
