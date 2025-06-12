using NotificationService.Models;
using NotificationService.Services.Interfaces;

namespace NotificationService.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IMessageQueue _queue;
        private readonly IRateLimiter _rateLimiter;

        public NotificationService(IMessageQueue queue, IRateLimiter rateLimiter)
        {
            _queue = queue;
            _rateLimiter = rateLimiter;
        }

        public async Task SendAsync(CreateNotificationRequest request)
        {
            var allowed = await _rateLimiter.AllowAsync(request.Recipient);
            if (!allowed)
                throw new InvalidOperationException("Rate limit exceeded");

            await _queue.PublishAsync("notifications", request);
        }
    }
}
