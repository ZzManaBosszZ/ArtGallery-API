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
        public async Task<List<ArtistDTO>> GetAllArtistAsync()
        {
            List<Entities.Artist> artist = await _context.Artist.Where(m => m.DeletedAt == null).OrderByDescending(m => m.Id).ToListAsync();
            List<ArtistDTO> result = new List<ArtistDTO>();
            foreach (Entities.Artist a in artist)
            {
                result.Add(new ArtistDTO
                {
                    Id = a.Id,
                   
                    createdAt = a.CreatedAt,
                    updatedAt = a.UpdatedAt,
                    deletedAt = a.DeletedAt,        
                });
            }
            return result;
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
    }
}
