using CartApi.Data;
using Common.Messaging;
using MassTransit;

namespace CartApi.Messaging.Consumers
{
    public class OrderCompletedEventConsumer : IConsumer<OrderCompletedEvent>
    {
        private readonly ICartRepository _repository;
        private readonly ILogger<OrderCompletedEventConsumer> _logger;
        public OrderCompletedEventConsumer(ICartRepository cartRepository, ILogger<OrderCompletedEventConsumer> logger)
        {
            _logger = logger;
            _repository = cartRepository;
        }
        //Consume method is automatically called when message is in bus
        public async Task Consume(ConsumeContext<OrderCompletedEvent> context)
        {
            _logger.LogWarning("We are in consume method now...");
            _logger.LogWarning("BuyerId: " + context.Message.BuyerId);
            await _repository.DeleteCartAsync(context.Message.BuyerId);
        }
    }
}

