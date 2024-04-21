namespace ArtGallery.Models.Artist
{
    public class EditArtistModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
        public IFormFile Image { get; set; }
    }
}