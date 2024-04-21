namespace ArtGallery.DTOs
{
    public class ArtistDTO : AbstractDTO<ArtistDTO>
    {
      
        public string Name { get; set; }
        public string Biography { get; set; }
        
        public string Image {  get; set; }
        public string ImagePath { get; set; }
    }
}