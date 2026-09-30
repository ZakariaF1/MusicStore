using System.ComponentModel.DataAnnotations;
using System.Linq;
using MusicStore.Domain;
using Xunit;

namespace MusicStore.Domain.Tests
{
    /// <summary>
    /// Characterization tests for current Domain behavior before structural refactors.
    /// </summary>
    public class AlbumTests
    {
        [Fact]
        public void Album_Title_Requires_MinimumLength_3()
        {
            var album = new Album { Title = "AB", ArtistId = 1 };
            var results = new System.Collections.Generic.List<ValidationResult>();
            var context = new ValidationContext(album);

            var isValid = Validator.TryValidateObject(album, context, results, validateAllProperties: true);

            Assert.False(isValid);
            Assert.Contains(results, r => r.MemberNames.Contains(nameof(Album.Title)));
        }

        [Fact]
        public void Album_With_Valid_Title_And_ArtistId_Passes_Validation()
        {
            var album = new Album { Title = "ABC", ArtistId = 1 };
            var results = new System.Collections.Generic.List<ValidationResult>();
            var context = new ValidationContext(album);

            var isValid = Validator.TryValidateObject(album, context, results, validateAllProperties: true);

            Assert.True(isValid);
            Assert.Empty(results);
        }
    }
}
