using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Core.Entities;
using SocialMedia.Infrastrucuture.Data.Configuration.Abstract;

namespace SocialMedia.Infrastrucuture.Data.Configuration
{
    public class CommentConfiguration : ICommentConfiguration
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comentario");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("IdComentario")
                .ValueGeneratedNever();

            builder.Property(e => e.UserId)
                .HasColumnName("IdUsuario");

            builder.Property(e => e.PostId)
                .HasColumnName("PostId");

            builder.Property(e => e.Active)
                .HasColumnName("Activo");

            builder.Property(e => e.Description)
                .IsRequired()
                .HasColumnName("Descripcion")
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.Date)
                .HasColumnName("Fecha")
                .HasColumnType("datetime");

            builder.HasOne(d => d.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comentario_Publicacion");

            builder.HasOne(d => d.User)
                .WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comentario_Usuario");
        }
    }
}
