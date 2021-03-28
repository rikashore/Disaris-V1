using Disaris.Common.Cosmetics;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disaris.Modules
{
    public class General : BaseCommandModule
    {
        public DisarisBot _client { private get; set; }

        [Command("ping")]
        [Description("Use to check if the bot is alive, also provides latency")]
        public async Task Ping(CommandContext ctx)
        {
            var pingEmbed = new DiscordEmbedBuilder()
                .WithColor(DisarisCosmetics.DisarisColor)
                .WithTitle("Pong!")
                .WithDescription($"Disaris' current latency is `{_client.latency} ms`")
                .Build();

            await ctx.RespondAsync(embed: pingEmbed);
        }

        [Command("echo")]
        public async Task Echo(CommandContext ctx)
        {
            DiscordMessage echoMessage = ctx.Message.ReferencedMessage;

            if(echoMessage == null)
            {
                await ctx.RespondAsync("You either need to reply to a message or provide text to echo");
                return;
            }

            await ctx.RespondAsync(echoMessage.Content);
        }

        [Command("echo")]
        [Description("repeats what you say")]
        public async Task Echo(CommandContext ctx, [RemainingText]string echoText)
        {
            if(echoText == null)
            {
                await ctx.RespondAsync("you need to provide text to echo!");
                return;
            }

            await ctx.RespondAsync(echoText);
        }

        [Command("pin")]
        [Description("Pin any replied message\nReply to the message you want to pin")]
        public async Task Pin(CommandContext ctx)
        {
            var pinMessage = ctx.Message.ReferencedMessage;

            if (pinMessage == null)
            {
                await ctx.RespondAsync("Need to reply to a message to pin"); 
                return;
            }

            await pinMessage.PinAsync();
        }

        [Command("unpin")]
        [Description("unpin a message\nReply to the message you want to pin")]
        public async Task Unpin(CommandContext ctx)
        {
            var unpinMessage = ctx.Message.ReferencedMessage;

            if (unpinMessage == null)
            {
                await ctx.RespondAsync("Reply to the message you want to unpin!");
                return;
            }

            if (!unpinMessage.Pinned)
            {
                await ctx.RespondAsync("This message is not pinned!");
                return;
            }

            await unpinMessage.UnpinAsync();
        }

        [Command("purge")]
        [Description("Clear some messages")]
        [RequireGuild]
        [Aliases("cc", "clear")]
        public async Task Purge(CommandContext ctx, int amount = 2)
        {
            var messages = await ctx.Channel.GetMessagesBeforeAsync(ctx.Message.Id ,amount);
            await ctx.Channel.DeleteMessagesAsync(messages);
            await ctx.Message.DeleteAsync();
            var delMsg = await ctx.RespondAsync($"Deleted {amount} messages");

            await Task.Delay(1000);
            await delMsg.DeleteAsync();
        }
    }
}
