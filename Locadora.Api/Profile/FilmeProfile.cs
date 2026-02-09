using Locadora.Api.Data.Dto;
using Locadora.Api.Data.Dto.Filmes;
using Locadora.Api.Models;

namespace Locadora.Api.Profile;

public class FilmeProfile : AutoMapper.Profile
{
    public FilmeProfile()
    {
        CreateMap<CreateFilmeDto, Filme>();
        CreateMap<UpdateFilmeDto, Filme>();
        CreateMap<Filme, UpdateFilmeDto>();
        CreateMap<Filme, ReadFilmeDto>();
    }
}
