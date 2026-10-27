using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Cooker.JwtBearer
{
    public class JwtHelper
    {
        public static string CreateToken(TokenModel tokenModel, params Claim[] claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenModel.SecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //.NET Core 的 JwtSecurityToken类承担实际创建token的任务
            /**
             **   Claims (Payload)
             **   Claims 部分包含了一些跟这个 token 有关的重要信息。 JWT 标准规定了一些字段，下面节选一些字段:
             **   iss: The issuer of the token，token 是给谁的  发送者
             **   audience: 接收的
             **   sub: The subject of the token，token 主题
             **   exp: Expiration Time。 token 过期时间，Unix 时间戳格式
             **   iat: Issued At。 token 创建时间， Unix 时间戳格式
             **   jti: JWT ID。针对当前 token 的唯一标识
             **   除了规定的字段外，可以包含其他任何 JSON 兼容的字段。
             **/
            claims.Append(new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64));
            claims.Append(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")));
            var token = new JwtSecurityToken(
                issuer: tokenModel.Issuer,
                audience: tokenModel.Audience,
                expires: DateTime.Now.AddMinutes(tokenModel.Expires),
                signingCredentials: creds,
                claims: claims
                );
            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            return accessToken;
        }
    }
}