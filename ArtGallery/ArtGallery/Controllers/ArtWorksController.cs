using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArtGallery.Entities;
using Humanizer.Localisation;
using ArtGallery.DTOs;
using ArtGallery.Models.GeneralService;
using System.Drawing;
using ArtGallery.Models.ArtWork;
using ArtGallery.Service.IMG;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http.Headers;
using ArtGallery.Models.SchoolOfArt;
using ArtGallery.Service.Artists;
using PayPal.v1.CustomerDisputes;
using ArtGallery.Models.Artist;

namespace ArtGallery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtWorksController : ControllerBase
    {
        private readonly ArtGalleryApiContext _context;
        private readonly IImgService _imgService;
        private readonly IArtistService _artistService;
        public ArtWorksController(ArtGalleryApiContext context, IImgService imgService, IArtistService artistService)
        {
            _context = context;
            _imgService = imgService;
            _artistService = artistService;
        }

        // GET: api/ArtWorks
        [HttpGet]
        public async Task<IActionResult> GetAllArtWorks(
        [FromQuery] string search = null,
        [FromQuery] List<int> schoolOfArtsIds = null)
        {
            try
            {
                var query = _context.ArtWork
                    .Include(a => a.ArtWorkSchoolOfArts).ThenInclude(a => a.SchoolOfArt)
                    .Include(a => a.ArtistArtWorks).ThenInclude(a => a.Artist)
                    .Where(a => a.DeletedAt == null);

                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(a => a.Name.Contains(search));
                }

                // Áp dụng bộ lọc SchoolOfArtIds
                if (schoolOfArtsIds != null && schoolOfArtsIds.Any())
                {
                    query = query.Where(a => a.ArtWorkSchoolOfArts.Any(a1 => schoolOfArtsIds.Contains(a1.SchoolOfArtId)));
                }

                List<ArtWork> artworks = await query.OrderByDescending(m => m.Id).ToListAsync();
                List<ArtWorkDTO> result = new List<ArtWorkDTO>();
                foreach (ArtWork aw in artworks)
                {
                    var artworkDTO = new ArtWorkDTO
                    {
                        Id = aw.Id,
                        Name = aw.Name,
                        ArtWorkImage = aw.ArtWorkImage,
                        Medium = aw.Medium,
                        Materials = aw.Materials,
                        Size = aw.Size,
                        Condition = aw.Condition,
                        Signature = aw.Signature,
                        Rarity = aw.Rarity,
                        CertificateOfAuthenticity = aw.CertificateOfAuthenticity,
                        Frame = aw.Frame,
                        Series = aw.Series,
                        Price = aw.Price,
                        FavoriteCount = aw.FavoriteCount,
                        createdAt = aw.CreatedAt,
                        updatedAt = aw.UpdatedAt,
                        deletedAt = aw.DeletedAt,
                    };
                    var schoolOfArts = new List<SchoolOfArtResponse>();
                    var Artist = new List<ArtistResponse>();
                    foreach (var item in aw.ArtWorkSchoolOfArts)
                    {
                        var schoolOfArt = new SchoolOfArtResponse
                        {
                            Id = item.Id,
                            Name = item.SchoolOfArt.Name,
                        };
                        schoolOfArts.Add(schoolOfArt);

                    }
                    artworkDTO.SchoolOfArts = schoolOfArts;
                    result.Add(artworkDTO);
                  
                    foreach (var item in aw.ArtistArtWorks)
                    {
                        var Artistss = new ArtistResponse
                        {
                            Id = item.Id,
                            Name = item.Artist.Name,
                        };
                        Artist.Add(Artistss);

                    }
                    artworkDTO.Artists = Artist;
                    result.Add(artworkDTO);
                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                var response = new GeneralService
                {
                    Success = false,
                    StatusCode = 400,
                    Message = ex.Message,
                    Data = ""
                };

                return BadRequest(response);
            }

        }

        // GET: api/ArtWorks/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetArtWorkById(int id)
        {
            try
            {
                var artWork = await _context.ArtWork
         .Include(a => a.ArtistArtWorks)
         .ThenInclude(a => a.Artist)
         .Include(a => a.ArtWorkSchoolOfArts)
         .ThenInclude(a => a.SchoolOfArt)
         .FirstOrDefaultAsync(x => x.Id == id && x.DeletedAt == null);

                if (artWork != null)
                {
                    var artWorkDto = new ArtWorkDTO
                    {
                        Id = artWork.Id,
                        Name = artWork.Name,
                        
                        ArtWorkImage = artWork.ArtWorkImage,
                        Medium = artWork.Medium,
                        Materials = artWork.Materials,
                        Size = artWork.Size,
                        Condition = artWork.Condition,
                        Signature = artWork.Signature,
                        Rarity = artWork.Rarity,
                        CertificateOfAuthenticity = artWork.CertificateOfAuthenticity,
                        Frame = artWork.Frame,
                        Series = artWork.Series,
                        Price = artWork.Price,
                        FavoriteCount = artWork.FavoriteCount,
                        createdAt = artWork.CreatedAt,

                    };

                    var schoolOfArts = new List<SchoolOfArtResponse>();
                    var artist = new List<ArtistResponse>();
                    foreach (var item in artWork.ArtWorkSchoolOfArts)
                    {
                        var schoolOfArtResponse = new SchoolOfArtResponse
                        {
                            Id = item.SchoolOfArt.Id,
                            Name = item.SchoolOfArt.Name,
                        };
                        schoolOfArts.Add(schoolOfArtResponse);
                    }
                    artWorkDto.SchoolOfArts = schoolOfArts;

                    foreach (var item in artWork.ArtistArtWorks)
                    {
                        var artistResponse = new ArtistResponse
                        {
                            Id = item.Artist.Id,
                            Name = item.Artist.Name,

                        };
                        artist.Add(artistResponse);
                    }
                    artWorkDto.Artists = artist;

                    return Ok(artWorkDto);
                }
                else
                {
                    var response = new GeneralService
                    {
                        Success = false,
                        StatusCode = 404,
                        Message = "Not Found",
                        Data = ""
                    };

                    return NotFound(response);
                };
            }
            catch (Exception ex)
            {
                var response = new GeneralService
                {
                    Success = false,
                    StatusCode = 400,
                    Message = ex.Message,
                    Data = ""
                };
                return BadRequest(response);
            }
        }
        // PUT: api/ArtWorks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPut("edit")]
        public async Task<IActionResult> EditArtWork([FromForm] EditArtWorkModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ArtWork existingtArtWork = await _context.ArtWork.AsNoTracking().FirstOrDefaultAsync(e => e.Id == model.Id);
                    if (existingtArtWork == null)
                    {
                        return NotFound(new GeneralService
                        {
                            Success = false,
                            StatusCode = 404,
                            Message = "Not Found",
                            Data = ""
                        });
                    }

                    ArtWork art = new ArtWork
                    {
                        Id = model.Id,
                        Name = model.Name,
                        Medium = model.Medium,
                        Materials = model.Materials,
                        Size = model.Size,
                        Condition = model.Condition,
                        Signature = model.Signature,
                        Rarity = model.Rarity,
                        CertificateOfAuthenticity = model.CertificateOfAuthenticity,
                        Frame = model.Frame,
                        Series = model.Series,
                        Price = model.Price,
                        CreatedAt = existingtArtWork.CreatedAt,
                        UpdatedAt = DateTime.Now,
                        DeletedAt = null
                    };

                    if (model.ArtWorkImage != null)
                    {
                        string imageUrl = await _imgService.UploadImageAsync(model.ArtWorkImage, "artwork");

                        if (imageUrl == null)
                        {
                            return BadRequest(new GeneralService
                            {
                                Success = false,
                                StatusCode = 400,
                                Message = "Failed to upload avatar.",
                                Data = ""
                            });
                        }

                        art.ArtWorkImage = imageUrl;
                    }
                    else
                    {
                        art.ArtWorkImage = existingtArtWork.ArtWorkImage;
                    }

                    _context.ArtWork.Update(art);
                    await _context.SaveChangesAsync();

                    return Ok(new GeneralService
                    {
                        Success = true,
                        StatusCode = 200,
                        Message = "Edit successfully",
                        Data = ""
                    });

                }
                catch (Exception ex)
                {
                    var response = new GeneralService
                    {
                        Success = false,
                        StatusCode = 400,
                        Message = ex.Message,
                        Data = ""
                    };

                    return BadRequest(response);
                }
            }
            var validationErrors = ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage);

            var validationResponse = new GeneralService
            {
                Success = false,
                StatusCode = 400,
                Message = "Validation errors",
                Data = string.Join(" | ", validationErrors)
            };

            return BadRequest(validationResponse);
        }

        // POST: api/ArtWorks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create")]
        //[Authorize(Roles = "Super Admin, Shopping Center Manager Staff")]
        public async Task<IActionResult> CreateArtWork([FromForm] CreateArtWorkModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var imageUrl = await _imgService.UploadImageAsync(model.ArtWorkImage, "artwork");

                    if (imageUrl == null)
                    {
                        return BadRequest(new GeneralService
                        {
                            Success = false,
                            StatusCode = 400,
                            Message = "Please provide a image.",
                            Data = ""
                        });
                    }
                    ArtWork artWork = new ArtWork
                    {
                        Name = model.Name,
                        ArtWorkImage = imageUrl,
                        Medium = model.Medium,
                        Materials = model.Materials,
                        Size = model.Size,
                        Condition = model.Condition,
                        Signature = model.Signature,
                        Rarity = model.Rarity,
                        CertificateOfAuthenticity = model.CertificateOfAuthenticity,
                        Frame = model.Frame,
                        Series = model.Series,
                        Price = model.Price,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        DeletedAt = null
                    };
                    _context.ArtWork.Add(artWork);
                    await _context.SaveChangesAsync();

                    foreach (var schoolOfArtId in model.SchoolOfArtIds)
                    {
                        var ArtworkSchoolOfArt = new ArtWorkSchoolOfArt
                        {
                            ArtWorkId = artWork.Id,
                            SchoolOfArtId = schoolOfArtId,
                        };

                        _context.ArtWorkSchoolOfArt.Add(ArtworkSchoolOfArt);

                    }
                    foreach (var artistId in model.ArtistId)
                    {
                        var ArtistArtWorks = new ArtistArtWork
                        {
                            ArtWorkId = artWork.Id,
                            ArtistId = artistId,
                        };

                        _context.ArtistArtWork.Add(ArtistArtWorks);

                    }

                    await _context.SaveChangesAsync();
                    return Created($"get-by-id?id={artWork.Id}", new ArtWorkDTO
                    {
                        Id = artWork.Id,
                        Name = artWork.Name,
                        ArtWorkImage = imageUrl,
                        Medium = artWork.Medium,
                        Materials = artWork.Materials,
                        Size = artWork.Size,
                        Condition = artWork.Condition,
                        Signature = artWork.Signature,
                        Rarity = artWork.Rarity,
                        CertificateOfAuthenticity = artWork.CertificateOfAuthenticity,
                        Frame = artWork.Frame,
                        Series = artWork.Series,
                        Price = artWork.Price,
                        createdAt = artWork.CreatedAt,
                        updatedAt = artWork.UpdatedAt,
                        deletedAt = artWork.DeletedAt,
                    });
                }
                catch (Exception ex)
                {
                    var response = new GeneralService
                    {
                        Success = false,
                        StatusCode = 400,
                        Message = ex.Message,
                        Data = ""
                    };

                    return BadRequest(response);
                }
            }
            var validationErrors = ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage);

            var validationResponse = new GeneralService
            {
                Success = false,
                StatusCode = 400,
                Message = "Validation errors",
                Data = string.Join(" | ", validationErrors)
            };

            return BadRequest(validationResponse);
        }
        // DELETE: api/ArtWorks/5
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteArtist(List<int> ids)
        {
            try
            {
                foreach (var id in ids)
                {
                    ArtWork artist = await _context.ArtWork.FindAsync(id);

                    if (artist != null)
                    {
                        artist.DeletedAt = DateTime.Now;
                    }
                }

                await _context.SaveChangesAsync();

                var response = new GeneralService
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "Soft delete successful",
                    Data = null
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new GeneralService
                {
                    Success = false,
                    StatusCode = 400,
                    Message = ex.Message,
                    Data = ""
                };

                return BadRequest(response);
            }
        }


    }
}
