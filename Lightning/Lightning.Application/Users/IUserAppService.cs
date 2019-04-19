using Lightning.Application.Users.dto;
using Lightning.Core.Dependency;
using Lightning.Core.ResultInfo;
using Lightning.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lightning.Application.Users
{
    public interface IUserAppService
    {
        Task<MessageDto<UserDto>> CheckLoginUserInfo(LoginUserDto dto);

        Task<MessageDto> RegisterUserInfo(RegisterUserDto dto);
    }
}
