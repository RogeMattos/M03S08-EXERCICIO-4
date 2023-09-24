
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<ProdutoModel> Produtos { get; set; }
    public DbSet<ClienteModel> Clientes { get; set; }
    public DbSet<VendaModel> Vendas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<VendaModel>()
            .HasOne(v => v.Produto)
            .WithMany(a => a.Vendas) 
            .HasForeignKey(v => v.ProdutoId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<VendaModel>()
            .HasOne(v => v.Cliente)
            .WithMany(a => a.Vendas) 
            .HasForeignKey(v => v.ClienteId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}
