﻿namespace BlazorShop.Application.Handlers.Commands.AccountHandler
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, JwtTokenResponse>
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<LoginCommandHandler> _logger;

        public LoginCommandHandler(IAccountService accountService, ILogger<LoginCommandHandler> logger)
        {
            _accountService = accountService;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<JwtTokenResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _accountService.LoginAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error when the user tried to log in");
                return JwtTokenResponse.Error(new Exception("There was an error when the user tried to log in", ex));
            }
        }
    }
}
