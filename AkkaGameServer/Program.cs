using AkkaGameServer;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

// Register all the services needed for the application to run
IServiceCollection collection = new ServiceCollection();
collection.RegisterDependencies();

var serviceProvider = collection.BuildServiceProvider();
var gameServer = serviceProvider.GetRequiredService<GameServer>();

// Start servers
gameServer.SetupDotNetty();
gameServer.SetupWebSocket();
gameServer.SetupAkka();

Console.WriteLine("Server started. Press any key to exit.");
Console.ReadKey();