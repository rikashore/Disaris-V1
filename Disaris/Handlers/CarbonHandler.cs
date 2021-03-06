using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disaris.Handlers
{
    public static class CarbonHandler
    {
        public static Dictionary<string, string> themeDict = new Dictionary<string, string>
        {
            {"draculapro", "dark" },
            {"vscode", "dark" },
            {"3024night", "dark" },
            {"a11ydark", "dark" },
            {"blackboard", "dark" },
            {"base16(dark)", "dark" },
            {"base16(light)", "light" },
            {"cobalt", "dark" },
            {"duotone", "dark" },
            {"hopscotch", "dark" },
            {"lucario", "dark" },
            {"material", "dark" },
            {"nonokai", "dark" },
            {"nightowl", "dark" },
            {"nord", "dark" },
            {"oceanicnext", "dark" },
            {"onelight", "light" },
            {"onedark", "dark" },
            {"panda", "dark" },
            {"paraiso", "dark" },
            {"seti", "dark" },
            {"shadesofpurple", "dark" },
            {"solarizeddark", "dark" },
            {"solarizedlight", "light" },
            {"synthwave84", "dark" },
            {"twilight", "dark" },
            {"verminal", "dark" },
            {"yeti", "light" },
            {"zenburn", "dark" }
        };

        /*
        public static string[] themes = {
                "draculapro",
                "vscode",
                "3024night",
                "a11ydark",
                "blackboard",
                "base16(dark)",
                "base16(light)",
                "cobalt",
                "duotone",
                "hopscotch",
                "lucario",
                "material",
                "monokai",
                "nightowl",
                "nord",
                "oceanicnext",
                "onelight",
                "onedark",
                "panda",
                "paraiso",
                "seti",
                "shadesofpurple",
                "solarizeddark",
                "solarizedlight",
                "synthwave84",
                "twilight",
                "verminal",
                "yeti",
                "zenburn"
        };

        public static string[] lightThemes =
        {
            "base16(light)",
            "onelight",
            "solarizedlight",
            "yeti"
        };

        public static string[] darkThemes = {
                "draculapro",
                "vscode",
                "3024night",
                "a11ydark",
                "blackboard",
                "base16(dark)",
                "cobalt",
                "duotone",
                "hopscotch",
                "lucario",
                "material",
                "monokai",
                "nightowl",
                "nord",
                "oceanicnext",
                "onedark",
                "panda",
                "paraiso",
                "seti",
                "shadesofpurple",
                "solarizeddark",
                "synthwave84",
                "twilight",
                "verminal",
                "zenburn"
        };
        */

        public static string[] GetDarkThemes()
        {
            var darkThemes = themeDict.Where(x => x.Value == "dark").Select(x => x.Key).ToArray();
            return darkThemes;
        }

        public static string[] GetLightThemes()
        {
            var lightThemes = themeDict.Where(x => x.Value == "light").Select(x => x.Key).ToArray();
            return lightThemes;
        }

        public static string[] GetThemes()
        {
            Dictionary<string, string>.KeyCollection themes = themeDict.Keys;
            return themes.ToArray();
        }

        public static string ThemeMatcher(string theme)
        {
            string themeInput = theme.ToLower();
            string validTheme = themeInput switch
            {
                "draculapro" => "dracula-pro",
                "vscode" => "vscode",
                "3024night" => "3024-night",
                "a11ydark" => "a11y-dark",
                "blackboard" => "blackboard",
                "base16(dark)" => "base16-dark",
                "base16(light)" => "base16-light",
                "cobalt" => "cobalt",
                "duotone" => "duotone-dark",
                "hopscotch" => "hopscotch",
                "lucario" => "lucario",
                "material" => "material",
                "monokai" => "monokai",
                "nightowl" => "night-owl",
                "nord" => "nord",
                "oceanicnext" => "oceanic-next",
                "onelight" => "one-light",
                "onedark" => "one-dark",
                "panda" => "panda",
                "paraiso" => "paraiso-dark",
                "seti" => "seti",
                "shadesofpurple" => "shades-of-purple",
                "solarizeddark" => "solarized+dark",
                "solarizedlight" => "solarized+light",
                "synthwave84" => "synthwave-84",
                "twilight" => "twilight",
                "verminal" => "verminal",
                "yeti" => "yeti",
                "zenburn" => "zenburn",
                _ => "vscode"
            };

            return validTheme;
        }
    }
}
