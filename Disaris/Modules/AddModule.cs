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
    [Group("add")]
    public class AddModule : BaseCommandModule
    {
        public Tags tags { private get; set; }

        [Command("tag")]
        [Description("Make a new tag")]
        [RequireGuild]
        public async Task TagAdd(CommandContext ctx, string tagName, [RemainingText] string tagContent)
        {
            var tag = await tags.GetTag(tagName);

            if (tag != null)
            {
                await ctx.RespondAsync("A tag with this name already exists");
                return;
            }

            await tags.AddTag(tagName, tagContent);

            await ctx.RespondAsync($"Tag {tagName} created");
        }

        [Command("nickname")]
        [Description("add a nickname to the user")]
        [RequireGuild, RequirePermissions(DSharpPlus.Permissions.ManageNicknames)]
        [Aliases("nick")]
        public async Task NickAdd(CommandContext ctx, DiscordMember nickUser, [RemainingText] string nick)
        {
            await nickUser.ModifyAsync(x => x.Nickname = nick);
            await ctx.RespondAsync($"Modified the nick of {nickUser.Username} to {nick}");
        }
    }
}
