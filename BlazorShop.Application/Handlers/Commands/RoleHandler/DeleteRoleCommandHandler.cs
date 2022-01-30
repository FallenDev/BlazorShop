﻿namespace BlazorShop.Application.Handlers.Commands.RoleHandler
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, RequestResponse>
    {
        private readonly IRoleService _roleService;
        private readonly ILogger<DeleteRoleCommandHandler> _logger;

        public DeleteRoleCommandHandler(IRoleService roleService, ILogger<DeleteRoleCommandHandler> logger)
        {
            _roleService = roleService;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<RequestResponse> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _roleService.DeleteRoleAsync(request.Id);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ErrorsManager.DeleteRoleCommand);
                return RequestResponse.Failure($"{ErrorsManager.DeleteRoleCommand}. {ex.Message}. {ex.InnerException?.Message}");
            }
        }
    }
}
