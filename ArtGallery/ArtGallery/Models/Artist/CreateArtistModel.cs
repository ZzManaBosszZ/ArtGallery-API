using System.ComponentModel.DataAnnotations;

namespace ArtGallery.Models.Artist
{
    public class CreateArtistModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public IFormFile Image { get; set; }

        [Required(ErrorMessage = "Biography is required")]
        public string Biography { get; set; }

        [Required(ErrorMessage = "Image is required")]
        public IFormFile ImagePath { get; set; }
    }
}
