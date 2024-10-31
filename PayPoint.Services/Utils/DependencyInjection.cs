using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PayPoint.Core.Interfaces;
using PayPoint.Infrastructure.Data;
using PayPoint.Services.Implementations;
using PayPoint.Services.Interfaces;
using PayPoint.Services.Mappings;

namespace PayPoint.Services.Utils;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PayPointDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("PayPointConnection")));
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ISubCategoryService, SubCategoryService>();

        services.AddAutoMapper(typeof(MappingProfile).Assembly);

        return services;
    }
}
