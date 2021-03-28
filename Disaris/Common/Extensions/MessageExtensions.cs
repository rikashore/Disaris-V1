using Disaris.Common.Cosmetics;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disaris.Common.Extensions
{
    public static class MessageExtensions
    {
        public static async Task<DiscordMessage> SendCarbonCodeAsync(this DiscordChannel channel, string title, string url)
        {
            var embed = new DiscordEmbedBuilder()
                .WithColor(DisarisCosmetics.DisarisColor)
                .WithTitle(title)
                .WithDescription("You can modify the snippet to your liking by following the link!")
                .WithUrl(url)
                .WithFooter("snippets made by carbon.")
                .Build();

            var message = await channel.SendMessageAsync(embed: embed);
            return message;
        }

        public static async Task<DiscordMessage> SendEmbedAsync(this DiscordChannel channel, DiscordEmbed embed)
        {
            var embedMsg = await channel.SendMessageAsync(embed: embed);

            return embedMsg;
        }

        public static async Task<DiscordMessage> SendEmbedAsync(this DiscordChannel channel, Func<DiscordEmbed> embedMethod)
        {
            var embed = embedMethod();
            var embedMsg = await channel.SendMessageAsync(embed: embed);

            return embedMsg;
        }

        public static async Task<DiscordMessage> SendErrorAsync(this DiscordChannel channel, string title, string content)
        {
            var errEmbed = new DiscordEmbedBuilder()
                .WithColor(DisarisCosmetics.DisarisColor)
                .WithTitle(title)
                .WithDescription(content)
                .Build();

            var errMsg = await channel.SendMessageAsync(errEmbed);

            return errMsg;
        }
    }
}
