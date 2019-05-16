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
using Lightning.Core.Token.TokenDto;
using Lightning.WebApi.AuthHelper.OverWrite;

namespace Lightning.WebApi.Controllers
{
    //[EnableCors("LimitRequests")]
    [Route("api/[controller]")]
    [ApiController]
    public class LightningController : ControllerBase
    {
        private readonly IDepartmentAppService _departmentAppService;
        private readonly IUserAppService _userAppService;
        /**读取appsettings配置文件节服务*/
        private readonly ApiConfigurtaionServices _apiConfigurtaion;
        public LightningController(IDepartmentAppService departmentAppService, IUserAppService userAppService, ApiConfigurtaionServices apiConfigurtaion)
        {
            _departmentAppService = departmentAppService;
            _userAppService = userAppService;
            _apiConfigurtaion = apiConfigurtaion;
        }
        [HttpPost("GetJsonWebToken")]
        public async Task<object> GetJsonWebToken(LoginUserDto dto)
        {
            try
            {
                // 将用户名称推送到声明中，以便我们稍后识别用户。
                string jwtStr = string.Empty;
                bool suc = false;
                //这里就是用户登陆以后，通过数据库去调取数据，分配权限的操作
                //这里直接写死了


                if (string.IsNullOrEmpty(dto.UserName) || string.IsNullOrEmpty(dto.Password))
                {
                    return new JsonResult(new
                    {
                        code = "201",
                        status = false,
                        message = "用户名或密码不能为空",
                        token = ""
                    });
                }

                TokenModelJwt tokenModel = new TokenModelJwt();
                tokenModel.Uid = 1;
                tokenModel.Role = "Admin";

                jwtStr = JwtHelper.IssueJwt(tokenModel);


                return Ok(new
                {
                    code = "200",
                    status = true,
                    message = "生成成功",
                    token = jwtStr
                });
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    code = "202",
                    status = false,
                    message = e.Message,
                    token = ""
                });
            }

        }
        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <returns></returns>
        [Authorize]
        //[Authorize(Roles = "Admin")]
        [HttpPost("GetAllDepartment")]
        public async Task<object> GetAllDepartment()
        {
            var result = await _departmentAppService.GetAllDepartment();
            return Ok(new
            {
                data = result
            });
        }

        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <returns></returns>
        //[Authorize]
        //[Authorize(Roles = "Admin")]
        [HttpPost("GetAll")]
        public async Task<object> GetAll()
        {
            //var result = await _departmentAppService.GetAllDepartment();
            return Ok(new
            {
                data = "123"
            });
        }
    }
}