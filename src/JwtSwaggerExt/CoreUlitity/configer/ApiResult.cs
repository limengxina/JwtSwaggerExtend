
namespace CoreUlitity.configer
{
    /// <summary>
    /// 请求错误提示类
    /// </summary>
    public class ApiResult
    {
        public bool Success { get; set; } = true;
        public string Msg { get; set; } = "";
        public string Type { get; set; } = "";
        public object Data { get; set; } = "";
        public object DataExt { get; set; } = "";
    }
}
