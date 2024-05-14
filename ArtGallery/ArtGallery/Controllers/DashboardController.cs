
﻿using ArtGallery.DTOs;
using ArtGallery.Entities;
using ArtGallery.Models.GeneralService;
using ArtGallery.Service.Artists;
using ArtGallery.Service.IMG;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Security;

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
        [Authorize(Roles = "Super Admin, Artist")]
        public async Task<IActionResult> GetTotalOfferCount()
        {
            try
            {
                // Sử dụng LINQ để đếm số lượng offer trong cơ sở dữ liệu
                var totalOffer = await _context.Offer.Where(m => m.DeletedAt == null).CountAsync();

                // Trả về số lượng offer trong response
                return Ok(totalOffer);
            }
            catch (Exception ex)
            {
                // Xử lý nếu có lỗi xảy ra
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("total-count-user")]
        [Authorize(Roles = "Super Admin")]

        public async Task<IActionResult> GetUserCount()
        {
            var userCount = new
            {
                TotalUser = await _context.Users.Where(u => u.Role.Equals("User")).CountAsync(),
                TotalArtist = await _context.Users.Where(u => u.Role.Equals("Artist")).CountAsync(),
            };

            return Ok(userCount);
        }

        [HttpGet("total-count-artist")]
        [Authorize(Roles = "Super Admin")]
        public async Task<IActionResult> GetTotalArtistCount()
        {
            try
            {
                // Sử dụng LINQ để đếm số lượng artist trong cơ sở dữ liệu
                var totalArtist = await _context.Artist.Where(m => m.DeletedAt == null).CountAsync();

                // Trả về số lượng artist trong response
                return Ok(totalArtist);
            }
            catch (Exception ex)
            {
                // Xử lý nếu có lỗi xảy ra
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("total-count-artwork")]
        [Authorize(Roles = "Super Admin, Artist")]
        public async Task<IActionResult> GetTotalArtworkCount()
        {
            try
            {
                // Sử dụng LINQ để đếm số lượng artwork trong cơ sở dữ liệu
                var totalArtwork = await _context.ArtWork.Where(m => m.DeletedAt == null).CountAsync();

                // Trả về số lượng artwork trong response
                return Ok(totalArtwork);
            }
            catch (Exception ex)
            {
                // Xử lý nếu có lỗi xảy ra
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("revenue/yearly")]
        [Authorize(Roles = "Super Admin, Artist")]
        public IActionResult GetYearlySales()
        {
            int currentYear = DateTime.UtcNow.Year;
            int startYear = currentYear - 4;
            List<int> allYears = Enumerable.Range(startYear, 5).ToList();
            var yearlySales = allYears
       .GroupJoin(_context.Offer.Where(o => o.IsPaid == 1), // Lọc các đơn hàng đã thanh toán
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
        [Authorize(Roles = "Super Admin, Artist")]
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
         .GroupJoin(_context.Offer.Where(o => o.IsPaid == 1),
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
        [Authorize(Roles = "Super Admin, Artist")]
        public IActionResult GetWeeklySales()
        {
            DateTime today = DateTime.UtcNow.Date;
            List<DateTime> past7Days = Enumerable.Range(0, 7)
                .Select(offset => today.AddDays(-offset))
                .ToList();
            var past7DaysSales = past7Days
        .GroupJoin(_context.Offer.Where(o => o.IsPaid == 1),
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


        [HttpGet("total-revenue")]
        [Authorize(Roles = "Super Admin, Artist")]
        public async Task<IActionResult> GetTotalRevenue()
        {
            try
            {
                var offers = await _context.Offer
                    .Where(o => o.IsPaid == 1)
                    .ToListAsync();
                var totalRevenue = offers.Sum(o => o.Total);

                return Ok(new { totalRevenue });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/Offer/TotalOfferToday
        [HttpGet("total-offer-today")]
        [Authorize(Roles = "Super Admin, Artist")]
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

        [HttpGet("list-offer-today")]
        [Authorize(Roles = "Super Admin, Artist")]
        public async Task<IActionResult> GetListOfferToDay()
        {
            try
            {
                DateTime today = DateTime.Now.Date;
                List<Offer> offers = await _context.Offer.Include(o => o.User).Include(o => o.OfferArtWorks)
        .ThenInclude(oaw => oaw.ArtWork).Where(o => o.CreatedAt.Value.Date == today).OrderByDescending(p => p.CreatedAt).ToListAsync();
                List<OfferDTO> result = new List<OfferDTO>();
                foreach (var offer in offers)
                {
                    result.Add(new OfferDTO
                    {
                        OfferCode = offer.OfferCode,
                        ArtWorkId = offer.ArtWorkId,
                        UserId = offer.UserId,
                        //UserName = offer.User.Fullname,
                        ToTal = offer.Total,
                        Status = offer.Status,
                        ArtWorkNames = offer.OfferArtWorks.Select(oaw => oaw.ArtWork.Name).ToList(),
                        ArtWorkImages = offer.OfferArtWorks.Select(oaw => oaw.ArtWork.ArtWorkImage).ToList(),
                        OfferPrice = offer.OfferPrice,
                        createdAt = offer.CreatedAt,
                        updatedAt = offer.UpdatedAt,
                        deletedAt = offer.DeletedAt,
                    }) ;
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

    }
}
