using Diarista.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Diarista.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() :
           base("LocalDbConnection")
        {
            Database.Delete();
            Database.CreateIfNotExists();
            Configuration.AutoDetectChangesEnabled = false;
            Database.SetInitializer<DatabaseContext>(null);
            this.Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Perfil>()
                .HasRequired(t => t.Usuario)
                .WithRequiredPrincipal(t => t.Perfil);

            modelBuilder.Entity<Casa>()
                .HasRequired(c => c.Perfil)
                .WithMany(c => c.Casas)
                .HasForeignKey(c => c.PerfilId);

            modelBuilder.Entity<Servico>()
                .HasRequired(s => s.Contratante)
                .WithMany(c => c.ServicosSolicitados)
                .HasForeignKey(s => s.ContratanteId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Servico>()
                .HasRequired(s => s.Casa)
                .WithRequiredPrincipal();

            modelBuilder.Entity<Servico>()
                .HasOptional(s => s.Diarista)
                .WithMany(c => c.ServicosFeitos)
                .HasForeignKey(s => s.DiaristaId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Perfil> Perfis { get; set; }
        public virtual DbSet<Casa> Casas { get; set; }
        public virtual DbSet<Servico> Servicos { get; set; }
    }
}