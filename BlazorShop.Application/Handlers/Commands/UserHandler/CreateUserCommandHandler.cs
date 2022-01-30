﻿namespace BlazorShop.Application.Handlers.Commands.UserHandler
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, RequestResponse>
    {
        private readonly IUserService _userService;
        private readonly ILogger<CreateUserCommandHandler> _logger;

        public CreateUserCommandHandler(IUserService userService, ILogger<CreateUserCommandHandler> logger)
        {
            _userService = userService;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<RequestResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.CreateUserAsync(request);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ErrorsManager.CreateUserCommand);
                return RequestResponse.Failure($"{ErrorsManager.CreateUserCommand}. {ex.Message}. {ex.InnerException?.Message}");
            }
        }
    }
}
