namespace Pingo.Networking;

internal interface IIngoingPacket<out TSelf> where TSelf : IIngoingPacket<TSelf>
{
    public int Identifier { get; }

    public TSelf Read(MemoryReader reader);
}
