using Locadora.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Locadora.Api.Data;

public class LocadoraContext : DbContext
{
    public LocadoraContext(DbContextOptions<LocadoraContext> options) : base(options)
    {
        
    }

    public DbSet<Filme> Filmes { get; set; }
}