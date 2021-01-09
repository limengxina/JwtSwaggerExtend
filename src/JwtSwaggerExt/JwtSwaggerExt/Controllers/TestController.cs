using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtSwaggerExt.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        /// <summary>
        /// 需要授权的接口
        /// </summary>
        /// <returns></returns>
        [Route("GetWeather")]
        [HttpGet]
        public IEnumerable<WeatherForecast> GetWeather()
        {
            var rng = new Random();
            var list = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

            return list;
        }

        [Route("GetPath")]
        [HttpGet]
        public object GetPath()
        {
            // 获取和设置包括该应用程序的目录的名称。
            var path1 = AppDomain.CurrentDomain.BaseDirectory;
            // 获取程序的基目录。
            var path2 = AppContext.BaseDirectory;
            // 获取模块的完整路径，包含文件名
            var path3 = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            // 获取和设置当前目录(该进程从中启动的目录)的完全限定目录。
            var path4 = System.Environment.CurrentDirectory;
            // 获取应用程序的当前工作目录，注意工作目录是可以改变的，而不限定在程序所在目录。
            var path5 = System.IO.Directory.GetCurrentDirectory();
            // 获取和设置包括该应用程序的目录的名称。
            var path6 = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

            return new
            {
                path1 = path1,
                path2 = path2,
                path3 = path3,
                path4 = path4,
                path5 = path5,
                path6 = path6,
            };
        }

    }
}
