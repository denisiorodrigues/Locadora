using AutoMapper;
using Locadora.Api.Data.Dto.Cinema;
using Locadora.Api.Models;

namespace Locadora.Api.Profiles;

public class CinemaProfile : Profile
{
    public CinemaProfile()
    {
        CreateMap<CreateCinemaDto, Cinema>();
        CreateMap<UpdateCinemaDto, Cinema>().ReverseMap();
        CreateMap<Cinema, ReadCinemaDto>()
            .ForMember(cinemaDto => cinemaDto.Endereco, 
                opt => opt.MapFrom(cinema => cinema.Endereco));
    }
}