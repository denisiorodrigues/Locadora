using AutoMapper;
using Locadora.Api.Data.Dto.Sessao;
using Locadora.Api.Models;

namespace Locadora.Api.Profiles;

public class SessaoProfile :  Profile
{
    public SessaoProfile()
    {
        CreateMap<CreateSessaoDto, Sessao>();
        CreateMap<Sessao, ReadSessaoDto>();
    }
}