using System;
using System.Collections.Generic;
using System.Threading;

using static Klyukay.KTools.TypeTools;

namespace Klyukay.KTools
{
    
    public class EventBus : IEventBus
    {

        private readonly SynchronizationContext _context;
        private readonly Dictionary<string, SubscriberCollection> _subscribersByTypeName = new Dictionary<string, SubscriberCollection>();

        public EventBus(SynchronizationContext context)
        {
            _context = context;
        }
        
        public void Publish<TEvent>() where TEvent : struct, IEvent => Publish(new TEvent());

        public void Publish<TEvent>(TEvent e) where TEvent : struct, IEvent
        {
            _context.Send(_ =>
            {
                var eventTypeName = GetFullName(typeof(TEvent));
                if (!_subscribersByTypeName.TryGetValue(eventTypeName, out var collection))
                {
                    return;
                }
            
                ((SubscriberCollection<TEvent>)collection).Invoke(e);
            }, null);
        }
        
        public void SubscribeTo<TEvent>(Action<TEvent> subscriber) where TEvent : struct, IEvent
        {
            _context.Send(_ =>
            {
                var eventTypeName = GetFullName(typeof(TEvent));
                if (!_subscribersByTypeName.TryGetValue(eventTypeName, out var collection))
                {
                    collection = new SubscriberCollection<TEvent>();
                    _subscribersByTypeName[eventTypeName] = collection;
                }

                ((SubscriberCollection<TEvent>) collection).Subscribers += subscriber;
            }, null);
        }

        public void UnsubscribeFrom<TEvent>(Action<TEvent> subscriber) where TEvent : struct, IEvent
        {
            _context.Send(_ =>
            {
                var eventTypeName = GetFullName(typeof(TEvent));
                if (!_subscribersByTypeName.TryGetValue(eventTypeName, out var collection))
                {
                    return;
                }

                ((SubscriberCollection<TEvent>) collection).Subscribers -= subscriber;
            }, null);
        }
        
        private abstract class SubscriberCollection { }
        
        private class SubscriberCollection<TEvent> : SubscriberCollection where TEvent : struct, IEvent
        {

            private readonly Queue<int> _freeIndexes = new Queue<int>();
            private readonly List<Action<TEvent>> _delegates = new List<Action<TEvent>>();
            private readonly List<Action<TEvent>> _workList = new List<Action<TEvent>>();
            private readonly Queue<TEvent> _eventsQueue = new Queue<TEvent>();
            
            private bool _onWork = false;
            
            public event Action<TEvent> Subscribers
            {
                add
                {
                    if (_freeIndexes.Count > 0)
                    {
                        int index = _freeIndexes.Dequeue();
                        _delegates[index] = value;
                    }
                    else
                    {
                        _delegates.Add(value);
                    }
                }
                remove
                {
                    int index = _delegates.IndexOf(value);
                    if (index < 0) return;
                    
                    _delegates[index] = null;
                    _freeIndexes.Enqueue(index);
                }
            }

            public void Invoke(TEvent e)
            {
                if (_onWork)
                {
                    _eventsQueue.Enqueue(e);
                    return;
                }

                _onWork = true;
                TEvent workEvent = e;

                while (_onWork)
                {
                    _workList.AddRange(_delegates);

                    for (int i = 0; i < _workList.Count; ++i)
                    {
                        _workList[i]?.Invoke(workEvent);
                    }

                    _workList.Clear();

                    if (_eventsQueue.Count > 0)
                    {
                        workEvent = _eventsQueue.Dequeue();
                    }
                    else
                    {
                        _onWork = false;
                    }
                }
            }
        }

    }
    
}