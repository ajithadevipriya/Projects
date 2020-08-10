using WooliesTest.Services.External.Entity;

namespace WooliesTest.Services
{
    public class TokenService : ITokenService
    {
        public UserToken GetToken()
        {
            return new UserToken
            {
                Name = "Devi Priya",
                Token = "1234-455662-22233333-3333"
            };
        }

    }
}