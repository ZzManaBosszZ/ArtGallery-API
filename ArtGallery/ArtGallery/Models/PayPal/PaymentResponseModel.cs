namespace ArtGallery.Models.PayPal
{
    public class PaymentResponseModel
    {
        public string OfferDescription { get; set; }
        public string TransactionId { get; set; }
        public string OfferId { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentId { get; set; }
        public string PayerId { get; set; }
        public bool Success { get; set; }
    }
}
