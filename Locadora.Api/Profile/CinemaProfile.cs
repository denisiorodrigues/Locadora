using Locadora.Api.Data.Dto.Cinema;
using Locadora.Api.Data.Dto.Filmes;
using Locadora.Api.Models;

namespace Locadora.Api.Profile;

public class CinemaProfile : AutoMapper.Profile
{
    public CinemaProfile()
    {
        CreateMap<CreateFilmeDto, Cinema>();
        CreateMap<UpdateCinemaDto, Cinema>();
        CreateMap<Cinema, ReadCinemaDto>();
    }
}