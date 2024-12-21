using System;
using System.Security.Cryptography;
using System.Text;

namespace WebApplication1.Helpers
{
    public static class OtpHelper
    {
        public static string GenerateOtp(string email)
        {
            // Sử dụng email và timestamp để tạo mã OTP
            var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmm"); // Lấy thời gian hiện tại đến phút
            var input = $"{email}:{timestamp}";

            // Hash dữ liệu để tạo mã OTP
            using (var sha256 = SHA256.Create())
            {
                var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Chuyển hash thành mã OTP (6 chữ số)
                var otp = BitConverter.ToInt32(hash, 0) % 1000000;
                return Math.Abs(otp).ToString("D6"); // Đảm bảo mã luôn dương và 6 chữ số
            }
        }

        public static bool ValidateOtp(string email, string otpCode)
        {
            // Tái tạo mã OTP và kiểm tra
            var generatedOtp = GenerateOtp(email);
            return generatedOtp == otpCode;
        }
    }
}
