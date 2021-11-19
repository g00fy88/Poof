using System;

namespace Pulse
{
    public interface IPulse
    {
        void Connect(ISensor sensor);

        void Send(ISignal signal);
    }
}
