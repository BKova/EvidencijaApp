using Microsoft.IdentityModel.Tokens;

namespace Evidencija.Auth
{
    public class TokenAuthenticationOptions
    {
        public TokenAuthenticationOptions(string Audience, string Issuer, SigningCredentials SigningCredentials)
        {
            this.Audience = Audience;
            this.Issuer = Issuer;
            this.SigningCredentials = SigningCredentials;
        }

        public string Audience { get; }
        public string Issuer { get; }
        public SigningCredentials SigningCredentials { get; }
    }
}