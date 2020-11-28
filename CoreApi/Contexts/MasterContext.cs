using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CoreApi.Entities.Master;

namespace CoreApi.Contexts
{
    public partial class MasterContext : DbContext
    {
        public MasterContext()
        {
        }

        public MasterContext(DbContextOptions<MasterContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Entregas> Entregas { get; set; }
        public virtual DbSet<LoteEntregas> LoteEntregas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entregas>(entity =>
            {
                entity.HasKey(e => e.IdEntrega)
                    .HasName("PK__Entregas__C852F553790CAE66");

                entity.Property(e => e.DtEntrega).HasColumnType("date");

                entity.Property(e => e.NmProduto)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VlUnitario).HasColumnType("numeric(10, 2)");

                entity.HasOne(d => d.IdLoteEntregaNavigation)
                    .WithMany(p => p.Entregas)
                    .HasForeignKey(d => d.IdLoteEntrega)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Entregas_LoteEntregas");
            });

            modelBuilder.Entity<LoteEntregas>(entity =>
            {
                entity.HasKey(e => e.IdLoteEntrega)
                    .HasName("PK__LoteEntr__E9AA23016DFFFEC4");

                entity.Property(e => e.DtEntregaMenor).HasColumnType("date");

                entity.Property(e => e.DtImportacao).HasColumnType("datetime");

                entity.Property(e => e.NmArquivoLote)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VlTotalImportado).HasColumnType("numeric(10, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
