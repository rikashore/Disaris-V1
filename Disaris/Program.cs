using Disaris.Common.Cosmetics;
using Disaris.Modules;
using DisarisInfrastructure;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Disaris
{
    class Program
    {
        private static void WriteDisarisInfo()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(DisarisCosmetics.DisarisAscii);
            Console.WriteLine($"\nVersion: 0.1.0\nAuthor: Aarav Navani\n");
            Console.ForegroundColor = default;
        }

        static void Main(string[] args)
        {
            WriteDisarisInfo();
            var disaris = new DisarisBot();
            disaris.RunAsync().GetAwaiter().GetResult();
        }
    }

    public class DisarisBot
    {
        public readonly EventId BotEventId = new EventId(11, "Disaris");
        public DiscordClient Client { get; private set; }
        public int latency => Client.Ping;
        public InteractivityExtension Interactivity { get; private set; }
        public ServiceCollection Services { get; private set; }
        public CommandsNextExtension Commands { get; private set; }

        private readonly Configuration configuration = new Configuration();

        public async Task RunAsync()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console(theme: SystemConsoleTheme.Grayscale)
                .CreateLogger();

            var logFactory = new LoggerFactory().AddSerilog();
            var config = new DiscordConfiguration
            {
                Token = configuration.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = LogLevel.Debug,
                LoggerFactory = logFactory,
                Intents = DiscordIntents.All
            };

            Client = new DiscordClient(config);

            Client.Ready += OnClientReady;
            Client.MessageCreated += OnMessage;

            Client.UseInteractivity(new InteractivityConfiguration
            {
                Timeout = TimeSpan.FromMinutes(5)
            });

            var services = new ServiceCollection()
                .AddSingleton(this)
                .AddDbContext<DisarisContext>()
                .AddSingleton<Tags>()
                .AddSingleton<HttpClient>()
                .AddSingleton(configuration)
                .BuildServiceProvider();


            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { configuration.Prefix },
                EnableMentionPrefix = true,
                EnableDms = false,
                Services = services
            };

            Commands = Client.UseCommandsNext(commandsConfig);

            Commands.SetHelpFormatter<DisarisHelp>();

            Commands.CommandErrored += OnError;

            Commands.RegisterCommands<General>();
            Commands.RegisterCommands<TagModule>();
            Commands.RegisterCommands<AddModule>();
            Commands.RegisterCommands<Carbon>();
            Commands.RegisterCommands<DnD>();
            Commands.RegisterCommands<StatTracker>();
            Commands.RegisterCommands<RotationTracker>();

            await Client.ConnectAsync();

            await Task.Delay(-1);

        }

        private Task OnMessage(DiscordClient sender, MessageCreateEventArgs e)
        {
            _ = Task.Run(async () =>
            {
                string[] words = { "disaris", "i", "need", "help" };
                string[] negation = { "dont", "do not", "dont" };
                var msg = e.Message.Content.ToLower();

                if (words.All(x => msg.Contains(x.ToLower())) && !negation.Any(msg.Contains))
                {
                    var helpContext = Commands.CreateFakeContext(sender.CurrentUser, e.Channel, e.Message.Content, "ds ", Commands.RegisteredCommands["help"] );
                    await Commands.ExecuteCommandAsync(helpContext);
                }
            });

            return Task.CompletedTask;
        }

        private Task OnError(CommandsNextExtension sender, CommandErrorEventArgs e)
        {
            e.Context.Client.Logger.LogError(BotEventId, $"{e.Context.User.Username} tried executing '{e.Command?.QualifiedName ?? "<unknown command>"}' but it errored: {e.Exception.GetType()}: {e.Exception.Message ?? "<no message>"}", DateTime.Now);

            return Task.CompletedTask;
        }

        private Task OnClientReady(DiscordClient sender, ReadyEventArgs e)
        {
            var activity = new DiscordActivity("ds help", ActivityType.Playing);
            Client.UpdateStatusAsync(activity);

            return Task.CompletedTask;
        }
    }
}
