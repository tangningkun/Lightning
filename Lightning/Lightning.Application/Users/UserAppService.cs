using Lightning.Application.Users.dto;
using Lightning.Core.AutoMapper;
using Lightning.Core.Encryption;
using Lightning.Core.ResultInfo;
using Lightning.Domain.Entities;
using Lightning.EntityFramework.Repositories.UserRepositories;
using Lightning.EntityFramework.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lightning.Application.Users
{
    public class UserAppService :IUserAppService
    {
        private readonly IUserRepository _userRepository;
        public UserAppService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="dto">登录信息</param>
        /// <returns></returns>
        public async Task<MessageDto<UserDto>> CheckLoginUserInfo(LoginUserDto dto)
        {
            try
            {
                var checkCount = await _userRepository.CountAsync(d => d.UserName == dto.UserName);
                if (checkCount == 0)
                    return new MessageDto<UserDto>
                    {
                        code = 201,
                        message = "该账户名不存在!",
                        result = "Fail"
                    };
                //var password = Encryptor.Md5Hash(dto.Password.Trim()).ToString();
                var count = await _userRepository.CountAsync(d => d.UserName == dto.UserName && d.Password == dto.Password);
                if (count != 0)
                {
                    var query = await _userRepository.FirstOrDefaultAsync(d => d.UserName == dto.UserName && d.Password == dto.Password);
                    return new MessageDto<UserDto>
                    {
                        code = 200,
                        message = "登录成功!",
                        result = "Success",
                        data = query.MapTo<User, UserDto>()
                    };
                }
                return new MessageDto<UserDto>
                {
                    code = 201,
                    message = "账户名或密码有误!",
                    result = "Fail"
                };
            }
            catch (Exception e)
            {
                return new MessageDto<UserDto>
                {
                    code = 202,
                    message = e.Message.ToString(),
                    result = "Fail"
                };
            }
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="dto">注册用户信息</param>
        /// <returns></returns>
        public async Task<MessageDto> RegisterUserInfo(RegisterUserDto dto)
        {
            //dto.Password = Encryptor.Md5Hash(dto.Password.Trim());
            var entity = new User
            {
                UserName = dto.UserName,
                Password = dto.Password,
                Id = Guid.NewGuid(),
                CreateTime = DateTime.Now,
                DepartmentId= Guid.NewGuid(),
                IsDeleted = true
            };
            try
            {
                var count = await _userRepository.CountAsync(d => d.UserName == entity.UserName);
                if (count != 0) return new MessageDto
                {
                    code = 201,
                    message = "该用户名已存在，请从新输入!",
                    result = "Fail"
                };
                await _userRepository.InsertAsync(entity);
                var result = new MessageDto
                {
                    code = 200,
                    message = "注册用户成功!",
                    result = "Success"
                };
                return result;
            }
            catch (Exception e)
            {
                var result = new MessageDto
                {
                    code = 202,
                    message = e.Message.ToString(),
                    result = "Fail"
                };
                return result;
            }
        }
    }
}
