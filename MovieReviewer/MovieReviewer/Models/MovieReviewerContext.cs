using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MovieReviewer.Models
{
    public partial class MovieReviewerContext : DbContext
    {
        public MovieReviewerContext()
        {
        }

        public MovieReviewerContext(DbContextOptions<MovieReviewerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actor> Actors { get; set; } = null!;
        public virtual DbSet<FavouriteList> FavouriteLists { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<Movie> Movies { get; set; } = null!;
        public virtual DbSet<MovieImg> MovieImgs { get; set; } = null!;
        public virtual DbSet<Rate> Rates { get; set; } = null!;
        public virtual DbSet<RateLike> RateLikes { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<WatchList> WatchLists { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                optionsBuilder.UseSqlServer(config.GetConnectionString("MyConStr"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>(entity =>
            {
                entity.ToTable("Actor");

                entity.Property(e => e.Detail)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Dob).HasColumnType("date");

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.Nationality)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FavouriteList>(entity =>
            {
                entity.ToTable("FavouriteList");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.FavouriteLists)
                    .HasForeignKey(d => d.MovieId)
                    .HasConstraintName("FK__Favourite__Movie__403A8C7D");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FavouriteLists)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Favourite__UserI__412EB0B6");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("Genre");

                entity.Property(e => e.GenreName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("Movie");

                entity.Property(e => e.DateCreate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MovieName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.OriginalLanguage)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Poster)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Trailer)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("trailer");

                entity.HasMany(d => d.Actors)
                    .WithMany(p => p.Movies)
                    .UsingEntity<Dictionary<string, object>>(
                        "ActorMovie",
                        l => l.HasOne<Actor>().WithMany().HasForeignKey("ActorId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__ActorMovi__Actor__32E0915F"),
                        r => r.HasOne<Movie>().WithMany().HasForeignKey("MovieId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__ActorMovi__Movie__31EC6D26"),
                        j =>
                        {
                            j.HasKey("MovieId", "ActorId").HasName("PK__ActorMov__EEA9AABE38AF3836");

                            j.ToTable("ActorMovie");
                        });

                entity.HasMany(d => d.Genres)
                    .WithMany(p => p.Movies)
                    .UsingEntity<Dictionary<string, object>>(
                        "GenreMovie",
                        l => l.HasOne<Genre>().WithMany().HasForeignKey("GenreId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__GenreMovi__Genre__2D27B809"),
                        r => r.HasOne<Movie>().WithMany().HasForeignKey("MovieId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__GenreMovi__Movie__2C3393D0"),
                        j =>
                        {
                            j.HasKey("MovieId", "GenreId").HasName("PK__GenreMov__BBEAC44D4127E15D");

                            j.ToTable("GenreMovie");
                        });
            });

            modelBuilder.Entity<MovieImg>(entity =>
            {
                entity.HasKey(e => e.MovieId)
                    .HasName("PK__MovieImg__4BD2941A47BBDE68");

                entity.ToTable("MovieImg");

                entity.Property(e => e.MovieId).ValueGeneratedNever();

                entity.Property(e => e.Img)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Movie)
                    .WithOne(p => p.MovieImg)
                    .HasForeignKey<MovieImg>(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MovieImg__MovieI__35BCFE0A");
            });

            modelBuilder.Entity<Rate>(entity =>
            {
                entity.ToTable("Rate");

                entity.Property(e => e.RateContent).HasMaxLength(255);

                entity.Property(e => e.RateDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Rates)
                    .HasForeignKey(d => d.MovieId)
                    .HasConstraintName("FK__Rate__MovieId__398D8EEE");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Rates)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Rate__UserId__38996AB5");
            });

            modelBuilder.Entity<RateLike>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("RateLike");

                entity.HasOne(d => d.Rate)
                    .WithMany()
                    .HasForeignKey(d => d.RateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RateLike__RateId__3C69FB99");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RateLike__UserId__3D5E1FD2");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Img).IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Role).HasDefaultValueSql("((2))");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<WatchList>(entity =>
            {
                entity.ToTable("WatchList");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.WatchLists)
                    .HasForeignKey(d => d.MovieId)
                    .HasConstraintName("FK__WatchList__Movie__440B1D61");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.WatchLists)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__WatchList__UserI__44FF419A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
