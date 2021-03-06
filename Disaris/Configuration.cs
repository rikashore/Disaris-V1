using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace Disaris
{
    public class Configuration
    {
        private string _prefix = null!;
        private string _token = null!;

        private readonly string _configurationPath = Path.Combine(Environment.CurrentDirectory, "config.json");

        public string Prefix
        {
            get => _prefix;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new NullReferenceException($"Prefix must be defined in {_configurationPath}");

                _prefix = value;
            }
        }

        public string Token
        {
            get => _token;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new NullReferenceException($"Token must be defined in {_configurationPath}");

                _token = value;
            }
        }

        public string? ApiTrackerKey { get; set; }

        public Configuration()
        {
            LoadConfiguration();

            if (ApiTrackerKey == null || string.IsNullOrEmpty(ApiTrackerKey))
                Log.Warning("No ApiTrackerKey has been set, commands using this will be unavailable");
        }

        private void LoadConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile(_configurationPath)
                .Build();

            Prefix = config.GetValue<string>(nameof(Prefix));
            Token = config.GetValue<string>(nameof(Token));
            ApiTrackerKey = config.GetValue<string>(nameof(ApiTrackerKey));
        }
    }
}
