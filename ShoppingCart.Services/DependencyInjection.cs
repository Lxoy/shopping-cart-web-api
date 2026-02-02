using Microsoft.Extensions.DependencyInjection;
using ShoppingCart.Services.Interfaces;
using ShoppingCart.Services.Services;

namespace ShoppingCart.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<ICartService, CartService>();

            return services;
        }
    }
}
