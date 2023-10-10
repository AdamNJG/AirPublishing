using ImageStorage.ImageVerification.Service;
using ImageStorage.ImageVerification.Service.interfaces;
using ImageStorage.Test.ImageVerification.mocks;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageStorage.Test.ImageVerification
{
    public static class ServiceConfiguration
    {
        public static ServiceProvider BuildServiceProvider()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IImageUploadService ,ImageUploadService>();
            services.AddSingleton<IImageStorage, ImageStorageMock>();
            services.AddSingleton<IImageUploadRepository, ImageUploadRepositoryMock>();

            return services.BuildServiceProvider();
        }
    }
}
