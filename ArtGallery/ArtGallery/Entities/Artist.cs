namespace ArtGallery.Entities
{
    public partial class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Image {  get; set; }  
        public int ArtWorkId { get; set; }
        public int SchoolOfArtId { get; set; }
        public string Biography { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        //public virtual ViewingRooms? ViewingRooms { get; set; } = null;
        public virtual ICollection<ArtWork> ArtWorks { get; set; } = new List<ArtWork>();
        public virtual ICollection<SchoolOfArt> SchoolOfArts { get; set; } = new List<SchoolOfArt>();
    }
}
