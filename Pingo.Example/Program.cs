using Pingo;

var status = await Minecraft.PingAsync();

Console.WriteLine($"The server is {status.Edition}!");