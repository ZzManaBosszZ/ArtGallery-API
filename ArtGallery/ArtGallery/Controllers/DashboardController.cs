using ArtGallery.Entities;
using ArtGallery.Service.Artists;
using ArtGallery.Service.IMG;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly ArtGalleryApiContext _context;
      
        public DashboardController(ArtGalleryApiContext context)
        {
            _context = context;
           
        }

        // Endpoint để lấy tổng số lượng offer
        [HttpGet("total-count-offer")]
        public async Task<IActionResult> GetTotalOfferCount()
        {
            try
            {
                // Sử dụng LINQ để đếm số lượng offer trong cơ sở dữ liệu
                var totalCount = await _context.Offer.Where(m => m.DeletedAt == null).CountAsync();

                // Trả về số lượng offer trong response
                return Ok(totalCount);
            }
            catch (Exception ex)
            {
                // Xử lý nếu có lỗi xảy ra
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("total-count-user")]
        public async Task<IActionResult> GetTotalUserCount()
        {
            try
            {
                // Sử dụng LINQ để đếm số lượng user trong cơ sở dữ liệu
                var totalCount = await _context.Users.Where(m => m.DeletedAt == null).CountAsync();

                // Trả về số lượng user trong response
                return Ok(totalCount);
            }
            catch (Exception ex)
            {
                // Xử lý nếu có lỗi xảy ra
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("total-count-Artist")]
        public async Task<IActionResult> GetTotalArtistCount()
        {
            try
            {
                // Sử dụng LINQ để đếm số lượng artist trong cơ sở dữ liệu
                var totalCount = await _context.Artist.Where(m => m.DeletedAt == null).CountAsync();

                // Trả về số lượng artist trong response
                return Ok(totalCount);
            }
            catch (Exception ex)
            {
                // Xử lý nếu có lỗi xảy ra
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("total-count")]
        public async Task<IActionResult> GetTotalArtworkCount()
        {
            try
            {
                // Sử dụng LINQ để đếm số lượng artwork trong cơ sở dữ liệu
                var totalCount = await _context.ArtWork.Where(m => m.DeletedAt == null).CountAsync();

                // Trả về số lượng artwork trong response
                return Ok(totalCount);
            }
            catch (Exception ex)
            {
                // Xử lý nếu có lỗi xảy ra
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
