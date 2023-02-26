using FluentValidation;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using OrderTestWebApp.DTOs;
using OrderTestWebApp.EF;
using OrderTestWebApp.Models;
using OrderTestWebApp.Services;
using OrderTestWebApp.Services.Interfaces;
using OrderTestWebApp.Validator;

namespace OrderTestWebApp.Extensions
{
    public static class AddDependensies
    {
        public static IServiceCollection AddDalDependensies(this IServiceCollection services, string connectionString)
        {
            services
                    .AddScoped<IOrderService, OrderService>()
                    .AddScoped<IValidator<Order>, OrderValidator>()
            .AddScoped<IValidator<OrderDTO>, OrderValidatorDTO>()
            .AddScoped<OrderDbContext>()
            .AddDbContext<OrderDbContext>(opt =>
            opt.UseSqlite(connectionString));
            return services;
        }
    }
}
