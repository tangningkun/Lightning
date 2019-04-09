using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lightning.WebApi.Controllers
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {

        /// <summary>
        /// AAA
        /// </summary>
        /// <returns></returns>
        [HttpPost("AAA")]
        public Task<string> AAA()
        {
            return Task<string>.Run(() => "aaa");
        }
        /// <summary>
        /// BBB
        /// </summary>
        /// <returns></returns>
        [HttpPost("BBB")]
        public Task<string> BBB()
        {
            return Task<string>.Run(() => "aaa");
        }
    }
}