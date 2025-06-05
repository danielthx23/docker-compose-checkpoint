using CoAlert.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoAlert.Infrastructure.Data.AppData
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<UsuarioEntity> Usuario { get; set; }
        public DbSet<PostagemEntity> Postagem { get; set; }
        public DbSet<LocalizacaoEntity> Localizacao { get; set; }
        public DbSet<ComentarioEntity> Comentario { get; set; }
        public DbSet<CategoriaDesastreEntity> CategoriaDesastre { get; set; }
        public DbSet<LikeEntity> Like { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // POSTAGEM - muitos para um com USUARIO
            modelBuilder.Entity<PostagemEntity>()
                .HasOne(p => p.Usuario)
                .WithMany()  // Usuario não tem coleção de Postagens
                .HasForeignKey(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // POSTAGEM - muitos para um com CATEGORIA_DESASTRE
            modelBuilder.Entity<PostagemEntity>()
                .HasOne(p => p.CategoriaDesastre)
                .WithMany()  // CategoriaDesastre não tem coleção de Postagens
                .HasForeignKey(p => p.CategoriaDesastreId)
                .OnDelete(DeleteBehavior.Cascade);

            // POSTAGEM - muitos para um com LOCALIZACAO
            modelBuilder.Entity<PostagemEntity>()
                .HasOne(p => p.Localizacao)
                .WithMany()  // Localizacao não tem coleção de Postagens
                .HasForeignKey(p => p.LocalizacaoId)
                .OnDelete(DeleteBehavior.Cascade);

            // POSTAGEM - um para muitos com COMENTARIOS
            modelBuilder.Entity<PostagemEntity>()
                .HasMany(p => p.Comentarios)
                .WithOne(c => c.Postagem)
                .HasForeignKey(c => c.PostagemId)
                .OnDelete(DeleteBehavior.Cascade);

            // COMENTARIO - muitos para um com USUARIO
            modelBuilder.Entity<ComentarioEntity>()
                .HasOne(c => c.Usuario)
                .WithMany()  // Usuario não tem coleção de Comentarios
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // COMENTARIO - relacionamento recursivo (respostas a comentários)
            modelBuilder.Entity<ComentarioEntity>()
                .HasOne<ComentarioEntity>()
                .WithMany()
                .HasForeignKey(c => c.IdComentarioParente)
                .OnDelete(DeleteBehavior.Cascade);

            // LIKE - muitos para um com USUARIO
            modelBuilder.Entity<LikeEntity>()
                .HasOne(l => l.Usuario)
                .WithMany()  // Usuario não tem coleção de Likes
                .HasForeignKey(l => l.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // LIKE - muitos para um com POSTAGEM (opcional)
            modelBuilder.Entity<LikeEntity>()
                .HasOne(l => l.Postagem)
                .WithMany()  // Postagem não tem coleção de Likes
                .HasForeignKey(l => l.PostagemId)
                .OnDelete(DeleteBehavior.Cascade);

            // LIKE - muitos para um com COMENTARIO (opcional)
            modelBuilder.Entity<LikeEntity>()
                .HasOne(l => l.Comentario)
                .WithMany()  // Comentario não tem coleção de Likes
                .HasForeignKey(l => l.ComentarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Ensure a like is either for a post or a comment, not both
            modelBuilder.Entity<LikeEntity>()
                .HasCheckConstraint("CK_Like_PostOrComment", 
                    "(ID_POSTAGEM IS NULL AND ID_COMENTARIO IS NOT NULL) OR (ID_POSTAGEM IS NOT NULL AND ID_COMENTARIO IS NULL)");
        }
    }
}
