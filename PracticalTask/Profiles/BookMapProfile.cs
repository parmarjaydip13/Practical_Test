using AutoMapper;
using Domain;
using PracticalTask.Models;
using System;
using System.Globalization;

namespace PracticalTask.Profiles
{

    public class BookMapProfile : Profile
    {
        public BookMapProfile()
        {
            CreateMap<BookViewModel, Book>().ForMember(dest => dest.PublishDate, member => member.MapFrom(src => DateTime.ParseExact(src.PublishDate, "dd-MM-yyyy", CultureInfo.InvariantCulture)));
            CreateMap<Book, BookViewModel>()
                .ForMember(dest => dest.PublishDate, member => member.MapFrom(src => src.PublishDate.ToString("dd-MM-yyyy")))
                .ForMember(dest => dest.PublishDateDt, member => member.MapFrom(src => src.PublishDate));
            ;
        }
    }
}
