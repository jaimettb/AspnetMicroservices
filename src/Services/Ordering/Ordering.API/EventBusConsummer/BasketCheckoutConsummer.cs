using EventBus.Messages.Events;
using MassTransit;
using System.Threading.Tasks;

namespace Ordering.API.EventBusConsummer
{
    public class BasketCheckoutConsummer : IConsumer<BasketCheckoutEvent>
    {
        public Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            throw new System.NotImplementedException();
        }
    }
}
