using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MusicStore.Api;
using Newtonsoft.Json.Serialization;

namespace MusicStoreWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Lax;
            });

            var musicStoreUrl = builder.Configuration["MusicStoreUrl"];
            builder.Services.AddScoped<IEmployeeApiMethods>(_ => new EmployeeApiMethods(musicStoreUrl));
            builder.Services.AddScoped<ICustomerApiMethods>(_ => new CustomerApiMethods(musicStoreUrl));
            builder.Services.AddScoped<IInvoiceApiMethods>(_ => new InvoiceApiMethods(musicStoreUrl));
            builder.Services.AddScoped<ITrackApiMethods>(_ => new TrackApiMethods(musicStoreUrl));
            builder.Services.AddScoped<IAlbumApiMethods>(_ => new AlbumApiMethods(musicStoreUrl));
            builder.Services.AddScoped<IArtistApiMethods>(_ => new ArtistApiMethods(musicStoreUrl));
            builder.Services.AddScoped<IGenreApiMethods>(_ => new GenreApiMethods(musicStoreUrl));
            builder.Services.AddScoped<IMediaTypeApiMethods>(_ => new MediaTypeApiMethods(musicStoreUrl));
            builder.Services.AddScoped<IPlaylistApiMethods>(_ => new PlaylistApiMethods(musicStoreUrl));

            builder.Services.AddHttpClient();

            builder.Services
                .AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });

            builder.Services.AddKendo();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=employee}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
