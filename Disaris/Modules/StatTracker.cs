using Disaris.Common.Cosmetics;
using Disaris.Common.Extensions;
using Disaris.Handlers;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Disaris.Modules
{
    public class StatTracker : BaseCommandModule
    {
        public Configuration _configuration { private get; set; }
        public HttpClient _httpclient { private get; set; }

        [Command("stats")]
        [Description("Grab the stats of an Apex Legends player\nUsage: ds stats [PC/PS4/X1] [username]")]
        [Aliases("stat")]
        public async Task PlayerStats(CommandContext ctx, string platform,[RemainingText] string username)
        {
            await ctx.TriggerTypingAsync();

            string result;
            try
            {
                result = await _httpclient.GetStringAsync($"https://api.mozambiquehe.re/bridge?version=5&platform={platform}&player={username}&auth={_configuration.ApiTrackerKey}");
            }
            catch (Exception)
            {
                await ctx.Channel.SendErrorAsync($"An error occured", "Couldn't fetch user data");
                return;
            }

            var stats = StatHandler.GetPlayerStats(result);
            var embeds = StatHandler.BuildStatEmbeds(stats);

            var statEmbed = embeds[0];
            var rankEmbed = embeds[1];

            await ctx.RespondAsync(statEmbed);
            await ctx.RespondAsync(rankEmbed);
        }
    }
}
