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
using ArtGallery.Service.Artist;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
using ArtGallery.Models.ArtWork;
using ArtGallery.Models.SchoolOfArt;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ArtGallery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly ArtGalleryApiContext _context;
        private readonly IImgService _imgService;
        private readonly IArtistService _artistService;

        public ArtistsController(ArtGalleryApiContext context, IImgService imgService, IArtistService artistService)
        {
            _context = context;
            _imgService = imgService;
            _artistService = artistService;


        }


        [HttpGet]
        [Authorize(Roles = "Super Admin")]
        public async Task<IActionResult> GetArtistAll(
        [FromQuery] string search = null,
        [FromQuery] List<int> artWorkIds = null,
        [FromQuery] List<int> schoolOfArtsIds = null)
        {
            try
            {
                // Bắt đầu với truy vấn gốc để lấy danh sách nghệ sĩ
                var query = _context.Artist
                    .Include(a => a.ArtWorks)
                    .Include(a => a.SchoolOfArts)
                    .Where(a => a.DeletedAt == null);

                // Áp dụng bộ lọc tìm kiếm
                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(a => a.Name.Contains(search));
                }

                // Áp dụng bộ lọc SchoolOfArtIds
                if (schoolOfArtsIds != null && schoolOfArtsIds.Any())
                {
                    query = query.Where(a => a.SchoolOfArts.Any(soa => schoolOfArtsIds.Contains(soa.Id)));
                }

                // Áp dụng bộ lọc ArtWorkIds
                if (artWorkIds != null && artWorkIds.Any())
                {
                    query = query.Where(a => a.ArtWorks.Any(aw => artWorkIds.Contains(aw.Id)));
                }

                // Lấy danh sách nghệ sĩ từ cơ sở dữ liệu
                var artists = await query.OrderByDescending(a => a.Id).ToListAsync();

                // Tạo danh sách kết quả để trả về
                var result = artists.Select(a => new ArtistDTO
                {
                    Id = a.Id,
                    Name = a.Name,
                    Biography = a.Biography,
                    Image = a.Image,

                    ArtWork = a.ArtWorks.Select(aw => new ArtWorkResponse
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
                        CreatedAt = aw.CreatedAt,
                        UpdatedAt = aw.UpdatedAt,
                        DeletedAt = aw.DeletedAt
                    }).ToList(),

                    SchoolOfArt = a.SchoolOfArts.Select(soa => new SchoolOfArtResponse
                    {
                        Id = soa.Id,
                        Name = soa.Name,
                        ArtistId = soa.ArtistId,
                        ArtWorkId = soa.ArtWorkId,
                        Slug = soa.Slug,
                        CreatedAt = soa.CreatedAt,
                        UpdatedAt = soa.UpdatedAt,
                        DeletedAt = soa.DeletedAt
                    }).ToList()
                }).ToList();

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



        [HttpGet("{id}")]
        [Authorize(Roles = "Super Admin")]
        public async Task<IActionResult> GetArtistById(int id)
        {
            try
            {
                var artist = await _context.Artist
                    .Include(a => a.ArtWorks)
                    .Include(a => a.SchoolOfArts)
                    .FirstOrDefaultAsync(x => x.Id == id && x.DeletedAt == null);

                if (artist == null)
                {
                    return NotFound();
                }

                var artistDto = new ArtistDTO
                {
                    Id = artist.Id,
                    Name = artist.Name,
                    Biography = artist.Biography,
                    Image = artist.Image,
                    createdAt = artist.CreatedAt,
                    updatedAt = artist.UpdatedAt,
                    deletedAt = artist.DeletedAt,

                    ArtWork = artist.ArtWorks.Select(aw => new ArtWorkResponse
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
                        CreatedAt = aw.CreatedAt,
                        UpdatedAt = aw.UpdatedAt,
                        DeletedAt = aw.DeletedAt
                    }).ToList(),

                    SchoolOfArt = artist.SchoolOfArts.Select(soa => new SchoolOfArtResponse
                    {
                        Id = soa.Id,
                        Name = soa.Name,
                        ArtistId = soa.ArtistId,
                        ArtWorkId = soa.ArtWorkId,
                        Slug = soa.Slug,
                        CreatedAt = soa.CreatedAt,
                        UpdatedAt = soa.UpdatedAt,
                        DeletedAt = soa.DeletedAt
                    }).ToList()
                };

                return Ok(artistDto);
            }
            catch (Exception ex)
            {
                var response = new GeneralService
                {
                    Success = false,
                    StatusCode = 500,
                    Message = "Internal server error",
                    Data = ex.Message
                };

                return StatusCode(500, response);
            }
        }


        [HttpPost("create")]
        [Authorize(Roles = "Super Admin")]
        public async Task<IActionResult> CreateArtist([FromForm] CreateArtistModel model)
        {
            try
            {
                bool artistExists = await _context.Artist.AnyAsync(a => a.Name.Equals(model.Name));

                // Kiểm tra xem nghệ sĩ đã tồn tại hay chưa
                if (artistExists)
                {
                    return BadRequest(new GeneralService
                    {
                        Success = false,
                        StatusCode = 400,
                        Message = "Artist already exists",
                        Data = ""
                    });
                }


                var image = await _imgService.UploadImageAsync(model.ImagePath, "Artist");
                if (image != null)
                {
                    // Tạo một đối tượng Artist từ dữ liệu trong model và ảnh đã tải lên
                    Artist artist = new Artist
                    {
                        Name = model.Name,
                        Image = image,
                        Biography = model.Biography,
                        CreatedAt = DateTime.Now,
                        DeletedAt = DateTime.Now,
                        UpdatedAt = null,
                    };

                    // Thêm nghệ sĩ mới vào cơ sở dữ liệu
                    _context.Artist.Add(artist);
                    await _context.SaveChangesAsync();

                    // Trả về thông báo thành công
                    return Created($"get-by-id?id={artist.Id}", new ArtistDTO
                    {
                        Id = artist.Id,
                        Name = artist.Name,
                        Image = artist.Image,
                        Biography = artist.Biography,
                        createdAt = artist.CreatedAt,
                        updatedAt = artist.UpdatedAt,
                        deletedAt = null,
                    });

                }
                else
                {
                    // Trả về thông báo lỗi nếu có vấn đề trong quá trình tải ảnh lên
                    return BadRequest(new GeneralService
                    {
                        Success = false,
                        StatusCode = 400,
                        Message = "Error uploading image",
                        Data = ""
                    });
                }
            }
            catch (Exception ex)
            {
                // Trả về thông báo lỗi nếu có lỗi xảy ra trong quá trình xử lý
                var response = new GeneralService
                {
                    Success = false,
                    StatusCode = 500,
                    Message = "Error creating artist: " + ex.Message,
                    Data = ""
                };

                return StatusCode(500, response);
            }
        }

        [HttpPut("edit")]
        [Authorize(Roles = "Super Admin")]
        public async Task<IActionResult> EditArtist([FromForm] EditArtistModel model)
        {
            try
            {
                // Tìm kiếm nghệ sĩ cần chỉnh sửa
                Artist existingArtist = await _context.Artist.FirstOrDefaultAsync(e => e.Id == model.Id);

                if (existingArtist != null)
                {
                    existingArtist.Name = model.Name;
                    existingArtist.UpdatedAt = DateTime.Now;

                    if (model.ImagePath != null)
                    {
                        string image = await _imgService.UploadImageAsync(model.ImagePath, "Artist");

                        if (image == null)
                        {
                            return BadRequest(new GeneralService
                            {
                                Success = false,
                                StatusCode = 400,
                                Message = "Failed to upload avatar.",
                                Data = ""
                            });
                        }

                        existingArtist.Image = image;
                    }

                    _context.Artist.Update(existingArtist);
                    await _context.SaveChangesAsync();

                    return Ok(new GeneralService
                    {
                        Success = true,
                        StatusCode = 200,
                        Message = "Artist edited successfully",
                        Data = existingArtist
                    });
                }
                else
                {
                    return NotFound(new GeneralService
                    {
                        Success = false,
                        StatusCode = 404,
                        Message = "Artist not found",
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
        [HttpDelete("delete")]
        [Authorize(Roles = "Super Admin")]
        public async Task<IActionResult> DeleteArtist(List<int> ids)
        {
            try
            {
                foreach (var id in ids)
                {
                    Artist artist = await _context.Artist.FindAsync(id);

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
