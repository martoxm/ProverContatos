using Microsoft.EntityFrameworkCore;
using ProverContatos.Domain.Entities;

namespace ProverContatos.Infrastructure.Data;

public class ProverContatosDbContext(DbContextOptions<ProverContatosDbContext> options) : DbContext(options)
{
    public DbSet<Contato> Contatos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)//LINQ to Entities garente que o EF converta corretamente as queries em C# para SQL, evitando erros de tradução e garantindo que as consultas sejam executadas corretamente no banco de dados.
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