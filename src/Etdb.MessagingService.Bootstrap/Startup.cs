using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Etdb.MessagingService.Bootstrap.Configuration;
using Etdb.ServiceBase.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Etdb.MessagingService.Bootstrap
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment hostingEnvironment;

        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            this.configuration = configuration;
            this.hostingEnvironment = hostingEnvironment;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
                {
                    options.EnableEndpointRouting = false;

                    var requireAuthenticatedUserPolicy = new AuthorizeFilter(new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build());

                    options.Filters.Add(requireAuthenticatedUserPolicy);
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            var identityServerConfiguration =
                this.configuration.GetSection(nameof(IdentityServerConfiguration))
                    .Get<IdentityServerConfiguration>();

            services.AddAuthentication("Bearer")
                .AddCookie("Etdb_MessagingService")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = identityServerConfiguration.Authority;
                    options.ApiName = ServiceNames.MessagingService;
                    options.RequireHttpsMetadata = this.hostingEnvironment.IsProduction();
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAuthentication()
                .UseRouting()
                .UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}