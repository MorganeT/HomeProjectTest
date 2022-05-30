using Core;
using Core.ReviewsCollection.Services;
using Core.ReviewsTracking.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReviewsCollection.Services;

namespace ReviewsCollection
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
            services.AddScoped<ICollectReviewsOfProductService, CollectReviewsOfProductService>();
            services.AddScoped<IHtmlService, HtmlService>();
            services.AddScoped<IProductReviewService, ProductReviewService>();
            services.AddScoped<INotificationService, NotificationService>();

            services.AddDbContext<ReviewsTrackingContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Reviews")));

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}