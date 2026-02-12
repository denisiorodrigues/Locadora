using AutoMapper;
using Locadora.Api.Data.Dto.Endereco;
using Locadora.Api.Models;

namespace Locadora.Api.Profiles;

public class EnderecoProfile : Profile
{
    public EnderecoProfile()
    {
        CreateMap<CreateEnderecoDto, Endereco>();
        CreateMap<UpdateEnderecoDto, Endereco>().ReverseMap();
        CreateMap<Endereco, ReadEnderecoDto>();
    }
}