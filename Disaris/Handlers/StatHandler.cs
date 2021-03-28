using Disaris.Common.Cosmetics;
using DSharpPlus.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disaris.Handlers
{
    public static class StatHandler
    {
        public static Root GetPlayerStats(string response)
        {
            Root playerStats = JsonConvert.DeserializeObject<Root>(response);
            return playerStats;
        }

        public static DiscordEmbed[] BuildStatEmbeds(Root stats)
        {
            var statEmbed = new DiscordEmbedBuilder()
                .WithTitle($"Stats for {stats.global.name}")
                .WithColor(DisarisCosmetics.DisarisColor)
                .WithThumbnail(stats.global.avatar)
                .AddField("Platform", stats.global.platform, true)
                .AddField("Level", stats.global.level.ToString(), true);

            if (stats.total.kills == null)
                statEmbed.AddField("Kills", "couldn't fetch that", true);
            else
                statEmbed.AddField("Kills", stats.total.kills.value == -1 ? "couldn't fetch that" : stats.total.kills.value.ToString(), true);

            if (stats.total.kd == null)
                statEmbed.AddField("K/D", "couldn't fetch that", true);
            else
                statEmbed.AddField("K/D", stats.total.kd.value == -1 ? "couldn't fetch that" : stats.total.kd.value.ToString(), true);

            statEmbed.Build();

            var rankEmbed = new DiscordEmbedBuilder()
                .WithTitle("Ranked info")
                .WithColor(DisarisCosmetics.DisarisColor)
                .WithThumbnail(stats.global.rank.rankImg)
                .AddField("Rank", stats.global.rank.rankName, true)
                .AddField("Division", stats.global.rank.rankDiv.ToString(), true)
                .AddField("Points", stats.global.rank.rankScore.ToString(), true)
                .Build();

            return new DiscordEmbed[] { statEmbed, rankEmbed };
        }
    }

    public class Rank
    {
        public int rankScore { get; set; }
        public string rankName { get; set; }
        public int rankDiv { get; set; }
        public int ladderPosPlatform { get; set; }
        public string rankImg { get; set; }
        public string rankedSeason { get; set; }
    }

    public class Global
    {
        public string name { get; set; }
        public long uid { get; set; }
        public string avatar { get; set; }
        public string platform { get; set; }
        public int level { get; set; }
        public Rank rank { get; set; }
    }

    public class Kills
    {
        public string name { get; set; }
        public int value { get; set; }
    }

    public class Kd
    {
        public int value { get; set; }
        public string name { get; set; }
    }

    public class Total
    {
        public Kills kills { get; set; }
        public Kd kd { get; set; }
    }

    public class Root
    {
        public Global global { get; set; }
        public Total total { get; set; }
    }
}
