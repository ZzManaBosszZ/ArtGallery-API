namespace ArtGallery.Entities
{
    public class ArtWork
    {
        public int Id { get; set; }
        public int ArtistId { get; set; }
        public int SchoolOfArtId { get; set; }
        public int? OfferId { get; set; }
        public string Name { get; set; } = null!;
        public string ArtWorkImage { get; set; } = null!;
        public string Medium { get; set; } = null!;
        public string Materials { get; set; } = null!;
        public string Size { get; set; } = null!;
        public string Condition { get; set; } = null!;
        public string Signature { get; set; } = null!;
        public string Rarity { get; set; } = null!;
        public string CertificateOfAuthenticity { get; set; } = null!;
        public string Frame { get; set; } = null!;
        public string Series { get; set; } = null!;
        public decimal Price { get; set; }
        public int FavoriteCount { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public virtual Artist Artist { get; set; }
        public virtual Offer Auction { get; set; }
        //public virtual ViewingRooms ViewingRooms { get; set; }
        public virtual ICollection<SchoolOfArt> SchoolOfArts { get; set; } = new List<SchoolOfArt>();
        public virtual ICollection<GalleryArtWork> GalleryArtWorks { get; set; } = new List<GalleryArtWork>();
    }
}
