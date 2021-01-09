using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace CoreUlitity.configer
{
    public class ConfigManager
    {
        public IConfiguration Configuration { get; set; }

        public ConfigManager()
        {
            var path = Directory.GetCurrentDirectory();
            Configuration = new ConfigurationBuilder().SetBasePath(path).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).AddJsonFile("appsettings.Development.json", optional: true)
             .Build();
        }

        public static ConfigManager Build => new ConfigManager();

        public SwaggerConfig SwaggerConfig => GetSwaggerSetting();

        private SwaggerConfig GetSwaggerSetting()
        {
            SwaggerConfig swaggerConfig = new SwaggerConfig();
            IConfigurationSection section = Configuration.GetSection("Conn:Setting:Swagger");
            swaggerConfig.Enable = Convert.ToBoolean(section["Enable"]);
            swaggerConfig.SwaggerName = section["SwaggerName"];
            swaggerConfig.SwaggerDesc = section["SwaggerDesc"];
            swaggerConfig.Version = section["Version"];
            swaggerConfig.XmlAssmblyName = section["XmlAssmblyName"];
            swaggerConfig.Authorization = Convert.ToBoolean(section["Authorization"]);
            swaggerConfig.Issuer = section["Issuer"];
            swaggerConfig.Audience = section["Audience"];
            swaggerConfig.JwtSecret = section["JwtSecret"];
            return swaggerConfig;
        }
    }
}
