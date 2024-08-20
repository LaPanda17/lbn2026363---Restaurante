using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Restaurante.Models
{
    public class AuthorizeRoleAttribute: Attribute, IAuthorizationFilter
    {
        private readonly string[] _roles;

        public AuthorizeRoleAttribute(params string[] roles)
        {
            _roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userId = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            if (userId == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            using (var scope = context.HttpContext.RequestServices.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<RestauranteContext>();
                var userRoles = db.UsuarioRoles
                    .Where(ur => ur.UsuarioId == int.Parse(userId))
                    .Select(ur => ur.Rol.Nombre)
                    .ToList();

                if (!_roles.Any(role => userRoles.Contains(role)))
                {
                    context.Result = new ForbidResult();
                }
            }
        }
    }
}
