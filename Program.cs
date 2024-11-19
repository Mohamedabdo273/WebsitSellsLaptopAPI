
using Microsoft.EntityFrameworkCore;
using WebsitSellsLaptop.Data;
using WebsitSellsLaptop.Repository.IRepository;
using WebsitSellsLaptop.Repository;
using Microsoft.AspNetCore.Identity;
using WebsitSellsLaptop.Models;
using Stripe;
using WebsitSellsLaptop.Utility;

namespace WebsitSellsLaptop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddDbContext<ApplicationDbContext>(
                option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                );
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
           .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddScoped<ICategory, CategoryRepository>();
            builder.Services.AddScoped<IProduct, ProductRepository>();
            builder.Services.AddScoped<IContactUs, ContactUsRepository>();
            builder.Services.AddScoped<IProductImg, ProductImgRepository>();
            builder.Services.AddScoped<ICard, CardRepository>();
            builder.Services.AddScoped<IOrder, OrederRepository>();
            builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
            StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
