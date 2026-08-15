using Microsoft.EntityFrameworkCore;
using ProverContatos.Domain.Entities;

namespace ProverContatos.Infrastructure.Data;

public class ProverContatosDbContext(
    DbContextOptions<ProverContatosDbContext> options) : DbContext(options)
{
    public DbSet<Contato> Contatos { get; set; }

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contato>(entity =>
        {
            entity.ToTable("Contatos");

            entity.HasKey(contato => contato.Id);

            entity.Property(contato => contato.Nome)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(contato => contato.DataNascimento)
                .IsRequired();

            entity.Property(contato => contato.Sexo)
                .IsRequired();

            entity.Property(contato => contato.Ativo)
                .IsRequired();

            entity.Ignore(contato => contato.Idade);
        });

        base.OnModelCreating(modelBuilder);
    }
}