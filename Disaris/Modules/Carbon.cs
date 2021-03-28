using Disaris.Common.Cosmetics;
using Disaris.Common.Extensions;
using Disaris.Handlers;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Disaris.Modules
{
    public class Carbon : BaseCommandModule
    {
        [Command("code")]
        [Description("Allows you to turn a code block into a carbon code snippet")]
        [Aliases("cs", "snippet")]
        public async Task CarbonCode(CommandContext ctx, string theme, [RemainingText] string code)
        {
            if (theme != null && code == null)
            {
                await ctx.Channel.SendMessageAsync("Using this command requires you to specify a theme, use `ds carbon-themes` to get a list of all available themes");
                return;
            }

            if (code == null)
            {
                await ctx.Channel.SendMessageAsync("You need to give a code block");
                return;
            }

            var cs1 = code.IndexOf("```") + 3;
            cs1 = code.IndexOf('\n', cs1) + 1;
            var cs2 = code.LastIndexOf("```");

            if (cs1 == -1 || cs2 == -1)
            {
                await ctx.Channel.SendErrorAsync("Error!", "You need to wrap the code into a code block.");
                return;
            }

            var cs = code.Substring(cs1, cs2 - cs1);
            cs = cs.Remove(cs.LastIndexOf("\n"));
            var csUrl = WebUtility.UrlEncode(cs);

            string validTheme = CarbonHandler.ThemeMatcher(theme);

            var baseUrl = "https://carbon.now.sh/";
            var url = @$"{baseUrl}?l=auto&t={validTheme}&code={csUrl}";

            string title = $"Code Snippet by {ctx.Member.Username}";

            await ctx.Channel.SendCarbonCodeAsync(title, url);
        }

        [Command("carbon-themes")]
        [Description("Lists all the available themes for carbon snippets")]
        [Aliases("themes")]
        public async Task ThemesList(CommandContext ctx)
        {
            string[] lightThemes = CarbonHandler.GetLightThemes();
            string[] darkThemes = CarbonHandler.GetDarkThemes();

            var themesEmbed = new DiscordEmbedBuilder()
                .WithTitle("Available themes")
                .WithDescription("You can use these themes with the code command")
                .AddField("Dark themes", $"> {string.Join("\n> ", darkThemes)}")
                .AddField("Light Themes", $"> {string.Join("\n> ", lightThemes)}")
                .WithColor(DisarisCosmetics.DisarisColor)
                .Build();

            await ctx.Channel.SendMessageAsync(embed: themesEmbed);
        }

        [Command("theme")]
        [Description("individual theme images")]
        public async Task Theme(CommandContext ctx, string theme)
        {
            string[] themes = CarbonHandler.GetThemes();

            if (!Array.Exists(themes, x => x == theme))
            {
                await ctx.Channel.SendErrorAsync("The theme provided does not match any themes", "use `ds carbon-themes` to get a list of available themes");
                return;
            }

            await ctx.TriggerTypingAsync();

            string path = $"./Images/Snippets/{theme}-theme.png";

            var msg = await new DiscordMessageBuilder()
                .WithContent($"Theme snippet for {theme}")
                .WithFile(path, File.OpenRead(path))
                .SendAsync(ctx.Channel);
        }
    }
}
