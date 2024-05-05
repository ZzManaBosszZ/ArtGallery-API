using ArtGallery.Models.PayPal;
using ArtGallery.Service.Paypal;
using Microsoft.AspNetCore.Mvc;

namespace ArtGallery.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPayPalService _payPalService;
        public PaymentsController( IPayPalService payPalService)
        {
            _payPalService = payPalService;
        }
        [HttpPost]
        [Route("PayPal")]
        public async Task<IActionResult> CreatePaymentUrl([FromForm] PaymentInformationModel model)
        {
            var url = await _payPalService.CreatePaymentUrl(model, HttpContext);

            return Redirect(url);
        }
        [HttpPost]
        public IActionResult PaymentCallback()
        {
            var response = _payPalService.PaymentExecute(Request.Query);

            return Json(response);
        }

        private IActionResult Json(PaymentResponseModel response)
        {
            throw new NotImplementedException();
        }
    }
}
