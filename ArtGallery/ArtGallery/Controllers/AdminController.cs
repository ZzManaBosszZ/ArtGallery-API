using ArtGallery.DTOs;
using ArtGallery.Entities;
using ArtGallery.Helper;
using ArtGallery.Models.Artist;
using ArtGallery.Models.GeneralService;
using ArtGallery.Service.Artists;
using ArtGallery.Service.Email;
using ArtGallery.Service.IMG;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ArtGallery.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ArtGalleryApiContext _context;
        private readonly IImgService _imgService;
        private readonly IEmailService _emailService;


        public AdminController(ArtGalleryApiContext context, IImgService imgService, IEmailService emailService)
        {
            _context = context;
            _imgService = imgService;
            _emailService = emailService;

        }

        [HttpPost("request-artist")]
        [Authorize/*(Roles = "User")]*/]
        public async Task<IActionResult> RequestArtist([FromForm] ArtistRequestModel model)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (!identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            try
            {
                var userClaims = identity.Claims;
                var userId = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == Convert.ToInt32(userId));
                if (user == null)
                {
                    return NotFound("User not found");
                }
                // Tạo một thực thể mới đại diện cho yêu cầu


                var image = await _imgService.UploadImageAsync(model.ImagePath, "Artist");
                if (image != null)
                {

                    var a = new ArtistRequest
                    {
                        UserId = user.Id,
                        Name = model.Name,
                        Image = image,
                        Biography = model.Biography,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };

                    return Created($"get-by-id?id={a.Id}", new ArtistRequestDTO
                    {
                        Id = a.Id,
                        Name = a.Name,
                        Image = a.Image,
                        Biography = a.Biography,
                        createdAt = a.CreatedAt,
                        updatedAt = a.UpdatedAt,
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
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("accept-artist-request/{requestId}")]
        [Authorize/*(Roles = "Admin")*/]
        public async Task<IActionResult> AcceptArtistRequest(int requestId)
        {
            try
            {
                switch (requestId)
                {
                    case 1:
                        var artistRequest = await _context.ArtistRequests.FindAsync(requestId);
                        if (artistRequest == null)
                        {
                            return NotFound("Artist request not found");
                        }
                        var user = await _context.Users.FindAsync(artistRequest.UserId);
                        if (user == null)
                        {
                            return NotFound("User not found");
                        }
                        user.Role = "Artist";
                        var newArtist = new Artist
                        {
                            Name = artistRequest.Name,
                            Image = artistRequest.Image,
                            Biography = artistRequest.Biography,
                        };
                        _context.Artist.Add(newArtist);
                        var mailrequest = new Mailrequest
                        {
                            ToEmail = user.Email,
                            Subject = "Artist Request Accepted",
                            Body = "Your artist request has been accepted. You are now an artist!"
                        };
                        await _emailService.SendEmailAsync(mailrequest);
                        await _context.SaveChangesAsync();

                        return Ok("Artist request accepted successfully");
                    case 0:
                        var rejectedRequest = await _context.ArtistRequests.FindAsync(requestId);
                        if (rejectedRequest == null)
                        {
                            return NotFound("Artist request not found");
                        }

                        // Gửi email thông báo từ chối
                        var rejectMailrequest = new Mailrequest
                        {
                            ToEmail = rejectedRequest.User.Email,
                            Subject = "Artist Request Rejected",
                            Body = "Your artist request has been rejected."
                        };
                        await _emailService.SendEmailAsync(rejectMailrequest);

                        // Xóa bản ghi ArtistRequest đã bị từ chối
                        _context.ArtistRequests.Remove(rejectedRequest);
                        await _context.SaveChangesAsync();
                        return Ok("Artist request rejected successfully");
                    default:
                        return BadRequest("Invalid request");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
