using ArtGallery.DTOs;
using ArtGallery.Entities;
using ArtGallery.Models.Artist;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Service.Artist
{
    public class ArtistService : IArtistService
    {
        private readonly ArtGalleryApiContext _context;
        public ArtistService(ArtGalleryApiContext context)
        {
            _context = context;
        }
       

        public Task<ArtistDTO> GetArtistByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ArtistDTO> CreateArtistAsync(CreateArtistModel model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateArtistAsync(EditArtistModel model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteArtistAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ArtistDTO>> GetAllArtistAsync()
        {
            throw new NotImplementedException();
        }
    }
}
