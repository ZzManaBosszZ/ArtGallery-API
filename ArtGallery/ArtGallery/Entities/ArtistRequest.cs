namespace ArtGallery.Entities
{
    public class ArtistRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; } // ID của người dùng gửi yêu cầu
        public string Name { get; set; } // Tên của nghệ sĩ
        public string Image { get; set; } // Đường dẫn đến hình ảnh của nghệ sĩ
        public string Biography { get; set; } // Tiểu sử của nghệ sĩ
        public DateTime CreatedAt { get; set; } // Ngày tạo yêu cầu
        public DateTime UpdatedAt { get; set; } // Ngày cập nhật yêu cầu
        public DateTime? ApprovedAt { get; set; } // Ngày yêu cầu được chấp nhận
        public DateTime? RejectedAt { get; set; } // Ngày yêu cầu bị từ chối
        public User User { get; set; }
    }
}
