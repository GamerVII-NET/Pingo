﻿using Pingo;
using Pingo.Status;

var options = new MinecraftPingOptions
{
    Address = "127.0.0.1",
    Port = 25565
};

var status = await Minecraft.PingAsync(options);

if (status is BedrockStatus bedrock)
{
    Console.WriteLine(string.Join(", ", bedrock.MessagesOfTheDay));
}
else
{
    var java = (JavaStatus?) status;
    Console.WriteLine(string.Join(", ", java!.MessagesOfTheDay));
}
