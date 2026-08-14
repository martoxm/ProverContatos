using Microsoft.EntityFrameworkCore;
using ProverContatos.Domain.Entities;

namespace ProverContatos.Infrastructure.Data;

public class ProverContatosDbContext(DbContextOptions<ProverContatosDbContext> options) : DbContext(options)
{
    public DbSet<Contato> Contatos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contato>(entity =>
        {
            entity.ToTable("Contatos");
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Nome).IsRequired().HasMaxLength(100);
            entity.Property(c => c.DataNascimento).IsRequired();
            entity.Property(c => c.Sexo).IsRequired();
            entity.Property(c => c.Ativo).IsRequired();
            entity.Ignore(c => c.Idade);
        });

        base.OnModelCreating(modelBuilder);
    }
}