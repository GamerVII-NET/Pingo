# Pingo
A simple library to ping Minecraft: Java/Bedrock edition servers that supports almost all versions.

## Usage

```cs
var options = new MinecraftPingOptions
{
    Address = "127.0.0.1",
    Port = 25565
};

var status = await Minecraft.PingAsync(options);
```