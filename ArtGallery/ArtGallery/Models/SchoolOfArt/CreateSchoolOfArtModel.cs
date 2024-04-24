namespace ArtGallery.Models.SchoolOfArt
{
    public class CreateSchoolOfArtModel
    {

        
        //public int ArtistId { get; set; }
        //public int ArtWorkId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
}
