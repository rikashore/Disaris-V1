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
    public class RotationTracker : BaseCommandModule
    {
        public Configuration _configuration { private get; set; }
        public HttpClient _httpclient { private get; set; }

        [Command("rotation")]
        [Description("Check the current map rotation for Apex Legends")]
        [Aliases("map-rotation")]
        public async Task MapRotation(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();

            string result;
            try
            {
                result = await _httpclient.GetStringAsync($"https://api.mozambiquehe.re/maprotation?auth={_configuration.ApiTrackerKey}");
            }
            catch (Exception)
            {
                await ctx.Channel.SendErrorAsync("An error occurred", "Couldn't fetch data");
                return;
            }

            var rotation = RotationHandler.GetRotation(result);
            var rotationEmbed = RotationHandler.BuildRotationEmbed(rotation);

            await ctx.RespondAsync(rotationEmbed);
        }
    }
}
