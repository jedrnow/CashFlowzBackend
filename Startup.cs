using Microsoft.EntityFrameworkCore;
using CashFlowzBackend.Data;
using System.Reflection;
using MediatR;
using FluentValidation;
using FluentValidation.AspNetCore;
using AutoMapper;
using CashFlowzBackend.Infrastructure.Services;
using CashFlowzBackend.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CashFlowzBackend.Infrastructure.Filters;

namespace CashFlowzBackend
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public static HashSet<string> RevokedTokens = new HashSet<string>();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CFDbContext>(options =>
                options.UseNpgsql(this.Configuration["Connnection:LocalConnectionString"]));

            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICheckUserService, CheckUserService>();
            services.AddScoped<IBudgetRepository, BudgetRepository>();
            services.AddScoped<ICheckBudgetService, CheckBudgetService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICheckCategoryService, CheckCategoryService>();

            services.AddMvc(options =>
            {
                options.Filters.Add<CustomExceptionFilterAttribute>();
            });


            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = this.Configuration["JwtSettings:Issuer"],
                    ValidAudience = this.Configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Configuration["JwtSettings:Key"]!)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });


            services.AddAuthorization();
            services.AddSwaggerGen();
            services.AddValidatorsFromAssemblyContaining<Startup>(); // register validators
            services.AddFluentValidationAutoValidation(); // the same old MVC pipeline behavior
            services.AddFluentValidationClientsideAdapters(); // for client side

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CashFlowzBackend v1"));
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.Use(async (context, next) =>
            {
                string token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                if (Startup.RevokedTokens.Contains(token))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Unathorized");
                    return;
                }

                await next.Invoke();
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
