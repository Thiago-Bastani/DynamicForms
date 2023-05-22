using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public virtual DbSet<Formulario> Formularios { get; set; }
    public virtual DbSet<Tipo> Tipos { get; set; }
    public virtual DbSet<Campo> Campos { get; set; }
    public virtual DbSet<RegistroViewModel> Registros { get; set; }
    public virtual DbSet<RegistroInfoViewModel> RegistrosInfo { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=localhost;Database=Formulario;Trusted_Connection=True;TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Formulario>(form
            => form.HasMany("Campos")
            .WithMany("Formularios"));

        modelBuilder.Entity<Campo>(campo
             => campo.HasAlternateKey(campo => campo.Nome));

        base.OnModelCreating(modelBuilder);
    }


}