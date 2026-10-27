using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Cooker.JwtBearer
{
    public sealed class TokenModel
    {
        private readonly IConfiguration _configuration;
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public int Expires { get; set; }

        public string SecurityKey { get; set; }
        public TokenModel()
        {

        }
        public TokenModel(IConfiguration configuration)
        {
            this._configuration = configuration;
            Init();
        }
        private void Init()
        {
            _configuration.GetSection("Jwt").Bind(this);
        }
        public string GetToken(params Claim[] claims)
        {
            return JwtHelper.CreateToken(this, claims);
        }
    }
}