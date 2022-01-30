﻿namespace BlazorShop.Application.Handlers.Commands.SubscriberHandler
{
    public class CreateSubscriberCommandHandler : IRequestHandler<CreateSubscriberCommand, RequestResponse>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<CreateSubscriberCommandHandler> _logger;
        private readonly IUserService _userService;

        public CreateSubscriberCommandHandler(IApplicationDbContext dbContext, ILogger<CreateSubscriberCommandHandler> logger, IUserService userService)
        {
            _dbContext = dbContext;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userService = userService;
        }

        public async Task<RequestResponse> Handle(CreateSubscriberCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Subscriber entity = _dbContext.Subscribers.FirstOrDefault(x => x.Id == request.Id);
                if (entity != null) throw new Exception("The entity already exists");

                var customer = await _userService.FindUserByIdAsync(request.CustomerId);
                var subscription = _dbContext.Subscriptions.FirstOrDefault(x => x.Id == request.SubscriptionId);

                entity = new Subscriber
                {
                    Status = SubscriptionStatus.Inactive,
                    CurrentPeriodEnd = request.CurrentPeriodEnd,
                    CurrentPeriodStart = request.DateStart,
                    DateStart = request.DateStart,
                    Customer = customer,
                    Subscription = subscription,
                    StripeSubscriberSubscriptionId = "",
                    HostedInvoiceUrl = "",
                };

                _dbContext.Subscribers.Add(entity);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return RequestResponse.Success(entity.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ErrorsManager.CreateSubscriberCommand);
                return RequestResponse.Failure($"{ErrorsManager.CreateSubscriberCommand}. {ex.Message}. {ex.InnerException?.Message}");
            }
        }
    }
}
