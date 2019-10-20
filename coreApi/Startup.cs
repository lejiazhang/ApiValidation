using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientValidationGenerator.AspNetCore.Configuration;
using coreApi.DependencyResolution;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StructureMap;

namespace coreApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            ClientValidationGeneratorConfiguration.AddClientValidationGenerator(services);

            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            return ConfigureIoC(services); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }

        public IServiceProvider ConfigureIoC(IServiceCollection services)
        {
            var container = IoC.DependencyResolution.IoC.Initialize(new DefaultRegistry());

            container.AssertConfigurationIsValid();

            //Populate the container using the service collection

            container.Populate(services);

            return container.GetInstance<IServiceProvider>();
        }
    }
}
