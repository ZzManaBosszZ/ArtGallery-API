﻿namespace ArtGallery.Models.SchoolOfArt
{
    public class SchoolOfArtResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ArtWorkId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }

}