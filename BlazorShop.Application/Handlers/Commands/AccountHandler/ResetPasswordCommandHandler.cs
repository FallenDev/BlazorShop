﻿namespace BlazorShop.Application.Handlers.Commands.AccountHandler
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, RequestResponse>
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<ResetPasswordCommandHandler> _logger;

        public ResetPasswordCommandHandler(IAccountService accountService, ILogger<ResetPasswordCommandHandler> logger)
        {
            _accountService = accountService;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<RequestResponse> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _accountService.ResetPasswordUserAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ErrorsManager.ResetPasswordCommand);
                return RequestResponse.Failure($"{ErrorsManager.ResetPasswordCommand}. {ex.Message}. {ex.InnerException?.Message}");
            }
        }
    }
}
