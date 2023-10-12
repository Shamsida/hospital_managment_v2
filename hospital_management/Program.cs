using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using hospital_management.Data;
namespace hospital_management
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // For Entity Framework
            var connectionString = builder.Configuration.GetConnectionString("DataContextConnection") ?? throw new InvalidOperationException("Connection string 'DataContextConnection' not found.");
            builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(connectionString));

            // For Identity
            builder.Services.AddDefaultIdentity<IdentityUser>().AddDefaultTokenProviders()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DataContext>();

            //Add Config for Required Email
            builder.Services.Configure<IdentityOptions>(
                opts => opts.SignIn.RequireConfirmedEmail = true);

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
                        app.UseAuthentication();;

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}