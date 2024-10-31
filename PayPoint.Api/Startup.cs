using FluentValidation;
using FluentValidation.AspNetCore;
using PayPoint.Api.Validators;
using PayPoint.Services.Utils;

namespace PayPoint.Api;

public static class Startup
{
    public static void Configure(this WebApplication app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
    }

    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructureServices(configuration);

        services.AddControllers();

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<ProductCreateDtoValidator>();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
}