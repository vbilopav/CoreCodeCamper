using Shared.Common;

namespace CoreCodeCamper.API
{
    public class AppConfig : TypeConfig<AppConfig>
    {
        protected override string SectionName => "App";

        public string DefaultCulture { get; set; } = "en-US";
    }

    public class SwaggerConfig : TypeConfig<SwaggerConfig>
    {
        protected override string SectionName => "Swagger";

        public string Title { get; set; } = "CoreCodeCamper API";
        public string Version { get; set; } = "v1";
        public string Endpoint { get; set; } = $"/swagger/v1/swagger.json";
    }
}
