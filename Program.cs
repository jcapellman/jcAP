using jcAP.API.Repositories;
using jcAP.API.Data;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.OpenApi;

using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace jcAP.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // logging from configuration
            builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

            // EF Core - read connection string from config / env
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? Environment.GetEnvironmentVariable("JCAP__CONNECTIONSTRING")
                ?? throw new InvalidOperationException("Connection string not configured.");

            builder.Services.AddDbContextPool<AppDbContext>(options =>
                options.UseNpgsql(connectionString, npgsql =>
                {
                    npgsql.EnableRetryOnFailure(3);
                    npgsql.CommandTimeout(30);
                }));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddHealthChecks();

            builder.Services.AddOpenApi();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "jcAP", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    options.IncludeXmlComments(xmlPath);
                }
            });

            // register generic and specific repositories
            builder.Services.AddScoped(typeof(jcAP.API.Repositories.Common.IRepository<>), typeof(jcAP.API.Repositories.Common.EfRepository<>));
            builder.Services.AddScoped<IToolRepository, ToolRepository>();

            // forwarded headers config
            builder.Services.Configure<ForwardedHeadersOptions>(opts =>
            {
                opts.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            builder.Services.Configure<Microsoft.AspNetCore.Routing.RouteOptions>(opts =>
            {
                opts.LowercaseUrls = true;
                opts.LowercaseQueryStrings = false;
            });

            var app = builder.Build();

            app.UseForwardedHeaders();

            app.UseMiddleware<ExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseExceptionHandler("/error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.MapHealthChecks("/health");
            app.MapControllers();

            app.Run();
        }
    }
}
