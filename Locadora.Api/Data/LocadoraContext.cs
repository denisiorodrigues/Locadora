using Locadora.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Locadora.Api.Data;

public class LocadoraContext : IdentityDbContext<PessoaComAcesso, PerfilDeAcesso, int>
{
    public LocadoraContext(DbContextOptions<LocadoraContext> options) : base(options)
    {
        
    }

    public DbSet<Filme> Filmes { get; set; }
}