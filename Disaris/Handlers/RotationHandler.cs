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
    public static class RotationHandler
    {
        public static Rotation GetRotation(string response)
        {
            Rotation rotationInfo = JsonConvert.DeserializeObject<Rotation>(response);
            return rotationInfo;
        }
        
        public static DiscordEmbed BuildRotationEmbed(Rotation rotation)
        {
            var rotationEmbed = new DiscordEmbedBuilder()
                .WithTitle("Map rotation for Apex Legends")
                .WithColor(DisarisCosmetics.DisarisColor)
                .WithTimestamp(DateTime.Now)
                .AddField("Current Map", rotation.current.map)
                .AddField("Started at", rotation.current.readableDate_start)
                .AddField("Will end at", rotation.current.readableDate_end)
                .AddField("Next Map", rotation.next.map)
                .AddField("Starts at", rotation.next.readableDate_start)
                .AddField("Duration", $"{rotation.next.DurationInMinutes} mins")
                .Build();

            return rotationEmbed;
        } 
    }

    public class Current
    {
        public int start { get; set; }
        public int end { get; set; }
        public string readableDate_start { get; set; }
        public string readableDate_end { get; set; }
        public string map { get; set; }
        public int DurationInSecs { get; set; }
        public int DurationInMinutes { get; set; }
        public int remainingSecs { get; set; }
        public int remainingMins { get; set; }
        public string remainingTimer { get; set; }
    }

    public class Next
    {
        public int start { get; set; }
        public int end { get; set; }
        public string readableDate_start { get; set; }
        public string readableDate_end { get; set; }
        public string map { get; set; }
        public int DurationInSecs { get; set; }
        public int DurationInMinutes { get; set; }
    }

    public class Rotation
    {
        public Current current { get; set; }
        public Next next { get; set; }
    }
}
