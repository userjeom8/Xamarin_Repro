using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompucareWard.API.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Reflection;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Pages;
using CompucareWard.API.Services;

namespace CompucareWard.API
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
            var connectionString = Configuration.GetConnectionString("ConnectionString");
            var identityString = Configuration.GetConnectionString("IdentityString");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddIdentityServerAuthentication(o =>
            {
                o.ApiName = "WardApi";
                o.ApiSecret = "NrxJqEne%3CgvejQTMvmaM*MX&zRn89jtjTxxtcUCNx7%BDR62&x$RHSGd24";
                o.Authority = identityString;
                o.RequireHttpsMetadata = false;                
            });
            //.AddJwtBearer(o =>
            //{
            //    o.Authority = identityString;
            //    o.Audience = "CompucareWardApi";
            //    o.RequireHttpsMetadata = false;
            //    o.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidIssuer = identityString,
            //        ValidateAudience = false,
            //        ValidAudience = "CompucareWardApi",
            //        ValidateLifetime = true,
            //    };
            //});            
            services.AddScoped<IGlobalSettingsService, GlobalSettingsService>();
            services.AddScoped<IInpatientBookingServiceInternal, InpatientBookingServiceInternal>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);               

            services.AddDbContext<CompucareWardContext>(options =>
            {
                options.UseSqlServer(connectionString,
                                     sqlServerOptionsAction: sqlOptions =>
                                     {
                                         sqlOptions.UseRowNumberForPaging(true);
                                         sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                                         //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                                         sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                     });

                // Changing default behavior when client evaluation occurs to throw. 
                // Default in EF Core would be to log a warning when client evaluation is performed.
                options.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
                //Check Client vs. Server evaluation: https://docs.microsoft.com/en-us/ef/core/querying/client-eval
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();
            //app.UseCors(
            //    builder => builder
            //    .AllowAnyOrigin()
            //    .AllowAnyMethod()
            //    .AllowAnyHeader()
            //    .AllowCredentials()
            //);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseAuthentication();            
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseMvc();
        }        
    }
}
