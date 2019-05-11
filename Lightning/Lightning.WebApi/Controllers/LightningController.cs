using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Lightning.Application.Departments;
using Lightning.Application.Departments.dto;
using Lightning.Application.Users;
using Lightning.Application.Users.dto;
using Lightning.Core.ListResult;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Lightning.WebApi.Application;
using Lightning.Core.Encryption;
using Microsoft.AspNetCore.Authorization;

namespace Lightning.WebApi.Controllers
{
    [EnableCors("LightningAny")]
    [Route("api/[controller]")]
    [ApiController]
    public class LightningController : ControllerBase
    {
        private readonly IDepartmentAppService _departmentAppService;
        private readonly IUserAppService _userAppService;
        /**读取appsettings配置文件节服务*/
        private readonly ApiConfigurtaionServices _apiConfigurtaion;
        public LightningController(IDepartmentAppService departmentAppService, IUserAppService userAppService,ApiConfigurtaionServices apiConfigurtaion)
        {
            _departmentAppService = departmentAppService;
            _userAppService = userAppService;
            _apiConfigurtaion = apiConfigurtaion;
        }
        [HttpPost("GetJsonWebToken")]
        public IActionResult GetJsonWebToken(LoginUserDto dto)
        {
            dto.Password=Encryptor.Md5Hash(dto.Password.Trim()).ToString();
            // 将用户名称推送到声明中，以便我们稍后识别用户。
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, dto.UserName)
            };
            //使用密钥对令牌进行签名。此秘密将在您的API和需要检查令牌是否合法的任何内容之间共享。
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_apiConfigurtaion.AppConfigurtaionValue("JwtSetting:SecurityKey")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //.NET Core’s JwtSecurityToken class takes on the heavy lifting and actually creates the token.
            /**
                * Claims (Payload)
                Claims 部分包含了一些跟这个 token 有关的重要信息。 JWT 标准规定了一些字段，下面节选一些字段:

                iss: The issuer of the token，token 是给谁的  发送者
                audience: 接收的
                sub: The subject of the token，token 主题
                exp: Expiration Time。 token 过期时间，Unix 时间戳格式
                iat: Issued At。 token 创建时间， Unix 时间戳格式
                jti: JWT ID。针对当前 token 的唯一标识
                除了规定的字段外，可以包含其他任何 JSON 兼容的字段。
                * */
            var token = new JwtSecurityToken(
                issuer: _apiConfigurtaion.AppConfigurtaionValue("JwtSetting:Issuer"),
                audience: _apiConfigurtaion.AppConfigurtaionValue("JwtSetting:Audience"),
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost("GetAllDepartment")]
        public async Task<List<DepartmentDto>> GetAllDepartment()
        {
            var result = await _departmentAppService.GetAllDepartment();
            return result;
        }
    }
}