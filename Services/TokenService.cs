using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DashboardTienda.Models;
using System.IdentityModel.Tokens.Jwt;


namespace DashboardTienda.Services
{

    public class TokenService
    {
        private static TokenService _instance;
        public LogedUser User { get; private set; }

        public static TokenService Instance => _instance ??= new TokenService();

        public string Token { get; set; }

        private TokenService() { }

        public void DecodeToken()
        {
            if (string.IsNullOrEmpty(Token))
                return;

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(Token) as JwtSecurityToken;

            if (jsonToken != null)
            {
                User = new LogedUser
                {
                    id = jsonToken.Claims.First(claim => claim.Type == "sub").Value,
                    mail = jsonToken.Claims.First(claim => claim.Type == "mail").Value,
                    name = jsonToken.Claims.First(claim => claim.Type == "name").Value,
                    role = jsonToken.Claims.First(claim => claim.Type == "role").Value,
                };
                
            }
        }
    }
}
