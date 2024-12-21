using Microsoft.AspNetCore.Http;
using WebApplication1.Models;


namespace WebChordCore.Utilities
{
    public static class CurrentContext
    {
        public static bool IsLogged(HttpContext httpContext)
        {
            return httpContext.Session.GetInt32("isLogin") == 1;
        }

        public static User GetCurUser(HttpContext httpContext, HopAmChuan2Context context)
        {
            var userId = httpContext.Session.GetInt32("user");
            if (userId.HasValue)
            {
                return context.Users.FirstOrDefault(u => u.Id == userId.Value);
            }
            return null;
        }
    }
}
