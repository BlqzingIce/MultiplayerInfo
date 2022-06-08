using System.Collections.Generic;
using System.Runtime.CompilerServices;
using IPA.Config.Stores;
using IPA.Config.Stores.Attributes;
using IPA.Config.Stores.Converters;
using MultiplayerInfo.Models;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace MultiplayerInfo.Configuration
{
    internal class PluginConfig
    {
        public static PluginConfig Instance { get; set; }

        public virtual bool ResultsInfo { get; set; } = true;
        public virtual bool ShowAccuracy { get; set; } = true;
        public virtual bool EnableNicknames { get; set; } = true;

        [UseConverter(typeof(ListConverter<Nicknames>))]
        public virtual List<Nicknames> NicknamesList { get; set; } = new List<Nicknames>();
    }
}