using Locadora.Api.Data.Dto.Endereco;
using Locadora.Api.Models;

namespace Locadora.Api.Profile;

public class EnderecoProfile : AutoMapper.Profile
{
    public EnderecoProfile()
    {
        CreateMap<CreateEnderecoDto, Endereco>();
        CreateMap<UpdateEnderecoDto, Endereco>().ReverseMap();
        CreateMap<Endereco, ReadEnderecoDto>();
    }
}