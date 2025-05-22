using Microsoft.EntityFrameworkCore;

namespace TisCircuitsAPI.Models
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

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
        public virtual DbSet<DemandeConge> DemandeConges { get; set; }
        public DbSet<Cours> Cours { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public DbSet<QuizOption> QuizOptions { get; set; }
        public DbSet<QuizResult> QuizResults { get; set; }



        // ✅ Nouveau :
        public virtual DbSet<Matiere> Matiere { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            // ✅ Configuration relation Fourniture ↔ Matiere
            modelBuilder.Entity<Fourniture>(entity =>
            {
                entity.HasOne(d => d.Matiere)
                    .WithMany()
                    .HasForeignKey(d => d.MatiereId)
                    .HasConstraintName("FK_Fourniture_Matiere");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
