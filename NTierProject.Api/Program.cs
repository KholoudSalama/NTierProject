
using NTierProject.Dal.Data;
using Microsoft.EntityFrameworkCore;
using NTierProject.Shared.BaseEntities;
using Microsoft.AspNetCore.Identity;
using NTierProject.Dal.UnitOfWorks;
using NTierProject.Bll.Interfaces;
using NTierProject.Bll.Services;
using NTierProject.Bll.Profile;

namespace NTierProject.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            // Add services to the container.
            builder.Services.AddIdentity<AppUser, IdentityRole>(optient => { optient.Password.RequireDigit = true;
                optient.Password.RequiredLength = 6;
            }).AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<ICompanyServices, CompanyServices>();
            builder.Services.AddAutoMapper(cdfg => cdfg.AddProfile<MappingProfile>());

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
              // app.MapOpenApi();
              app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
