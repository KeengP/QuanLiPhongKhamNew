using System.ComponentModel.DataAnnotations;

namespace QuanLiPhongKham.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập không được để trống!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email không được để trống!")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ!")]
        public string Email { get; set; }

        public string Role { get; set; } = "User";
    }
}
