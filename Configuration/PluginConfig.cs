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

        public virtual bool EnableNicknames { get; set; } = false;

        [UseConverter(typeof(ListConverter<Nicknames>))]
        public virtual List<Nicknames> NicknamesList { get; set; } = new List<Nicknames>();

        public virtual bool ShowRank { get; set; } = true;
        public virtual bool ShowCombo { get; set; } = false;
        public virtual bool ShowMisses { get; set; } = true;
        public virtual bool ShowBombs { get; set; } = false;
        public virtual bool ShowScore { get; set; } = true;
        public virtual bool ShowPercent { get; set; } = true;
        public virtual bool ShowAccuracy { get; set; } = false;
    }
}