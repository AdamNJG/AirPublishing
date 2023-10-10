using ImageStorage.ImageVerification.Repository;
using ImageStorage.ImageVerification.Service;
using ImageStorage.ImageVerification.Service.interfaces;
using ImageStorage.ImageVerification.Storage;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ImageStorage.Application
{
    public class Startup
    {
        private IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var assembly = Assembly.Load("ImageStorage.ImageVerification");

            services.AddControllers().PartManager.ApplicationParts.Add(new AssemblyPart(assembly));
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddScoped<IImageUploadService, ImageUploadService>();
            services.AddScoped<IImageUploadRepository, ImageUploadRepository>();

            if (_configuration.GetValue<bool>("StoreLocal"))
            {
                services.AddScoped<IImageStorage, LocalImageStorage>();
            }
            else
            {
                services.AddScoped<IImageStorage, ImageBlobStorage>();
            }

            services.AddDbContext<ImageUploadContext>(options =>
            {
                options.UseInMemoryDatabase("ImageUploadStore");
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:3000") // this is obviously just for testing
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
