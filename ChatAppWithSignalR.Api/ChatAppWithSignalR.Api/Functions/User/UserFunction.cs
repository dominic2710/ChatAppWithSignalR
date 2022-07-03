using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace ChatAppWithSignalR.Api.Functions.User
{
    public class UserFunction : IUserFunction
    {
        private readonly ChatAppContext _chatAppContext;

        public UserFunction(ChatAppContext chatAppContext)
        {
            _chatAppContext = chatAppContext;
        }

        public User? Authenticate(string loginId, string password)
        {
            try
            {
                var entity = _chatAppContext.TblUsers.SingleOrDefault(x=>x.LoginId == loginId);
                if (entity == null) return null;

                var isPasswordMatched = VertifyPassword (password, entity.StoredSalt, entity.Password);

                if (!isPasswordMatched ) return null;

                var token = GenerateJwtToken(entity);

                return new User
                {
                    Id = entity.Id,
                    UserName = entity.UserName,
                    Token = token
                };

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public User GetUserById(int id)
        {
            throw new NotImplementedException();
            //var entity = _chatAppContext.TblUsers
            //    .Where(x => x.Id == id)
            //    .FirstOrDefault();

            //if (entity == null) return new User();

            //var awayDuration = entity.IsOnline ? "" : Utilities.CalcAwayDuration(entity.LastLogonTime);
            //return new User
            //{
            //    UserName = entity.UserName,
            //    Id = entity.Id,
            //    AvatarSourceName = entity.AvatarSourceName,
            //    IsAway = awayDuration != "" ? true : false,
            //    AwayDuration = awayDuration,
            //    IsOnline = entity.IsOnline,
            //    LastLogonTime = entity.LastLogonTime
            //};
        }

        
        private bool VertifyPassword(string enteredPassword, byte[] storedSalt, string storedPassword)
        {
            string encryptyedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: enteredPassword,
                salt: storedSalt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return encryptyedPassword.Equals(storedPassword);
        }

        private string GenerateJwtToken(TblUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("1234567890123456");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials (
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}
