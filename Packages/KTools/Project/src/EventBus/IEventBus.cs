using System;

namespace Klyukay.KTools
{
    
    public interface IEventBus
    {

        void Publish<TEvent>() where TEvent : struct, IEvent;
        void Publish<TEvent>(TEvent e) where TEvent : struct, IEvent;

        void SubscribeTo<TEvent>(Action<TEvent> onBaseEnvironmentReady) where TEvent : struct, IEvent;
        void UnsubscribeFrom<TEvent>(Action<TEvent> onBaseEnvironmentReady) where TEvent : struct, IEvent;
    }
    
}