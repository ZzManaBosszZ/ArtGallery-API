﻿namespace ArtGallery.Models.Offer
{
    public class OfferDetail
    {
        public int Id { get; set; }
        public int ArtWorkId { get; set; }
        public string OfferCode { get; set; }
        public int UserId { get; set; }
        public string? ArtWorkName { get; set; }
        public string? ArtWorkImage { get; set; }
        public string UserName { get; set; }
        public decimal OfferPrice { get; set; }
        public decimal ToTal { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int Status { get; set; }
        public List<OfferArtWorkResponse> OfferArtWork { get; set; }


    }
}