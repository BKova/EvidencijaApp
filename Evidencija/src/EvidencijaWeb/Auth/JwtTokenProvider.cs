using System;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;
using Evidencija.Database.Models;
using Microsoft.IdentityModel.Tokens;

namespace Evidencija.Auth
{
    public interface IToken
    {
        TokenData Create(User User, int ExpiresInMinutes);
    }

    public class JwtTokenProvider : IToken
    {
        private TokenAuthenticationOptions _tokenOptions;

        public JwtTokenProvider(TokenAuthenticationOptions TokenAuthOptions)
        {
            _tokenOptions = TokenAuthOptions;
        }

        public TokenData Create(User User, int ExpiresInMinutes)
        {
            var Claims = new Claim[] {
                new Claim("UserId", Convert.ToString(User.Id)),
                new Claim("IsAdmin", Convert.ToString(User.IsAdmin))
            };

            DateTime Expires = DateTime.UtcNow.AddMinutes(ExpiresInMinutes);

            var JwtTokenHandler = new JwtSecurityTokenHandler();

            var Token = JwtTokenHandler.CreateJwtSecurityToken(
                    issuer: _tokenOptions.Issuer,
                    audience: _tokenOptions.Audience,
                    signingCredentials: _tokenOptions.SigningCredentials,
                    subject: new ClaimsIdentity(new GenericIdentity(User.UserName, "TokenAuth"), Claims),
                    expires: Expires
                    );

            var Result = new TokenData(true, Token.RawData, Expires);

            return Result;
        }

    public ClaimsPrincipal Validate(string tokenData)
    {
        var TokenHandler = new JwtSecurityTokenHandler();

            SecurityToken Token;

            var Principal = TokenHandler.ValidateToken(tokenData, new TokenValidationParameters()
            {
                IssuerSigningKey = _tokenOptions.SigningCredentials.Key,

                ValidIssuer = _tokenOptions.Issuer,

                ValidAudience = _tokenOptions.Audience,

                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero,

            }, out Token);

            return Principal;
        }

}
    public class TokenData
    {
        public bool Authenticated { get; }

        public string Token { get; }

        public DateTime Expires { get; }

        public TokenData(bool Authenticated, string Token, DateTime Expires)
        {
            this.Authenticated = Authenticated;
            this.Token = Token;
            this.Expires = Expires;
        }
    }

}