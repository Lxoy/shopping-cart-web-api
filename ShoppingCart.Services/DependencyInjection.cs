using Microsoft.Extensions.DependencyInjection;
using ShoppingCart.Services.Services;

namespace ShoppingCart.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ArticleService>();
            services.AddScoped<CartService>();

            return services;
        }
    }
}
