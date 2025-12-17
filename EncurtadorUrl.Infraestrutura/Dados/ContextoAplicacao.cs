using Microsoft.EntityFrameworkCore;
using EncurtadorUrl.Dominio.Entidades;

namespace EncurtadorUrl.Infraestrutura.Dados;

public class ContextoAplicacao : DbContext
{
    public ContextoAplicacao(DbContextOptions<ContextoAplicacao> options)
        : base(options) { }

    public DbSet<UrlEncurtada> UrlsEncurtadas => Set<UrlEncurtada>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UrlEncurtada>()
            .HasIndex(x => x.CodigoCurto)
            .IsUnique();
    }
}