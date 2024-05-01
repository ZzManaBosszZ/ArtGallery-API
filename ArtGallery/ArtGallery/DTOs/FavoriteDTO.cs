namespace ArtGallery.DTOs
{
    internal class FavoriteDTO : AbstractDTO<FavoriteDTO>
    {
        public int ArtWorkId { get; set; }
        public string ArtWorkName { get; set; } = null!;
        public string ArtWorkImage { get; set; } = null!;

        public int UserId { get; set; }
    }
}