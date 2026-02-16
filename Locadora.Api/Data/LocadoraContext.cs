using Locadora.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Locadora.Api.Data;

public class LocadoraContext : DbContext
{
    public LocadoraContext(DbContextOptions<LocadoraContext> options) : base(options)
    {
    }

    public DbSet<Filme> Filmes { get; set; }
    public DbSet<Cinema> Cinemas { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<Sessao> Sessoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Sessao>()
            .HasKey(s => new { s.FilmeId, s.CinemaId });
        
        modelBuilder.Entity<Sessao>()
            .HasOne(s => s.Cinema)
            .WithMany(c => c.Sessoes)
            .HasForeignKey(f => f.CinemaId);
        
        modelBuilder.Entity<Sessao>()
            .HasOne(s => s.Filme)
            .WithMany(c => c.Sessoes)
            .HasForeignKey(f => f.FilmeId);
    }
}