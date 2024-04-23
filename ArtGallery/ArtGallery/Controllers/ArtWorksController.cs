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

namespace ArtGallery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtWorksController : ControllerBase
    {
        private readonly ArtGalleryApiContext _context;
        private readonly IImgService _imgService;

        public ArtWorksController(ArtGalleryApiContext context ,IImgService imgService)
        {
            _context = context;
            _imgService = imgService;
        }

        // GET: api/ArtWorks
        [HttpGet]
        public async Task<IActionResult> GetAllArtWorks()
        {
            try
            {
                List<ArtWork> genres = await _context.ArtWork.Where(m => m.DeletedAt == null).OrderBy(m => m.Id).ToListAsync();
                List<ArtWorkDTO> result = new List<ArtWorkDTO>();
                foreach (ArtWork m in genres)
                {
                    result.Add(new ArtWorkDTO
                    {
                        Id = m.Id,
                        Name = m.Name,
                        ArtWorkImage = m.ArtWorkImage,
                        Medium = m.Medium,
                        Materials = m.Materials,
                        Size = m.Size,
                        Condition = m.Condition,
                        Signature = m.Signature,
                        Rarity = m.Rarity,
                        CertificateOfAuthenticity = m.CertificateOfAuthenticity,
                        Frame = m.Frame,
                        Series = m.Series,
                        Price = m.Price,
                        FavoriteCount = m.FavoriteCount,
                        createdAt = m.CreatedAt,
                        updatedAt = m.UpdatedAt,
                        deletedAt = m.DeletedAt,
                    });
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
                ArtWork artWork = await _context.ArtWork.FirstOrDefaultAsync(a => a.Id == id && a.DeletedAt == null);
                if (artWork != null)
                {
                  var ArtWorkDTO = new ArtWorkDTO
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
                        updatedAt = artWork.UpdatedAt,
                        deletedAt = artWork.DeletedAt,
                    };
                    ArtWorkDTO.FavoriteCount = await _context.Favorite.Where(f => f.ArtWorkId == artWork.Id).CountAsync();
                    return Ok(artWork);
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
                        FavoriteCount = model.FavoriteCount,
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
        [Authorize(Roles = "Super Admin, Shopping Center Manager Staff")]
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
