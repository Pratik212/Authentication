using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Helpers
{
    /// <summary>
    /// AppSettingsServiceExtension
    /// </summary>
    public static class AppSettingsServiceExtension
    {
         /// <summary>
         /// AddAppSettingsService
         /// </summary>
         /// <param name="services"></param>
         /// <param name="config"></param>
         /// <returns></returns>
         public static IServiceCollection AddAppSettingsService(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<Settings>(options =>
            {
                options.Environment = config.GetSection("AppSettings:Environment").Value;
                options.AdminUrl = config.GetSection("AppSettings:AdminUrl").Value;
                options.Secret = config.GetSection("AppSettings:Secret").Value;
                options.DocumentsPath = config.GetSection("AppSettings:DocumentsPath").Value;
                options.EmailTemplatesPath = config.GetSection("AppSettings:EmailTemplatesPath").Value;
                options.ProfilePicturePath = config.GetSection("AppSettings:ProfilePicturePath").Value;
                options.BehaviorPicturePath = config.GetSection("AppSettings:BehaviorPicturePath").Value;
                options.MediaPath = config.GetSection("AppSettings:MediaPath").Value;
                options.StorePicturePath = config.GetSection("AppSettings:StorePicturePath").Value;
                options.ProductPicturePath = config.GetSection("AppSettings:ProductPicturePath").Value;
                
            });
            return services;
        }
    }
}