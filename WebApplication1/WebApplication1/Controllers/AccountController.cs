using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using WebApplication1.Helpers;
using WebApplication1.Models;
using WebApplication1.Services;

public class AccountController : Controller
{
    private readonly IMemoryCache _memoryCache;
    private readonly EmailService _emailService;
    private readonly HopAmChuan2Context _context;

    public AccountController(IMemoryCache memoryCache, EmailService emailService, HopAmChuan2Context context)
    {
        _memoryCache = memoryCache;
        _emailService = emailService;
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> SendResetLink(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
        {
            ModelState.AddModelError("", "Email không tồn tại trong hệ thống.");
            return View("ForgotPassword");
        }

        // Generate OTP
        var otpCode = OtpHelper.GenerateOtp(email);

        // Save OTP to memory cache (valid for 10 minutes)
        _memoryCache.Set(email, otpCode, TimeSpan.FromMinutes(10));

        // Send OTP via email
        var subject = "Reset Password OTP";
        var body = $"<p>Xin chào,</p><p>Mã OTP của bạn là: <strong>{otpCode}</strong></p><p>Mã này có hiệu lực trong 10 phút.</p>";
        _emailService.SendEmail(email, subject, body);

        TempData["Message"] = "Mã OTP đã được gửi đến email của bạn.";
        return RedirectToAction("VerifyOtp", new { email });
    }

    [HttpGet]
    public IActionResult VerifyOtp(string email)
    {
        ViewBag.Email = email;
        return View();
    }

    [HttpPost]
    public IActionResult VerifyOtp(string email, string otpCode)
    {
        if (_memoryCache.TryGetValue(email, out string cachedOtp))
        {
            if (cachedOtp == otpCode)
            {
                TempData["Message"] = "Mã OTP hợp lệ. Vui lòng thay đổi mật khẩu.";
                return RedirectToAction("ResetPassword", new { email });
            }
            else
            {
                ModelState.AddModelError("", "Mã OTP không chính xác.");
            }
        }
        else
        {
            ModelState.AddModelError("", "Mã OTP đã hết hạn. Vui lòng yêu cầu mã OTP mới.");
        }

        ViewBag.Email = email; 
        return View();
    }

    [HttpGet]
    public IActionResult ResetPassword(string email)
    {
        ViewBag.Email = email;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword(string email, string password, string confirmPassword)
    {
        if (password != confirmPassword)
        {
            TempData["Message"] = "Mật khẩu không khớp.";
            return RedirectToAction("ResetPassword", new { email });
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user != null)
        {
            user.Password = password; // Lưu mật khẩu mới (cần mã hóa trước khi lưu)
            await _context.SaveChangesAsync();
            TempData["Message"] = "Mật khẩu đã được thay đổi thành công.";
            return RedirectToAction("Index","Login");
        }

        TempData["Message"] = "Email không tồn tại.";
        return RedirectToAction("ForgotPassword");
    }
}
