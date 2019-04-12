using Abp.AutoMapper;
using Lightning.Application.AppServices;
using Lightning.Application.AppServices.dto;
using Lightning.Application.Users.dto;
using Lightning.Core.Encryption;
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

        public async Task<MessageDto<UserDto>> CheckLoginUserInfo(LoginUserDto dto)
        {
            try
            {
                var count1 = _userRepository.Count();
                var checkCount = await _userRepository.CountAsync(d => d.UserName == dto.UserName);
                if (checkCount == 0)
                    return new MessageDto<UserDto>
                    {
                        code = 201,
                        message = "该账户名不存在!",
                        result = "Fail"
                    };
                var password = Encryptor.Md5Hash(dto.Password.Trim()).ToString();
                var count = await _userRepository.CountAsync(d => d.UserName == dto.UserName && d.Password == password);
                if (count != 0)
                {
                    var query = await _userRepository.FirstOrDefaultAsync(d => d.UserName == dto.UserName && d.Password == password);
                    return new MessageDto<UserDto>
                    {
                        code = 200,
                        message = "登录成功!",
                        result = "Success",
                        data = query.MapTo<UserDto>()
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

        public async Task<MessageDto> RegisterUserInfo(RegisterUserDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
