using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArtGallery.Entities;
using Microsoft.AspNetCore.Authorization;
using ArtGallery.Models.Artist;
using ArtGallery.Models.GeneralService;
using ArtGallery.Service.IMG;
using ArtGallery.DTOs;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ArtGallery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly ArtGalleryApiContext _context;
        private readonly IImgService _imgService;

      public ArtistsController(ArtGalleryApiContext context, IImgService imgService)
        {
            _context = context;
            _imgService = imgService;
            
        }

        // GET: api/Artists
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllArtists()
        {
            try
            {
                List<Artist> artists = await _context.Artist.Where(a => a.DeletedAt == null).OrderByDescending(a => a.Id).ToListAsync();
                List<ArtistDTO> result = new List<ArtistDTO>();

                foreach (var item in artists)
                {
                    result.Add(new ArtistDTO
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Biography = item.Biography,
                        createdAt = item.CreatedAt,
                        updatedAt = item.UpdatedAt,
                        deletedAt = item.DeletedAt
                    });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                var response = new GeneralService
                {
                    Success = false,
                    StatusCode = 500,
                    Message = ex.Message,
                    Data = ""
                };

                return StatusCode(500, response);
            }
        }


        // GET: api/Artists/5
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetArtistById(int id)
        {
            try
            {
                var artist = await _context.Artist.FirstOrDefaultAsync(a => a.Id == id && a.DeletedAt == null);

                if (artist != null)
                {
                    return Ok(new ArtistDTO
                    {
                        Id = artist.Id,
                        Name = artist.Name,
                        Biography = artist.Biography,
                        //Thêm các trường khác nếu cần
                        createdAt = artist.CreatedAt,
                        updatedAt = artist.UpdatedAt,
                        deletedAt = artist.DeletedAt
                    });
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
                }
            }
            catch (Exception ex)
            {
                var response = new GeneralService
                {
                    Success = false,
                    StatusCode = 500,
                    Message = ex.Message,
                    Data = ""
                };

                return StatusCode(500, response);
            }
        }


        // PUT: api/Artists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditArtist([FromForm] EditArtistModel model)
        {
            try
            {
                // Tìm nghệ sĩ cần chỉnh sửa
                var artist = await _context.Artist.FindAsync(model.Id);

                if (artist == null)
                {
                    return NotFound(new GeneralService
                    {
                        Success = false,
                        StatusCode = 404,
                        Message = "Artist not found",
                        Data = ""
                    });
                }

                // Kiểm tra và tải lên hình ảnh mới nếu có
                if (model.Image != null)
                {
                    var imageUrl = await _imgService.UploadImageAsync(model.Image, "artists");

                    if (imageUrl != null)
                    {
                        artist.Image = imageUrl;
                    }
                    else
                    {
                        return BadRequest(new GeneralService
                        {
                            Success = false,
                            StatusCode = 400,
                            Message = "Please provide a valid image.",
                            Data = ""
                        });
                    }
                }

                // Cập nhật thông tin của nghệ sĩ
                artist.Name = model.Name;
                artist.Biography = model.Biography;
                artist.UpdatedAt = DateTime.Now;

                // Lưu các thay đổi vào cơ sở dữ liệu
                await _context.SaveChangesAsync();

                // Trả về kết quả
                return Ok(new GeneralService
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "Artist updated successfully",
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


        // POST: api/Artists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Artist>> PostArtist(Artist artist)
        //{
        //    _context.Artist.Add(artist);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetArtist", new { id = artist.Id }, artist);
        //}
        [HttpPost("create")]
        public async Task<IActionResult> CreateArtists([FromForm] CreateArtistModel model)
        {
            try
            {
                var imageUrl = await _imgService.UploadImageAsync(model.Image, "Artists");
                if (imageUrl != null)
                {
                    Artist artist = new Artist
                    {
                        Name = model.Name,
                        Image = imageUrl,
                        Biography = model.Biography,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        DeletedAt = null

                    };
                    _context.Artist.Add(artist);
                    await _context.SaveChangesAsync();

                    return Created($"get-by-id?id={artist.Id}", new ArtistDTO
                    {
                        Id = artist.Id,
                        Name = artist.Name,
                        Biography = artist.Biography,
                        Image = imageUrl,
                        createdAt = artist.CreatedAt,
                        updatedAt = artist.UpdatedAt,
                        deletedAt = artist.DeletedAt,
                    });
                }
                else
                {
                    return BadRequest(new GeneralService
                    {
                        Success = false,
                        StatusCode = 400,
                        Message = "Please provide an image.",
                        Data = ""
                    });
                }
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

        // DELETE: api/Artists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            var artist = await _context.Artist.FindAsync(id);
            if (artist == null)
            {
                return NotFound();
            }

            _context.Artist.Remove(artist);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArtistExists(int id)
        {
            return _context.Artist.Any(e => e.Id == id);
        }
    }
}
