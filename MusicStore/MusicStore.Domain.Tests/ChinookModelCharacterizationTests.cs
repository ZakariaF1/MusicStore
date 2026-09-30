using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MusicStore.Domain;
using MusicStore.Repository.MySql;
using Xunit;

namespace MusicStore.Domain.Tests
{
    /// <summary>
    /// Locks current EF Core mapping behavior before removing Schema attributes from Domain.
    /// </summary>
    public class ChinookModelCharacterizationTests
    {
        private static IModel CreateModel()
        {
            var options = new DbContextOptionsBuilder<UnitOfWork>()
                .UseMySql(
                    "Server=127.0.0.1;Database=chinookdatabase_unused;User=root;",
                    new MySqlServerVersion(new Version(8, 0, 0)))
                .Options;

            using var context = new UnitOfWork(options);
            return context.Model;
        }

        [Theory]
        [InlineData(typeof(Album), nameof(Album.AlbumId))]
        [InlineData(typeof(Artist), nameof(Artist.ArtistId))]
        [InlineData(typeof(Customer), nameof(Customer.CustomerId))]
        [InlineData(typeof(Employee), nameof(Employee.EmployeeId))]
        [InlineData(typeof(Genre), nameof(Genre.GenreId))]
        [InlineData(typeof(Invoice), nameof(Invoice.InvoiceId))]
        [InlineData(typeof(InvoiceItem), nameof(InvoiceItem.InvoiceLineId))]
        [InlineData(typeof(MediaType), nameof(MediaType.MediaTypeId))]
        [InlineData(typeof(Playlist), nameof(Playlist.PlaylistId))]
        [InlineData(typeof(Track), nameof(Track.TrackId))]
        public void Single_key_identity_entities_map_expected_primary_key(Type entityType, string keyProperty)
        {
            var entity = CreateModel().FindEntityType(entityType);
            Assert.NotNull(entity);

            var primaryKey = entity.FindPrimaryKey();
            Assert.NotNull(primaryKey);
            Assert.Equal(keyProperty, primaryKey.Properties.Single().Name);
            Assert.Equal(ValueGenerated.OnAdd, primaryKey.Properties.Single().ValueGenerated);
        }

        [Fact]
        public void PlaylistTrack_maps_composite_primary_key()
        {
            var entity = CreateModel().FindEntityType(typeof(PlaylistTrack));
            Assert.NotNull(entity);

            var primaryKey = entity.FindPrimaryKey();
            Assert.NotNull(primaryKey);
            Assert.Equal(
                new[] { nameof(PlaylistTrack.PlaylistId), nameof(PlaylistTrack.TrackId) },
                primaryKey.Properties.Select(p => p.Name).ToArray());
        }

        [Theory]
        [InlineData(typeof(Album), "albums")]
        [InlineData(typeof(Artist), "artists")]
        [InlineData(typeof(Customer), "customers")]
        [InlineData(typeof(Employee), "employees")]
        [InlineData(typeof(Genre), "genres")]
        [InlineData(typeof(Invoice), "invoices")]
        [InlineData(typeof(InvoiceItem), "invoice_items")]
        [InlineData(typeof(MediaType), "media_types")]
        [InlineData(typeof(Playlist), "playlists")]
        [InlineData(typeof(PlaylistTrack), "playlist_track")]
        [InlineData(typeof(Track), "tracks")]
        public void Entities_map_to_chinook_table_names(Type entityType, string tableName)
        {
            var entity = CreateModel().FindEntityType(entityType);
            Assert.NotNull(entity);
            Assert.Equal(tableName, entity.GetTableName());
        }
    }
}
