﻿namespace ArtGallery.DTOs
{
    public class FollowDTO : AbstractDTO<FollowDTO>
    {
        public int ArtistId { get; set; }
        public string ArtistName { get; set; } = null!;
        public string ArtistImage { get; set; } = null!;

        public int UserId { get; set; }
    }
}
