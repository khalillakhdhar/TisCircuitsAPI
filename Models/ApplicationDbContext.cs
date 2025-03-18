using Microsoft.EntityFrameworkCore;

namespace TisCircuitsAPI.Models
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Vos DbSet correspondants aux tables SQL
        public virtual DbSet<AccesFormation> AccesFormation { get; set; }
        public virtual DbSet<Demande> Demande { get; set; }
        public virtual DbSet<DemandeEmp> DemandeEmp { get; set; }
        public virtual DbSet<Details> Details { get; set; }
        public virtual DbSet<Formation> Formation { get; set; }
        public virtual DbSet<Fourniture> Fourniture { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<Type_Fourniture> Type_Fourniture { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuration de vos entités ici, par exemple :
            modelBuilder.Entity<AccesFormation>(entity =>
            {
                entity.HasOne(d => d.Id_formationNavigation).WithMany(p => p.AccesFormation)
                    .HasConstraintName("FK_AccesFormation_Formation");

                entity.HasOne(d => d.Id_serviceNavigation).WithMany(p => p.AccesFormation)
                    .HasConstraintName("FK_AccesFormation_Service");
            });

            modelBuilder.Entity<Demande>(entity =>
            {
                entity.HasKey(e => e.id).HasName("PK__Demande__3213E83F4DF6353E");
                entity.HasOne(d => d.id_fichierNavigation).WithMany(p => p.Demande)
                    .HasConstraintName("FK__Demande__id_fich__60A75C0F");
            });

            // Ajoutez ici les configurations pour les autres entités...

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
