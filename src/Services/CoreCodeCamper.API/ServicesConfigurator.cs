using CoreCodeCamper.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shared.Common;
using Swashbuckle.AspNetCore.Swagger;

namespace CoreCodeCamper.API
{
    public class ServicesConfigurator
    {
        private readonly IServiceCollection services;
        private readonly IConfiguration config;
        private readonly ILoggerFactory loggerFactory;

        public ServicesConfigurator(
            IServiceCollection services, 
            IConfiguration config)
        {
            this.services = services;
            this.config = config;

            var serviceProvider = services.BuildServiceProvider();
            this.loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
        }

        public void Configure()
        {
            TypeConfig.CreateAll(config);

            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(SwaggerConfig.Current.Version,
                    new Info
                    {
                        Title = SwaggerConfig.Current.Title,
                        Version = SwaggerConfig.Current.Version
                    }
                );
            });
            services.AddDbContext<CodeCamperDataContext>(c =>
            {
                try
                {
                    // Requires LocalDB which can be installed with SQL Server Express 2016
                    // https://www.microsoft.com/en-us/download/details.aspx?id=54284
                    //c.UseSqlServer(Configuration.GetConnectionString("CatalogConnection"));
                    c.UseLoggerFactory(loggerFactory);
                    c.UseSqlServer(@"Data Source=VEDRAN-PC\SQLEXPRESS2017;Initial Catalog=CoreCodeCamper;Integrated Security=True;");

                }
                catch (System.Exception ex)
                {
                    var message = ex.Message;
                }
            });
        }
    }
}
