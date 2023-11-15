using System;

namespace SmartContextMenu
{
    [Serializable]
    public class EventArgs<T> : EventArgs
    {
        public T Entity { get; }

        public EventArgs(T entity)
        {
            Entity = entity;
        }
    }
}
