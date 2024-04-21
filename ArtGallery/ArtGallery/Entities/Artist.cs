namespace ArtGallery.Entities
{
    public partial class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Image {  get; set; }  
        public int ArtWorkId { get; set; }
        public int ArtWorkMovementId { get; set; }
        public string Biography { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        //public virtual ViewingRooms? ViewingRooms { get; set; } = null;
        public virtual ICollection<ArtWork> ArtWorks { get; set; } = new List<ArtWork>();
        public virtual ICollection<ArtWorkMovement> ArtWorkMovements { get; set; } = new List<ArtWorkMovement>();
    }
}
