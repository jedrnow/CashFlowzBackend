using AutoMapper;
using CashFlowzBackend.API.Commands;
using CashFlowzBackend.Infrastructure.Repositories;
using MediatR;
using CashFlowzBackend.Data.Models;
using CashFlowzBackend.Data.Models.Dtos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CashFlowzBackend.Infrastructure.Constants;
using CashFlowzBackend.Data.Models.View;

namespace CashFlowzBackend.API.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResultDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public LoginCommandHandler(IUserRepository userRepository, IConfiguration configuration, IMapper mapper)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<LoginResultDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetUserByLogin(request.LoginInput.Login);

            ValidateUserExistsByLogin(user);

            CheckPassword(request.LoginInput.Password, user.PasswordHash);

            string token = GenerateJwtToken(user.Id,user.Email, user.Login);

            LoginResultViewModel result = new()
            {
                UserId = user.Id,
                Token = token
            };

            LoginResultDto mappedResult = _mapper.Map<LoginResultDto>(result);

            return mappedResult;
        }

        private static void ValidateUserExistsByLogin(User user)
        {
            if (user == null)
            {
                throw new KeyNotFoundException();
            }
        }

        private void CheckPassword(string givenPassword, string passwordHash)
        {
            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(givenPassword,passwordHash);

            if (!isPasswordCorrect)
            {
                throw new UnauthorizedAccessException("Wrong password");
            }
        }

        private string GenerateJwtToken(int userId, string email, string login)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]!);

            List<Claim> claims = new()
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Sub,email),
                new(JwtRegisteredClaimNames.Email,email),
                new("userId", userId.ToString())
            };

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(TimeSpan.FromMinutes(TokenSettings.DefaultExpireTimeInMinutes)),
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string jwt = tokenHandler.WriteToken(token);

            return jwt;
        }
    }
}
