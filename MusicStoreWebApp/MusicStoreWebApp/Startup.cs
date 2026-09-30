using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MusicStore.Api;
using MusicStore.Api.Responses;
using Newtonsoft.Json.Serialization;

namespace MusicStoreWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //services.AddSingleton<IConfiguration>(Configuration); /////

            services.AddScoped<IEmployeeApiMethods>(p => new EmployeeApiMethods(Configuration["MusicStoreUrl"]));
            services.AddScoped<ICustomerApiMethods>(p => new CustomerApiMethods(Configuration["MusicStoreUrl"]));
            services.AddScoped<IInvoiceApiMethods>(p => new InvoiceApiMethods(Configuration["MusicStoreUrl"]));
            services.AddScoped<ITrackApiMethods>(p => new TrackApiMethods(Configuration["MusicStoreUrl"]));
            services.AddScoped<IAlbumApiMethods>(p => new AlbumApiMethods(Configuration["MusicStoreUrl"]));
            services.AddScoped<IArtistApiMethods>(p => new ArtistApiMethods(Configuration["MusicStoreUrl"]));
            services.AddScoped<IGenreApiMethods>(p => new GenreApiMethods(Configuration["MusicStoreUrl"]));
            services.AddScoped<IMediaTypeApiMethods>(p => new MediaTypeApiMethods(Configuration["MusicStoreUrl"]));
            services.AddScoped<IPlaylistApiMethods>(p => new PlaylistApiMethods(Configuration["MusicStoreUrl"]));


            services.AddHttpClient();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options =>
            options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            services.AddKendo();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=employee}/{action=Index}");
            });
        }
    }
}
