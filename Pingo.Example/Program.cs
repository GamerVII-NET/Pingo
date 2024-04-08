using Pingo;
using Pingo.Status;

var options = new MinecraftPingOptions
{
    Address = "127.0.0.1",
    Port = 25565
};

var status = await Minecraft.PingAsync(options);

if (status is BedrockStatus bedrock)
{
    // This is a Minecraft: Bedrock edition server!
}
else
{
    var java = (JavaStatus?) status;
    // This is a Minecraft: Java edition server!
}