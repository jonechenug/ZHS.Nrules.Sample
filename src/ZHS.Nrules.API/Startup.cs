using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NRules.RuleModel;
using Swashbuckle.AspNetCore.Swagger;
using ZHS.Nrules.Application;
using ZHS.Nrules.Application.Service;
using ZHS.Nrules.API.Repository.Promotion;
using ZHS.Nrules.Infrastructure.Repository;
using ZHS.Nrules.Infrastructure.Repository.Orders;
using ZHS.Nrules.Infrastructure.RuleEngine;
using ZHS.Nrules.RuleEngine;

namespace ZHS.Nrules.API
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
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IBasePromotionRepository, PromotionRepository>();

            services.AddTransient<IExecuterContainer, NulesExecuterContainer>();
            services.AddTransient<IExecuterRepository, ExecuterRepository>();

            services.AddTransient<OrderService>();
            services.AddTransient<PromotionService>();
            services.AddTransient<RuleEngineService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseMvc();
        }
    }
}
