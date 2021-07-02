using Battle.API.Extensions;
using Battle.Infrastructure;
using Battle.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Battle.API
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
            services.AddDbContext<BattleAppContext>(
                options => options
                            //.UseLazyLoadingProxies()
                            //.EnableSensitiveDataLogging()
                            //.UseSqlServer(Configuration.GetConnectionString("BattleAppConnection"))
                            .UseInMemoryDatabase(databaseName: "BattleAppData")
                            );
            services.AddBattleInfrastructureService();
            services.AddControllers()
                .AddNewtonsoftJson(
                    options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc(Configuration["SwaggerGenOptions:name"],
                        new OpenApiInfo()
                        {
                            Title = Configuration["SwaggerGenOptions:title"],
                            Version = Configuration["SwaggerGenOptions:version"]
                        });
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwaggerForBattleApplication(
                options => Configuration.Bind("SwaggerOptions", options));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
