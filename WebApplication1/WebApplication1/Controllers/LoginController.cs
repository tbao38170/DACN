using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using WebApplication1.Models;


namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        private readonly HopAmChuan2Context _context;

        public LoginController(HopAmChuan2Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var checkLogin = HttpContext.Session.GetInt32("isLogin");
            if (checkLogin != null)
            {
                // Get user information from session
                var userId = HttpContext.Session.GetInt32("user");
                var user = _context.Users.FirstOrDefault(u => u.Id == userId);

                // Pass user information to the view
                return View(user);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(string TenTK, string TenMK)
        {
            var model = _context.Users.FirstOrDefault(t => t.UserName == TenTK && t.Password == TenMK);
            if (model != null)
            {
                HttpContext.Session.SetInt32("user", model.Id);
                HttpContext.Session.SetInt32("isLogin", 1);
                HttpContext.Session.SetString("userName", model.UserName);

                // Kiểm tra loại tài khoản
                if (model.IdUserGroup == 1)
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (model.IdUserGroup == 2)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.ErrMess = "Tài Khoản Hoặc Mật Khẩu Không Đúng";
            return View("Index");

        }

        [CheckLoginUser]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        //Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user, IFormCollection form)
        {
            try
            {
                // Extracting values from StringValues to string
                var name = form["Name"].ToString();
                var phone = form["Phone"].ToString();
                var username = form["Username"].ToString();
                var email = form["Email"].ToString();
                var password = form["Password"].ToString();
                var confirmPassword = form["ConfirmPassword"].ToString();

                // Check if the username already exists
                var existingUser = _context.Users.FirstOrDefault(u => u.UserName == username);
                if (existingUser != null)
                {
                    TempData["ErrMess"] = "Tài khoản đã tồn tại";
                    return RedirectToAction("Register", "Login");
                }

                // Check if password and confirm password match
                if (password != confirmPassword)
                {
                    TempData["ErrMess"] = "Mật khẩu không khớp";
                    return RedirectToAction("Register", "Login");
                }

                // Validate the user data
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                {
                    TempData["ErrMess"] = "Vui lòng điền đầy đủ thông tin";
                    return RedirectToAction("Register", "Login");
                }

                // Setting user properties
                user.Password = password;
                user.UserName = username;
                user.Email = email;
                user.Name = name;
                user.Phone = phone;
                user.IdUserGroup = 2;

                _context.Users.Add(user);
                _context.SaveChanges();

                TempData["SuccessMess"] = "Tạo tài khoản thành công";
                return RedirectToAction("Register", "Login");
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);

                TempData["ErrMess"] = "Có lỗi xảy ra trong quá trình đăng ký";
                return RedirectToAction("Register", "Login");
            }
        }

        //Forgot Password
        public IActionResult ForgotPassword()
        {
            return View();
        }

        
    }
}
