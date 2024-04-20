namespace ArtGallery.Entities
{
    public partial class ArtWorkMovement
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!; 
        public string Slug { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ArtWork ArtWork { get; set; }
        public virtual Artist Artist { get; set; }
    }
}
