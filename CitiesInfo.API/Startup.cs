using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitiesInfo.API.Entities;
using CitiesInfo.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CitiesInfo.API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public static IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc();

            #if DEBUG
                services.AddTransient<IMailService, LocalMailService>();
            #else
                services.AddTransient<IMailService, CloudMailService>();
            #endif

            services.AddScoped<ICityInfoRepository, CityInfoRepository>();
            
            // var connectionString = @"Server=(localdb)\mssqllocaldb;Database=CityInfoDB;Trusted_Connection=True;";
            var connectionString = Configuration["connectionStrings:cityInfoDBConnectionString"];
            services.AddDbContext<CityInfoContext>(o => o.UseSqlServer(connectionString));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
            CityInfoContext cityInfoContext)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            cityInfoContext.EnsureSeedDataForContext();

            app.UseCors(builder =>
                builder.WithOrigins("http://localhost:4200")
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                );

            app.UseStatusCodePages();

            AutoMapper.Mapper.Initialize(cfg => {
                cfg.CreateMap<Entities.City, Models.CityWithoutPointsOfInterestDTO>();
                cfg.CreateMap<Entities.City, Models.CityDTO>();
                cfg.CreateMap<Entities.PointOfInterest, Models.PointOfInterestDTO>();
                cfg.CreateMap<Models.PointOfInterestForCreateDTO, Entities.PointOfInterest>();
                cfg.CreateMap<Models.PointOfInterestForUpdateDTO, Entities.PointOfInterest>();
                cfg.CreateMap<Entities.PointOfInterest, Models.PointOfInterestForUpdateDTO>();
            });

            app.UseMvc();

            // app.Run(async (context) =>
            // {
            //     await context.Response.WriteAsync("Hello World!");
            // });
        }
    }
}
