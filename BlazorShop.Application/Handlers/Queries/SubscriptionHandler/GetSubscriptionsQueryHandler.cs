﻿namespace BlazorShop.Application.Handlers.Queries.SubscriptionHandler
{
    public class GetSubscriptionsQueryHandler : IRequestHandler<GetSubscriptionsQuery, Result<SubscriptionResponse>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<GetSubscriptionsQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetSubscriptionsQueryHandler(IApplicationDbContext dbContext, ILogger<GetSubscriptionsQueryHandler> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        public Task<Result<SubscriptionResponse>> Handle(GetSubscriptionsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = _dbContext.Subscriptions
                    .ProjectTo<SubscriptionResponse>(_mapper.ConfigurationProvider)
                    .ToList();

                return Task.FromResult(new Result<SubscriptionResponse>
                {
                    Successful = true,
                    Items = result ?? new List<SubscriptionResponse>()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error while getting the subscriptions");
                return Task.FromResult(new Result<SubscriptionResponse>
                {
                    Error = $"There was an error while getting the subscriptions. {ex.Message}. {ex.InnerException?.Message}"
                });
            }
        }
    }
}
