using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreUlitity.Systems;
using CoreUlitity.Security;
using CoreUlitity.DateTimeExt;

namespace JwtSwaggerExt.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private RsaKey GetRsaKey()
        {
            RsaKey rsaKey = RsaCrypt.GenerateRsaKeys();// 生成RSA密钥对
            return rsaKey;
        }

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

        /// <summary>
        /// 产生唯一有序短id
        /// </summary>
        /// <returns></returns>
        [Route("CreateId")]
        [HttpGet]
        public List<string> CreateId()
        {
            var set = new HashSet<string>();
            for (int i = 0; i < 100; i++)
            {
                set.Add(SnowFlake.GetInstance().GetUniqueId());
            }

            return set.ToList();
        }

        /// <summary>
        /// 获取日期信息
        /// </summary>
        /// <returns></returns>
        [Route("GetCalendar")]
        [HttpGet]
        public object GetCalendar(string date="2021-01-01")
        {
            ChineseCalendar today = new ChineseCalendar(DateTime.Parse(date));
            return  new
            {
                ChineseDateString = today.ChineseDateString,
                AnimalString = today.AnimalString,
                GanZhiDateString = today.GanZhiDateString,
                DateHoliday = today.DateHoliday,
            };
        }

        [Route("GetRsaEncrypt")]
        [HttpGet]
        public object GetRsaEncrypt(string encryptStr="123456")
        {
            var md5 = encryptStr.MDString();
            var aes = encryptStr.AESEncrypt();
            var des = encryptStr.DesEncrypt();
            var hashEncod = encryptStr.HashEncoding();


            string encrypt = encryptStr.RSAEncrypt(GetRsaKey().PublicKey);// 公钥加密

            return new
            {
                rsaEncrypt = encrypt,
                md5 = md5,
                aes = aes,
                des = des,
                hashEncod = hashEncod
            };
        }

        [Route("GetRsaDecrypt")]
        [HttpGet]
        public object GetRsaDecrypt(string encryptStr)
        {
            string decrypt = encryptStr.RSADecrypt(GetRsaKey().PrivateKey);// 私钥解密

            return new
            {
                decrypt = decrypt
            };
        }

        /// <summary>
        /// 获取目录
        /// </summary>
        /// <returns></returns>
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
