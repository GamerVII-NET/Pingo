namespace Pingo.Status;

public sealed class BedrockStatus : StatusBase
{
    public required string Message { get; set; }

    public long Server { get; set; }

    public long Time { get; set; }
}