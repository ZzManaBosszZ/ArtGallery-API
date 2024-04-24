using ArtGallery.DTOs;
using ArtGallery.Entities;
using ArtGallery.Models.Artist;
using Microsoft.EntityFrameworkCore;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace ArtGallery.Service.Artists
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

        public async Task<List<ArtistDTO>> GetAllArtistAsync()
        {
            List<Artist> artist = await _context.Artist.Where(m => m.DeletedAt == null).OrderByDescending(m => m.Id).ToListAsync();
            List<ArtistDTO> result = new List<ArtistDTO>();
            foreach (Artist m in artist)
            {
                result.Add(new ArtistDTO
                {
                    Id = m.Id,
                    Name = m.Name,
                    Image = m.Image,
                    createdAt = m.CreatedAt,
                    updatedAt = m.UpdatedAt,
                    deletedAt = m.DeletedAt,
                });
            }
            return result;
        }
    }
}
