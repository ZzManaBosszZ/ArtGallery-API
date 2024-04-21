namespace ArtGallery.Entities
{
    public partial class ArtWorkMovement
    {
        public int Id { get; set; }
        public int ArtistId { get; set; }
        public int ArtWorkId { get; set; }
        public string Name { get; set; } = null!; 
        public string Slug { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ArtWork ArtWork { get; set; } = null!;
        public virtual Artist Artist { get; set; } = null!;
    }
}
