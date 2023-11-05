using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VeriVox.Business;
using VeriVox.Business.Interfaces;
using VeriVox.Core.Messages;
using VeriVox.Host.Mapping;
using VeriVox.Repository;
using VeriVox.Repository.Access_DB;
using VeriVox.Repository.Interfaces;
using static System.Net.WebRequestMethods;

namespace VeriVox.Host
{
    public class DependencyInjectionConfig
    {
        
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IFormRepository, FormRepository>();
            services.AddScoped<IFormService, FormService>();
            services.AddAutoMapper(typeof(AutomapperProfile));
            services.AddHttpContextAccessor();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ILinkService, LinkService>();
            services.AddScoped<ILinkRepository, LinkRepository>();
            services.AddScoped<ResponsesAnswersServices>();
            services.AddScoped<ResponsesAnswersRepository>();
            services.AddScoped<UserMessages>();
            services.AddScoped<JwtAuthenticationHandler>();
        }
    }
}
