using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PostgresCRUD.Models;

namespace PostgresCRUD.DataAccess
{
    public partial class postgresContext : DbContext
    {
        /*public postgresContext()
        {
        }*/

        public postgresContext(DbContextOptions<postgresContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appuser> Appusers { get; set; } = null!;
        public virtual DbSet<Birim> Birims { get; set; } = null!;
        public virtual DbSet<Bolum> Bolums { get; set; } = null!;
        public virtual DbSet<Egitim> Egitims { get; set; } = null!;
        public virtual DbSet<Gorev> Gorevs { get; set; } = null!;
        public virtual DbSet<Kodlar> Kodlars { get; set; } = null!;
        public virtual DbSet<Okul> Okuls { get; set; } = null!;
        public virtual DbSet<Personels> Personels { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("pg_catalog", "adminpack");

            modelBuilder.Entity<Appuser>(entity =>
            {
                entity.ToTable("appuser");

                entity.Property(e => e.Id)
                    .HasColumnName("id");
                entity.Property(e => e.Passwordhash)
                    .HasColumnName("passwordhash");

                entity.Property(e => e.Passwordsalt)
                    .HasColumnName("passwordsalt");

                entity.Property(e => e.Username)
                    .HasMaxLength(150)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Birim>(entity =>
            {
                entity.ToTable("birim");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Ad)
                    .HasMaxLength(250)
                    .HasColumnName("ad");

                entity.Property(e => e.Zindex).HasColumnName("zindex");
            });

            modelBuilder.Entity<Bolum>(entity =>
            {
                entity.ToTable("bolum");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Ad)
                    .HasMaxLength(250)
                    .HasColumnName("ad");
            });

            modelBuilder.Entity<Egitim>(entity =>
            {
                entity.ToTable("egitim");

                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();

                entity.Property(e => e.BolumId).HasColumnName("bolum_id");

                entity.Property(e => e.DiplomaNo)
                    .HasMaxLength(50)
                    .HasColumnName("diploma_no");

                entity.Property(e => e.Mezuniyet).HasColumnName("mezuniyet");

                entity.Property(e => e.OkulId).HasColumnName("okul_id");

                entity.Property(e => e.PersonelId).HasColumnName("personel_id");

                entity.Property(e => e.Tur)
                    .HasMaxLength(150)
                    .HasColumnName("tur");

                entity.HasOne(d => d.Bolum)
                    .WithMany(p => p.Egitims)
                    .HasForeignKey(d => d.BolumId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_egt_bolum");

                entity.HasOne(d => d.Okul)
                    .WithMany(p => p.Egitims)
                    .HasForeignKey(d => d.OkulId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_egt_okul");

                entity.HasOne(d => d.Personel)
                    .WithMany(p => p.Egitims)
                    .HasForeignKey(d => d.PersonelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_egt_pers");

                entity.HasOne(d => d.TurNavigation)
                    .WithMany(p => p.Egitims)
                    .HasForeignKey(d => d.Tur)
                    .HasConstraintName("fk_egt_tur");
            });

            modelBuilder.Entity<Gorev>(entity =>
            {
                entity.ToTable("gorev");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Ad)
                    .HasMaxLength(250)
                    .HasColumnName("ad");

                entity.Property(e => e.Zindex).HasColumnName("zindex");
            });

            modelBuilder.Entity<Kodlar>(entity =>
            {
                entity.ToTable("kodlar");

                entity.HasIndex(e => new { e.Tablo, e.Kod }, "kodlar_tablo_kod_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(150)
                    .HasColumnName("id");

                entity.Property(e => e.Isaktif).HasColumnName("isaktif");

                entity.Property(e => e.Kod)
                    .HasMaxLength(100)
                    .HasColumnName("kod");

                entity.Property(e => e.KodAck)
                    .HasMaxLength(250)
                    .HasColumnName("kod_ack");

                entity.Property(e => e.Tablo)
                    .HasMaxLength(50)
                    .HasColumnName("tablo");
            });

            modelBuilder.Entity<Okul>(entity =>
            {
                entity.ToTable("okul");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Ad)
                    .HasMaxLength(250)
                    .HasColumnName("ad");
            });

            modelBuilder.Entity<Personels>(entity =>
            {
                entity.ToTable("personel");

                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();

                entity.Property(e => e.Ad)
                    .HasMaxLength(250)
                    .HasColumnName("ad");

                entity.Property(e => e.AnaAd)
                    .HasMaxLength(250)
                    .HasColumnName("ana_ad");

                entity.Property(e => e.BabaAd)
                    .HasMaxLength(250)
                    .HasColumnName("baba_ad");

                entity.Property(e => e.BirimId).HasColumnName("birim_id");

                entity.Property(e => e.Foto)
                    .HasMaxLength(250)
                    .HasColumnName("foto");

                entity.Property(e => e.GorevId).HasColumnName("gorev_id");

                entity.Property(e => e.KanGrup)
                    .HasMaxLength(150)
                    .HasColumnName("kan_grup");

                entity.Property(e => e.MedeniDurum)
                    .HasMaxLength(150)
                    .HasColumnName("medeni_durum");

                entity.Property(e => e.Soyad)
                    .HasMaxLength(250)
                    .HasColumnName("soyad");

                entity.HasOne(d => d.Birim)
                    .WithMany(p => p.Personels)
                    .HasForeignKey(d => d.BirimId)
                    .HasConstraintName("fk_pers_birim");

                entity.HasOne(d => d.Gorev)
                    .WithMany(p => p.Personels)
                    .HasForeignKey(d => d.GorevId)
                    .HasConstraintName("fk_pers_gorev");

                entity.HasOne(d => d.KanGrupNavigation)
                    .WithMany(p => p.PersonelKanGrupNavigations)
                    .HasForeignKey(d => d.KanGrup)
                    .HasConstraintName("fk_pers_kodlar_kangrup");

                entity.HasOne(d => d.MedeniDurumNavigation)
                    .WithMany(p => p.PersonelMedeniDurumNavigations)
                    .HasForeignKey(d => d.MedeniDurum)
                    .HasConstraintName("fk_pers_kodlar_mdn");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
