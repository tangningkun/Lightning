using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.Token.TokenDto
{
    public class TokenMessages
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }

    }
}
