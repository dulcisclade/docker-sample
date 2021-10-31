using Amazon.S3;
using Amazon.SimpleNotificationService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace AwsWebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //'ProfilesLocation' for default location for profile.
            services.AddDefaultAWSOptions(Configuration.GetAWSOptions())
                .AddAWSService<IAmazonS3>()
                .AddAWSService<IAmazonSimpleNotificationService>();

            // options.Filters.Add(typeof(HttpCustomExceptionFilter));
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .SetIsOriginAllowed((host) => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
                
            services.AddSwaggerGen(options =>
            {
                options.UseInlineDefinitionsForEnums();
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "AwsWebApi - HTTP API",
                    Version = "v1",
                    Description = "AwsWebApi  Service HTTP API"
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(setup =>
            {
                setup.SwaggerEndpoint("/swagger/v1/swagger.json", "AwsWebApi V1");
            });

            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}