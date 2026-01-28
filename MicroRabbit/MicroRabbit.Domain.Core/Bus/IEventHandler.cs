using MicroRabbit.Domain.Core.Events;

namespace MicroRabbit.Domain.Core.Bus
{
    public interface IEventHandler<in TEnvent> : IEventHandler
        where TEnvent : Event
    {
        Task Handle(TEnvent @event);
    }

    public interface IEventHandler
    {
    }
}
