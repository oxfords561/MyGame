using System;

namespace QF.GraphDesigner
{
    public interface IEventManager
    {
        void Signal(Action<object> obj);
    }
}