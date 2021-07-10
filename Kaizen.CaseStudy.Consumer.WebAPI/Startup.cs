using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kaizen.CaseStudy.Consumer.Core;
using Kaizen.CaseStudy.Consumer.Data;
using Kaizen.CaseStudy.Consumer.Data.Context;
using Kaizen.CaseStudy.Consumer.Services;
using Kaizen.CaseStudy.Consumer.Services.ConsumerAService;
using Kaizen.CaseStudy.Consumer.Services.ConsumerBService;
using Kaizen.CaseStudy.Consumer.WebAPI.Middlewares;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.CaseStudy.Consumer.WebAPI
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
            services.AddDbContext<DbContext,ConsumerAContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ConsumerAConnection"), x => x.MigrationsAssembly("Kaizen.CaseStudy.Consumer.Data")));

            services.AddDbContext<DbContext,ConsumerBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ConsumerBConnection"), x => x.MigrationsAssembly("Kaizen.CaseStudy.Consumer.Data")));

            services.AddDbContext<DbContext,ConsumerCContext>(options => options.UseOracle(Configuration.GetConnectionString("ConsumerCConnection"), x => x.MigrationsAssembly("Kaizen.CaseStudy.Consumer.Data")));

            services.AddScoped<IConsumerAService, ConsumerAService>();
            services.AddScoped<IConsumerBService, ConsumerBService>();
            services.AddScoped<IConsumerBService, ConsumerBService>();


            services.AddScoped<IUnitOfWork, UnitOfWork>();

         

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Kaizen.CaseStudy.Consumer.WebAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kaizen.CaseStudy.Consumer.WebAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
