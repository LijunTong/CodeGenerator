using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Core.Event
{
    public static class EventAggregatorExt
    {
        public static void RegisterMessage(this IEventAggregator eventAggregator, Action<string> action)
        {
            eventAggregator.GetEvent<MessageEvent>().Subscribe(action);
        }

        public static void SendMessage(this IEventAggregator eventAggregator, string message)
        {
            eventAggregator.GetEvent<MessageEvent>().Publish(message);
        }

        public static void RegisterLoading(this IEventAggregator eventAggregator, Action<bool> action)
        {
            eventAggregator.GetEvent<LoadingEvent>().Subscribe(action);
        }

        public static void ToggleLoading(this IEventAggregator eventAggregator, bool isShow)
        {
            eventAggregator.GetEvent<LoadingEvent>().Publish(isShow);
        }
    }
}
