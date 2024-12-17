using AutoMapper;
using PLivres.Models;
using PLivres.DTOs;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Book, BookDto>()
            .ForMember(dest => dest.ImageCover, opt => opt.MapFrom(src => $"/uploads/{src.ImageCover}"));

        CreateMap<CreateBookDto, Book>()
            .ForMember(dest => dest.ImageCover, opt => opt.Ignore()); // pour gérez l'image manuellement
    }
}

