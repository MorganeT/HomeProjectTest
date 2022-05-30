using Core.ReviewsCollection.Entities;
using Core.ReviewsTracking.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Core
{
    public class ReviewsTrackingContext : DbContext
    {
        public ReviewsTrackingContext()
        {

        }

        private static readonly ILoggerFactory _loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddFilter(
                (category, level) =>
                    category == DbLoggerCategory.Database.Command.Name
                    && level == LogLevel.Information);

            builder.AddConsole();
            builder.AddDebug();
        });


        public virtual DbSet<ProductTracking> ProductTrackings { get; set; }

        public virtual DbSet<ProductReview> ProductReviews { get; set; }

        public ReviewsTrackingContext(DbContextOptions<ReviewsTrackingContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(_loggerFactory)
                .EnableSensitiveDataLogging();

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReviewsTrackingContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
