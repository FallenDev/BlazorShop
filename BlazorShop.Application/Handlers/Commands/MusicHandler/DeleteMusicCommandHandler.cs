﻿namespace BlazorShop.Application.Handlers.Commands.MusicHandler
{
    public class DeleteMusicCommandHandler : IRequestHandler<DeleteMusicCommand, RequestResponse>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<DeleteMusicCommandHandler> _logger;

        public DeleteMusicCommandHandler(IApplicationDbContext dbContext, ILogger<DeleteMusicCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<RequestResponse> Handle(DeleteMusicCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = _dbContext.Musics.FirstOrDefault(x => x.Id == request.Id);
                if (entity == null) throw new Exception("The entity does not exists");

                _dbContext.Musics.Remove(entity);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return RequestResponse.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error deleting the music");
                return RequestResponse.Error(new Exception("There was an error deleting the music", ex));
            }
        }
    }
}
