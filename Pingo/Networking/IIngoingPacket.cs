namespace Pingo.Networking;

internal interface IIngoingPacket<out TSelf> where TSelf : IIngoingPacket<TSelf>
{
    public static abstract int Identifier { get; }

    public static abstract TSelf Read(MemoryReader reader);
}