using System.Security.Claims;

namespace WepAPIAssignment.extentsions
{
    public static class ClaimsPrincipalExtinstions
    {
        public static string RetriveEmailFromPrincipal(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.Email);
        }
    }
}
