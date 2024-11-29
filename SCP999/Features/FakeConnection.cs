using Mirror;
using System;

namespace SCP999;
public class FakeConnection : NetworkConnectionToClient
{
    public FakeConnection(int connectionId) : base(connectionId)
    {
    }

    public override string address => "localhost";

    public override void Send(ArraySegment<byte> segment, int channelId = 0)
    {
    }
}