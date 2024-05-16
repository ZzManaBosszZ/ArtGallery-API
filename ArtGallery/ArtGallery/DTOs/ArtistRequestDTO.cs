namespace ArtGallery.DTOs
{
    public class ArtistRequestDTO : AbstractDTO<ArtistRequestDTO>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Biography { get; set; }
    }
}
