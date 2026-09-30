using Microsoft.EntityFrameworkCore;
using MusicStore.Domain;

namespace MusicStore.Repository.MySql
{
    public class UnitOfWork : DbContext
    {
        public UnitOfWork(DbContextOptions<UnitOfWork> options) : base(options)
        {

        }
        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<InvoiceItem> InvoiceItems { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<MediaType> MediaTypes { get; set; }
        public virtual DbSet<Playlist> Playlists { get; set; }
        public virtual DbSet<PlaylistTrack> PlaylistTrack { get; set; }
        public virtual DbSet<Track> Tracks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>(entity =>
            {
                entity.HasKey(e => e.AlbumId);

                entity.ToTable("albums");

                entity.HasIndex(e => e.ArtistId)
                    .HasDatabaseName("IFK_AlbumArtistId");

                entity.Property(e => e.AlbumId)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ArtistId).HasColumnType("int(11)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("varchar(160)");
            });

            modelBuilder.Entity<Artist>(entity =>
            {
                entity.HasKey(e => e.ArtistId);

                entity.ToTable("artists");

                entity.Property(e => e.ArtistId)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Name).HasColumnType("varchar(120)");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.ToTable("customers");

                entity.HasIndex(e => e.SupportRepId)
                    .HasDatabaseName("IFK_CustomerSupportRepId");

                entity.Property(e => e.CustomerId)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Address).HasColumnType("varchar(70)");

                entity.Property(e => e.City).HasColumnType("varchar(40)");

                entity.Property(e => e.Company).HasColumnType("varchar(80)");

                entity.Property(e => e.Country).HasColumnType("varchar(40)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(60)");

                entity.Property(e => e.Fax).HasColumnType("varchar(24)");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnType("varchar(40)");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Phone).HasColumnType("varchar(24)");

                entity.Property(e => e.PostalCode).HasColumnType("varchar(10)");

                entity.Property(e => e.State).HasColumnType("varchar(40)");

                entity.Property(e => e.SupportRepId).HasColumnType("int(11)");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.ToTable("employees");

                entity.HasIndex(e => e.ReportsTo)
                    .HasDatabaseName("IFK_EmployeeReportsTo");

                entity.Property(e => e.EmployeeId)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Address).HasColumnType("varchar(70)");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.City).HasColumnType("varchar(40)");

                entity.Property(e => e.Country).HasColumnType("varchar(40)");

                entity.Property(e => e.Email).HasColumnType("varchar(60)");

                entity.Property(e => e.Fax).HasColumnType("varchar(24)");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.HireDate).HasColumnType("date");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Phone).HasColumnType("varchar(24)");

                entity.Property(e => e.PostalCode).HasColumnType("varchar(10)");

                entity.Property(e => e.ReportsTo).HasColumnType("int(11)");

                entity.Property(e => e.State).HasColumnType("varchar(40)");

                entity.Property(e => e.Title).HasColumnType("varchar(30)");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasKey(e => e.GenreId);

                entity.ToTable("genres");

                entity.Property(e => e.GenreId)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Name).HasColumnType("varchar(120)");
            });

            modelBuilder.Entity<InvoiceItem>(entity =>
            {
                entity.HasKey(e => e.InvoiceLineId);

                entity.ToTable("invoice_items");

                entity.HasIndex(e => e.InvoiceId)
                    .HasDatabaseName("IFK_InvoiceLineInvoiceId");

                entity.HasIndex(e => e.TrackId)
                    .HasDatabaseName("IFK_InvoiceLineTrackId");

                entity.Property(e => e.InvoiceLineId)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.InvoiceId).HasColumnType("int(11)");

                entity.Property(e => e.Quantity).HasColumnType("int(11)");

                entity.Property(e => e.TrackId).HasColumnType("int(11)");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(10,2)");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(e => e.InvoiceId);

                entity.ToTable("invoices");

                entity.HasIndex(e => e.CustomerId)
                    .HasDatabaseName("IFK_InvoiceCustomerId");

                entity.Property(e => e.InvoiceId)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.BillingAddress).HasColumnType("varchar(70)");

                entity.Property(e => e.BillingCity).HasColumnType("varchar(40)");

                entity.Property(e => e.BillingCountry).HasColumnType("varchar(40)");

                entity.Property(e => e.BillingPostalCode).HasColumnType("varchar(10)");

                entity.Property(e => e.BillingState).HasColumnType("varchar(40)");

                entity.Property(e => e.CustomerId).HasColumnType("int(11)");

                entity.Property(e => e.InvoiceDate).HasColumnType("date");

                entity.Property(e => e.Total).HasColumnType("decimal(10,2)");
            });

            modelBuilder.Entity<MediaType>(entity =>
            {
                entity.HasKey(e => e.MediaTypeId);

                entity.ToTable("media_types");

                entity.Property(e => e.MediaTypeId)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Name).HasColumnType("varchar(120)");
            });

            modelBuilder.Entity<Playlist>(entity =>
            {
                entity.HasKey(e => e.PlaylistId);

                entity.ToTable("playlists");

                entity.Property(e => e.PlaylistId)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Name).HasColumnType("varchar(120)");
            });

            modelBuilder.Entity<PlaylistTrack>(entity =>
            {
                entity.HasKey(e => new { e.PlaylistId, e.TrackId });

                entity.ToTable("playlist_track");

                entity.HasIndex(e => e.TrackId)
                    .HasDatabaseName("IFK_PlaylistTrackTrackId");

                entity.Property(e => e.PlaylistId).HasColumnType("int(11)");

                entity.Property(e => e.TrackId).HasColumnType("int(11)");
            });

            modelBuilder.Entity<Track>(entity =>
            {
                entity.HasKey(e => e.TrackId);

                entity.ToTable("tracks");

                entity.HasIndex(e => e.AlbumId)
                    .HasDatabaseName("IFK_TrackAlbumId");

                entity.HasIndex(e => e.GenreId)
                    .HasDatabaseName("IFK_TrackGenreId");

                entity.HasIndex(e => e.MediaTypeId)
                    .HasDatabaseName("IFK_TrackMediaTypeId");

                entity.Property(e => e.TrackId)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.AlbumId).HasColumnType("int(11)");

                entity.Property(e => e.Bytes).HasColumnType("int(11)");

                entity.Property(e => e.Composer).HasColumnType("varchar(220)");

                entity.Property(e => e.GenreId).HasColumnType("int(11)");

                entity.Property(e => e.MediaTypeId).HasColumnType("int(11)");

                entity.Property(e => e.Milliseconds).HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(10,2)");
            });
        }
    }
}
