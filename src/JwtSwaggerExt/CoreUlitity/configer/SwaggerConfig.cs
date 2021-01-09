
namespace CoreUlitity.configer
{
    public class SwaggerConfig
    {
        public string SwaggerName { get; set; }

        public string Title { get; set; }

        /// <summary>
        /// Swagger描述
        /// </summary>
        public string SwaggerDesc { get; set; }

        public bool Enable { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// xml名称
        /// </summary>
        public string XmlAssmblyName { get; set; }

        /// <summary>
        /// 是否启用授权
        /// </summary>
        public bool Authorization { get; set; }

        /// <summary>
        /// JWT的密钥
        /// </summary>
        public string JwtSecret { get; set; }

        /// <summary>
        /// 发行人
        /// </summary>
        public string Issuer { get; set; }

        public string Audience { get; set; }

    }
}
