using System;

namespace Vip.ElginTEF.Extensions
{
    internal static class EventHandlerExtensions
    {
        public static void Raise(this EventHandler eventHandler, object sender, EventArgs e)
        {
            eventHandler?.Invoke(sender, e);
        }

        public static void Raise(this EventHandler eventHandler, EventArgs e)
        {
            eventHandler?.Invoke(null, e);
        }

        public static void Raise<T>(this EventHandler<T> eventHandler, object sender, T e) where T : EventArgs
        {
            eventHandler?.Invoke(sender, e);
        }

        public static void Raise<T>(this EventHandler<T> eventHandler, T e) where T : EventArgs
        {
            eventHandler?.Invoke(null, e);
        }

        public static void Raise(this EventHandler<EventArgs> eventHandler, object sender)
        {
            eventHandler?.Invoke(sender, EventArgs.Empty);
        }

        public static void Raise(this EventHandler<EventArgs> eventHandler)
        {
            eventHandler?.Invoke(null, EventArgs.Empty);
        }

        public static void Raise(this EventHandler eventHandler, object sender)
        {
            eventHandler?.Invoke(sender, EventArgs.Empty);
        }

        public static void Raise(this EventHandler eventHandler)
        {
            eventHandler?.Invoke(null, EventArgs.Empty);
        }
    }
}