using Locadora.Api.Data.Dto;
using Locadora.Api.Models;

namespace Locadora.Api.Profile;

public class FilmeProfile : AutoMapper.Profile
{
    public FilmeProfile()
    {
        CreateMap<CreateFilmeDto, Filme>();
    }
}