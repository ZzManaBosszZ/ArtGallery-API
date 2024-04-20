namespace ArtGallery.Entities
{
    public partial class Auction
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ArtWorkId { get; set;}

        public decimal StartingPrice { get; set; }

        public int Status { get; set; }

        public virtual User User { get; set; } = null!;

        public virtual ICollection<ArtWork> ArtWorks { get; set; } = new List<ArtWork>();
    }
}
