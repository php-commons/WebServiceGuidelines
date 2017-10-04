using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PHP.WebServiceConcept.Domain;
using PHP.WebServiceConcept.Persistence;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;

namespace PHP.WebServiceConcept.AccountService
{
    public class Startup
    {
        private Container _container;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _container = new Container();            
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(_container));
            services.AddSingleton<IViewComponentActivator>(new SimpleInjectorViewComponentActivator(_container));

            services.EnableSimpleInjectorCrossWiring(_container);
            services.UseSimpleInjectorAspNetRequestScoping(_container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            _container.AddDomain(env.EnvironmentName);
            _container.AddPersistence(env.EnvironmentName);

            _container.RegisterMvcControllers(app);
            _container.RegisterMvcViewComponents(app);
                
            app.UseMvc();
        }
    }
}
