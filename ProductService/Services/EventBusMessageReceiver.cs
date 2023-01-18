using System.Text.Json;
using Azure.Messaging.ServiceBus;
using MediatR;
using ProductService.Commands;
using ProductService.Configurations;
using ProductService.Events;

namespace ProductService.Services
{
    public class EventBusMessageReceiver
    {
        private readonly EventBusConnection connection;
        private readonly IMediator mediator;

        public EventBusMessageReceiver(EventBusConnection connection, IMediator mediator)
        {
            this.connection = connection;
            this.mediator = mediator;
        }

        public async void receiveMessagesAsync()
        {
            ServiceBusProcessor processor = connection.processor;
            processor.ProcessMessageAsync += MessageHandler;
            processor.ProcessErrorAsync += ErrorHandler;
            await processor.StartProcessingAsync();
        }

        async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();
            var orderStartedEvent = JsonSerializer.Deserialize<OrderStartedIntegrationEvent>(body);
            var orderCommand = new OrderStartedIntegrationEventCommand(orderStartedEvent);
            var validationResult = await mediator.Send(orderCommand);
            await this.connection.sender.SendMessageAsync(new ServiceBusMessage(JsonSerializer.Serialize(validationResult)));
            await args.CompleteMessageAsync(args.Message);
        }

        // handle any errors when receiving messages
        Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }
    }
}
