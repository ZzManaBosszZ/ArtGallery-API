using ArtGallery.Entities;
using ArtGallery.Models.GeneralService;
using ArtGallery.Models.Users;
using ArtGallery.Service.IMG;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ArtGallery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {


        private readonly ArtGalleryApiContext _context;

        public MenuController(ArtGalleryApiContext context)
        {
            _context = context;

        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Menu ()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (!identity.IsAuthenticated)
            {
                return Unauthorized(new GeneralService{ Success = false, StatusCode = 401, Message = "Not Authorized", Data = "" });
            }

            try
            {
                var userClaims = identity.Claims;
                var userId = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == Convert.ToInt32(userId));

                if (user == null)
                {
                    return NotFound(new GeneralService
                    {
                        Success = false,
                        StatusCode = 404,
                        Message = "Incorrect current password",
                        Data = ""
                    });
                }

                if (user.Role.Contains("Super Admin"))
                {
                    var menu = new List<Menu>
                    {
                        new Menu { Title = "Dashboard", Url = "", Icon = "" },
                        new Menu { Title = "Artist", Url = "", Icon = "" },
                        new Menu { Title = "ArtWork", Url = "", Icon = "" },
                        new Menu { Title = "Event", Url = "", Icon = "" },
                        new Menu { Title = "", Url = "", Icon = "" },
                        new Menu { Title = "", Url = "", Icon = "" },
                        //new Menu { Title = "", Url = "", Icon = "" },
                        //new Menu { Title = "", Url = "", Icon = "" },
                        //new Menu { Title = "", Url = "", Icon = "" },
                        //new Menu { Title = "", Url = "", Icon = "" },
                        //new Menu { Title = "", Url = "", Icon = "" },
                        //new Menu { Title = "", Url = "", Icon = "" },
                        //new Menu { Title = "", Url = "", Icon = "" },

                    };
                    return Ok(menu);
                }
                
                else
                {
                    // Mặc định cho các trường hợp khác
                    var menu = new List<Menu>();
                    return Ok(menu);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new GeneralService
                {
                    Success = false,
                    StatusCode = 404,
                    Message = ex.Message,
                });
            }
        }
    } 
}
