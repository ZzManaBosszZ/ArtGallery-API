﻿namespace ArtGallery.Models.Offer
{
    public class OfferResponse
    {
        public int Id { get; set; }
        public decimal OfferPrice { get; set; }
        public string UserName { get; set; }

        public decimal ToTal { get; set; }

        public int status {  get; set; }

        public int ispaid { get; set; }
        public string offercode { get; set; }
    }
}
