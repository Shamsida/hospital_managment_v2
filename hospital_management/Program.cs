using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Stripe;
using hospital_management.DAL.Data;
using hospital_management.BLL.Services;
using hospital_management.BLL.Services.Interface;
//using NETCore.MailKit.Core;
using hospital_management.DAL.Models;
using AutoMapper;
using hospital_management.IL;
using hospital_management.IL.Mapper;

namespace hospital_management
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // For Entity Framework
            var connectionString = builder.Configuration.GetConnectionString("Connection") ?? throw new InvalidOperationException("Connection string 'Connection' not found.");
            builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(connectionString));

            // For Identity
            builder.Services.AddDefaultIdentity<IdentityUser>().AddDefaultTokenProviders()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DataContext>();

            //Add Config for Required Email
            builder.Services.Configure<IdentityOptions>(
                opts => opts.SignIn.RequireConfirmedEmail = true);

            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            // Add services to the container.
            builder.Services.AddControllers();
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

            //Add Email Configs
            var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
            builder.Services.AddSingleton(emailConfig);

            builder.Services.AddScoped<IPatientService, PatientService>();
            builder.Services.AddScoped<IEmailService, EmailService>();

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