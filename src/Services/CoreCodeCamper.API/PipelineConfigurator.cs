using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.AspNetCore;
using System.Globalization;

namespace CoreCodeCamper.API
{
    public class PipelineConfigurator
    {
        private readonly IApplicationBuilder app;
        private readonly IHostingEnvironment env;
        private readonly IServiceCollection services;
        private readonly IConfiguration config;

        public PipelineConfigurator(
            IApplicationBuilder app, 
            IHostingEnvironment env,
            IServiceCollection services,
            IConfiguration config)
        {
            this.app = app;
            this.env = env;
            this.services = services;
            this.config = config;
        }
        
        public void Configure()
        {            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(SwaggerConfig.Current.Endpoint, $"{SwaggerConfig.Current.Title} {SwaggerConfig.Current.Version}");
            });

            var cultureInfo = CultureInfo.GetCultureInfo(AppConfig.Current.DefaultCulture);
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
           
            app.UseDiagnosticPage(new DiagnosticPageOptions {
                Environment = env,
                Services = services,
                Configuration = config
            }, 
            (nameof(AppConfig), AppConfig.Current), (nameof(SwaggerConfig), SwaggerConfig.Current));

            app.UseMvc();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
        }
    }
}
