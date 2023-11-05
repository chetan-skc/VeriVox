using FluentAssertions.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VeriVox.Business;
using VeriVox.Business.Interfaces;
using VeriVox.Database.Context;
using VeriVox.Database.DataSeeding;
using VeriVox.Host;
using VeriVox.Repository;
using VeriVox.Repository.Access_DB;
using VeriVox.Repository.Interfaces;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.




        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();



        DependencyInjectionConfig.Configure(builder.Services, builder.Configuration);
        builder.Services.AddDbContext<CFA_DbContext>(options =>
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("VeriVoxConnectionString")));


        builder.Services.AddAuthentication("Bearer")
         .AddScheme<AuthenticationSchemeOptions, JwtAuthenticationHandler>("Bearer",
                                                                           options => { });
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("CreateCompany", policy =>
            {
                policy.RequireClaim("Role", "1", "3"); // User with role 1,3 can only create company
            });

            options.AddPolicy("ViewCompany", policy =>
            {
                policy.RequireClaim("Role", "1", "2", "3", "4", "5", "6");
            });

            options.AddPolicy("DeleteForm", policy =>
            {
                policy.RequireClaim("Role", "1", "3", "5");
            });

            options.AddPolicy("UpdateForm", policy =>
            {
                policy.RequireClaim("Role", "1", "3", "5");
            });
            options.AddPolicy("AddContact", policy =>
            {
                policy.RequireClaim("Role", "1", "3", "5");
            });
            options.AddPolicy("ViewContacts", policy =>
            {
                policy.RequireClaim("Role", "1","2", "3","4", "5","6");
            });
        });

        var buder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

        IConfiguration configuration = buder.Build();


        var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: MyAllowSpecificOrigins,
              policy =>
              {
                  policy.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod();
              });
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }





        app.UseHttpsRedirection();
        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.UseCors(MyAllowSpecificOrigins);

        app.Run();
    }
}