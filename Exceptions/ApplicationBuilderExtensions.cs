using Microsoft.AspNetCore.Builder;

namespace FamilyFinance.Exceptions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddGlobalErrorHandler(this IApplicationBuilder applicationBuilder)
     => applicationBuilder.UseMiddleware<GlobalExceptionHandlingMiddleware>();
    }
}
