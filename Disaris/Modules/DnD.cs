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
    public class DnD : BaseCommandModule
    {
        [Group("roll")]
        [Description("roll different sided dice")]
        public class Dice : BaseCommandModule 
        {
            private Random rng = new Random();

            [Command("20")]
            [Description("Rolls a 20 sided die")]
            [Aliases("twenty")]
            public async Task Roll20(CommandContext ctx)
            {
                int num = rng.Next(1, 21);
                await ctx.RespondAsync($"{ctx.Member.Username} rolled: {num}.");
            }

            [Command("12")]
            [Description("Rolls a 12 sided die")]
            [Aliases("twelve")]
            public async Task Roll12(CommandContext ctx)
            {
                int num = rng.Next(1, 13);
                await ctx.RespondAsync($"{ctx.Member.Username} rolled: {num}.");
            }

            [Command("10")]
            [Description("Rolls a 10 sided die")]
            [Aliases("ten")]
            public async Task Roll10(CommandContext ctx)
            {
                int num = rng.Next(1, 11);
                await ctx.RespondAsync($"{ctx.Member.Username} rolled: {num}.");
            }

            [Command("6")]
            [Description("Rolls a 6 sided die")]
            [Aliases("six")]
            public async Task Roll6(CommandContext ctx)
            {
                int num = rng.Next(1, 7);
                await ctx.RespondAsync($"{ctx.Member.Username} rolled: {num}.");
            }
        }
    }
}
