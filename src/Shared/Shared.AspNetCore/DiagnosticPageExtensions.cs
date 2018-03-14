using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;


namespace Shared.AspNetCore
{
    public class DiagnosticPageConfig : TypeConfig<DiagnosticPageConfig>
    {
        protected override string SectionName => "DiagnosticPage";

        public string DefaultPath { get; set; } = "/diagnostic";
        public bool ShowHostingEnvironment { get; set; } = true;
        public bool ShowActions { get; set; } = true;
        public bool ShowCulture { get; set; } = true;
        public bool ShowServices { get; set; } = true;
        public bool ShowConfiguration { get; set; } = true;
    }

    public class DiagnosticPageOptions
    {
        public IHostingEnvironment Environment { get; set; }
        public IServiceCollection Services { get; set; }
        public IConfiguration Configuration { get; set; }
        public DiagnosticPageConfig DiagnosticPageConfig { get; set; } = DiagnosticPageConfig.CurrentOrNew;
    }

    public static class DiagnosticPageExtensions
    {
        private static void AddHostingEnvironment(DiagnosticPageOptions options, Dictionary<string, object> result)
        {
            if (!options.DiagnosticPageConfig.ShowHostingEnvironment)
            {
                return;
            }            
            if (options.Environment == null)
            {
                throw new ArgumentException("Environment value is not supplied.");
            }
            result["HostingEnvironment"] = options.Environment;
        }


        private static void AddActions(DiagnosticPageOptions options, IApplicationBuilder app, Dictionary<string, object> result)
        {
            if (!options.DiagnosticPageConfig.ShowActions)
            {
                return;
            }                        
            result["Actions"] = app.ApplicationServices
                .GetRequiredService<IActionDescriptorCollectionProvider>()
                .ActionDescriptors
                .Items
                .Select(item => new { name = item.DisplayName, item.AttributeRouteInfo.Template });
        }

        private static void AddCulture(DiagnosticPageOptions options, Dictionary<string, object> result)
        {
            if (!options.DiagnosticPageConfig.ShowCulture)
            {
                return;
            }
            result["Culture"] = new { CultureInfo.CurrentCulture, CultureInfo.CurrentUICulture };
        }

        private static void AddServices(DiagnosticPageOptions options, Dictionary<string, object> result)
        {
            if (!options.DiagnosticPageConfig.ShowServices)
            {
                return;
            }
            if (options.Services == null)
            {
                throw new ArgumentException("Services value is not supplied.");
            }
            result["Services"] = options.Services.Select(item => new
                {
                    lifetime = item.Lifetime.ToString(),
                    type = item.ServiceType.FullName,
                    implementation = item.ImplementationType?.FullName
                });
        }

        private static void AddConfiguration(DiagnosticPageOptions options, Dictionary<string, object> result)
        {
            if (!options.DiagnosticPageConfig.ShowConfiguration)
            {
                return;
            }
            if (options.Configuration == null)
            {
                throw new ArgumentException("Services value is not supplied.");
            }
            result["Configuration"] = options.Configuration.AsEnumerable();
        }

        public static IApplicationBuilder UseDiagnosticPage(
            this IApplicationBuilder app,
            DiagnosticPageOptions options,
            params (string key, object value)[] extra)
        {
            app.Map(options.DiagnosticPageConfig.DefaultPath, builder => builder.Run(async context =>
            {
                context.Response.ContentType = "application/json";
                var result = new Dictionary<string, object>();
                foreach (var (key, value) in extra)
                {
                    result[key] = value;
                }
                AddHostingEnvironment(options, result);
                AddActions(options, app, result);
                AddCulture(options, result);
                AddServices(options, result);
                AddConfiguration(options, result);

                await context.Response.WriteAsync(JsonConvert.SerializeObject(result, Formatting.Indented));
            }));
            return app;
        }
    }
}
