using Entity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebPulsaciones.Config;
using WebPulsaciones.Models;

namespace WebPulsaciones
{
    public class JwtService
    {
        private readonly AppSetting _appSettings;
        public JwtService(IOptions<AppSetting> appSettings) => _appSettings = appSettings.Value;
        public LoginViewModel GenerateToken(User userLogIn)
        {
            var userResponse = new LoginViewModel() { FirstName = userLogIn.FirstName, LastName = userLogIn.LastName, Username = userLogIn.UserName, Role = userLogIn.Role };
            var signingCredentials = GetSigningCredentials();
            var claims = GetClaims(userLogIn);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            userResponse.Token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return userResponse;
        }
        private List<Claim> GetClaims(User userLogIn)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userLogIn.UserName.ToString()),
                new Claim(ClaimTypes.Email, userLogIn.Email.ToString()),
                new Claim(ClaimTypes.MobilePhone, userLogIn.MobilePhone.ToString()),
                new Claim(ClaimTypes.Role, userLogIn.Role)
            };

            return claims;
        }
        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_appSettings.Secret);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
                issuer: _appSettings.ValidIssuer,
                audience: _appSettings.ValidAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_appSettings.ExpiryInMinutes),
                signingCredentials: signingCredentials);

            return tokenOptions;
        }
    }
}