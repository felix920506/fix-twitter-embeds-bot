using DSharpPlus;

namespace fixTwitterEmbedsBotCS;

internal class Program {
    private static async Task Main(String[] args) {

        //
        // Setup Client
        //

        var token = await File.ReadAllTextAsync("token.txt");

        var clientConfig = new DiscordConfiguration {
            Token = token,
            TokenType = TokenType.Bot,
            Intents = DiscordIntents.All
        };

        var discordClient = new DiscordClient(clientConfig);

        //
        // Bind Event Handlers
        //

        discordClient.MessageCreated += MessageHandler.messageCreateEventHandler;

        //
        // Launch Client
        //

        await discordClient.ConnectAsync();
        await Task.Delay(-1);
        
    }
}