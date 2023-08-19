using System.Linq;

namespace CoreDevextremeTheme.Middlewares
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ApplicationDbContext dbContext)
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Kullanıcının rollerini veritabanından alın
            var userRoles = dbContext.UserRoles
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.RoleId)
                .ToList();

            // Kullanıcının isteğe erişimi var mı kontrol et
            var controller = context.Request.Path.Value.Split('/')[2]; // Controller adını alın
            var authorizedRoles = dbContext.ControllerRoles
                .Where(cr => cr.ControllerName == controller)
                .Select(cr => cr.RoleId)
                .ToList();

            // Kullanıcının yetkisi varsa devam et, yoksa 403 Forbidden döndür
            if (userRoles.Any(roleId => authorizedRoles.Contains(int.Parse(roleId))))
            {
                await _next(context);
            }
            else
            {
                context.Response.StatusCode = 403;
            }
        }

    }
}
