namespace Pingo;

public sealed class Status
{
    public Edition Edition { get; init; }

    public bool IsLegacy { get; init; }
}

public enum Edition
{
    Java,
    Bedrock
}