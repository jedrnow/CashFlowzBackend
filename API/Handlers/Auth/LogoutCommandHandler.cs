using AutoMapper;
using CashFlowzBackend.API.Commands;
using CashFlowzBackend.Infrastructure.Repositories;
using MediatR;
using CashFlowzBackend.Data.Models;
using CashFlowzBackend.Data.Models.Dtos;
using CashFlowzBackend.Data.Models.Input;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CashFlowzBackend.Infrastructure.Constants;
using CashFlowzBackend.Data.Models.View;
using NuGet.Common;

namespace CashFlowzBackend.API.Handlers
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public LogoutCommandHandler(IUserRepository userRepository, IConfiguration configuration, IMapper mapper)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<bool> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            if (Startup.RevokedTokens.Contains(request.Token))
            {
                throw new Exception("Token already revoked");
            }

            Startup.RevokedTokens.Add(request.Token);

            return true;
        }
    }
}
