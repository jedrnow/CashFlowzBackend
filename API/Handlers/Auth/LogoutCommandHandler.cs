using CashFlowzBackend.API.Commands;
using MediatR;

namespace CashFlowzBackend.API.Handlers
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, bool>
    {

        public LogoutCommandHandler()
        {
        }

        public async Task<bool> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            if (Startup.RevokedTokens.Contains(request.Token))
            {
                return false;
            }

            // Add the token to the blacklist
            Startup.RevokedTokens.Add(request.Token);

            return true;
        }
    }
}
