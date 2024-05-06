using ArtGallery.Entities;
using ArtGallery.Service.Artists;
using ArtGallery.Service.IMG;
using Microsoft.AspNetCore.Authorization;
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


        [HttpGet("revenue/yearly")]
        public IActionResult GetYearlySales()
        {
            int currentYear = DateTime.UtcNow.Year;
            int startYear = currentYear - 4;
            List<int> allYears = Enumerable.Range(startYear, 5).ToList();
            var yearlySales = allYears
                .GroupJoin(_context.Offer,
                    year => year,
                    order => order.CreatedAt.Value.Year,
                    (year, orders) => new
                    {
                        Year = year,
                        TotalSales = orders.Sum(o => o.Total)
                    })
                .OrderBy(result => result.Year)
                .ToList();

            return Ok(yearlySales);
        }

        [HttpGet("revenue/monthly/{year}")]
        public IActionResult GetMonthlySales(int year)
        {
            // Validate the input year
            if (year <= 0)
            {
                return BadRequest("Invalid year parameter.");
            }
            DateTime startDate = new DateTime(year, 1, 1);
            List<DateTime> allMonthsOfYear = Enumerable.Range(0, 12)
                .Select(offset => startDate.AddMonths(offset))
                .ToList();
            var monthlySales = allMonthsOfYear
                .GroupJoin(_context.Offer,
                    date => new { Year = date.Year, Month = date.Month },
                    order => new { Year = order.CreatedAt.Value.Year, Month = order.CreatedAt.Value.Month },
                    (date, orders) => new
                    {
                        Year = date.Year,
                        Month = date.Month,
                        TotalSales = orders.Sum(o => o.Total)
                    })
                .OrderBy(result => result.Year)
                .ThenBy(result => result.Month)
                .Select(result => new
                {
                    Year = result.Year,
                    Month = result.Month,
                    TotalSales = result.TotalSales
                })
                .ToList();

            return Ok(monthlySales);
        }

        [HttpGet("revenue/weekly")]
        public IActionResult GetWeeklySales()
        {
            DateTime today = DateTime.UtcNow.Date;
            List<DateTime> past7Days = Enumerable.Range(0, 7)
                .Select(offset => today.AddDays(-offset))
                .ToList(); 
            var past7DaysSales = past7Days
                .GroupJoin(_context.Offer,
                    date => date.Date,
                    order => order.CreatedAt.Value.Date,
                    (date, orders) => new
                    {
                        Date = date,
                        TotalSales = orders.Sum(o => o.Total)
                    })
                .Select(result => new
                {
                    Date = result.Date,
                    TotalSales = result.TotalSales
                })
                .ToList();

            return Ok(past7DaysSales);
        }


        [HttpGet("TotalRevenue")]
        public async Task<IActionResult> GetTotalRevenue()
        {
            try
            {
                var offers = await _context.Offer
                    .Where(o => o.Status == 1)
                    .ToListAsync();
                var totalRevenue = offers.Sum(o => o.Total);

                return Ok(new { totalRevenue });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/Offer/Today
        [HttpGet("Today")]
        public IActionResult GetOffersCreatedToday()
        {
            try
            {               
                var todayStart = DateTime.Today;
                var todayEnd = DateTime.Today.AddDays(1).AddSeconds(-1);
                var offersToday = _context.Offer
                    .Include(o => o.User) 
                    .Where(o => o.CreatedAt >= todayStart && o.CreatedAt <= todayEnd)
                    .ToList();

                return Ok(offersToday);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/Offer/TotalOfferToday
        [HttpGet("TotalOfferToday")]
        public async Task<IActionResult> GetTotalOfferToday()
        {
            try
            {
                DateTime today = DateTime.Today; 
                var totalOfferToday = await _context.Offer
                    .Where(o => o.CreatedAt.HasValue && o.CreatedAt.Value.Date == today)
                    .CountAsync();

                return Ok(new { totalOfferToday });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
