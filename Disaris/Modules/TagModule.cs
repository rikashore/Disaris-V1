using Disaris.Common.Cosmetics;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DisarisInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disaris.Modules
{
    public class TagModule : BaseCommandModule
    {
        public Tags tags { private get; set; }

        [Command("tag")]
        [Description("grab a tag")]
        [RequireGuild]
        public async Task Tag(CommandContext ctx, string tagName)
        {
            var tag = await tags.GetTag(tagName);

            if (tag == null)
            {
                await ctx.RespondAsync("That tag does not exist!");
                return;
            }

            await ctx.RespondAsync(tag.Content);
        }

        [Command("deltag")]
        [Description("deletes a tag")]
        [Aliases("delt")]
        [RequireGuild]
        public async Task TagDelete(CommandContext ctx, string tagName)
        {
            var tagResult = await tags.DeleteTag(tagName);

            if(tagResult == 0)
            {
                await ctx.RespondAsync($"Tag {tagName} does not exist");
                return;
            }

            await ctx.RespondAsync($"Tag {tagName} was successfully deleted");
        }
    }
}
