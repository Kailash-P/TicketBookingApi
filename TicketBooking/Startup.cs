using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TicketBooking.BAL.Implementation;
using TicketBooking.BAL.Interface;
using TicketBooking.DAL;
using TicketBooking.DAL.Repositories.Implementation;
using TicketBooking.Models.Common;

namespace TicketBooking
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddDbContext<TicketContext>(options => options.UseNpgsql(Configuration.GetConnectionString("TicketDb")));
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    });
            });

            var azureAd = new AzureAdConfig();
            this.Configuration.Bind("AzureAd", azureAd);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = AzureADDefaults.BearerAuthenticationScheme;
                options.DefaultChallengeScheme = AzureADDefaults.BearerAuthenticationScheme;
            }).AddJwtBearer(AzureADDefaults.BearerAuthenticationScheme, options =>
            {
                options.Audience = $"https://{azureAd.Domain}/{azureAd.ClientId}";
                options.Authority = $"{azureAd.Instance}{azureAd.TenantId}";
            });

            services.AddAuthorization(options =>
            {
                var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
                    AzureADDefaults.BearerAuthenticationScheme);
                defaultAuthorizationPolicyBuilder =
                    defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
                options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ticket Booking Api", Version = "v1" });
                var authorizationUrl = $"{this.Configuration["AzureAd:Instance"]}{this.Configuration["AzureAd:TenantId"]}/oauth2/authorize";
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(authorizationUrl),
                        },
                    },
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" },
                        },
                        new List<string>()
                    },
                });
                c.CustomSchemaIds(x => x.FullName);
            });

            

            // Register Services

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IMovieRepository, MovieRepository>();

            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICityRepository, CityRepository>();

            services.AddScoped<IMultiplexService, MultiplexService>();
            services.AddScoped<IMultiplexRepository, MultiplexRepository>();

            services.AddScoped<IUserBookingService, UserBookingService>();
            services.AddScoped<IUserBookingRepository, UserBookingRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.Use(async (context, next) =>
            {
                var result = await context.AuthenticateAsync(AzureADDefaults.BearerAuthenticationScheme).ConfigureAwait(false);

                if (result?.Principal != null)
                {
                    context.User = result.Principal;
                }

                await next().ConfigureAwait(false);
            });
            app.UseRouting();
            app.UseCors("AllowAllOrigins");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ticket Booking V1");
                c.OAuth2RedirectUrl($"{this.Configuration["Swagger:RedirectUrl"]}/swagger/oauth2-redirect.html");
                c.OAuthClientId(this.Configuration["Swagger:ClientId"]);
                c.OAuthAdditionalQueryStringParams(new Dictionary<string, string> { { "resource", $"https://{this.Configuration["AzureAd:Domain"]}/{this.Configuration["AzureAd:ClientId"]}" } });
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
