using IPA.Config.Stores.Attributes;
using IPA.Config.Stores.Converters;
using System.Collections.Generic;
using MultiplayerInfo.Models;

namespace MultiplayerInfo
{
    public class PluginConfig
    {
        public virtual bool EnableNicknames { get; set; } = false;

        [UseConverter(typeof(ListConverter<Nickname>))]
        public virtual List<Nickname> Nicknames { get; set; } = new List<Nickname>();

        public virtual bool ShowRank { get; set; } = true;
        public virtual bool ShowCombo { get; set; } = false;
        public virtual bool ShowMisses { get; set; } = true;
        public virtual bool ShowBombs { get; set; } = false;
        public virtual bool ShowScore { get; set; } = true;
        public virtual bool ShowPercent { get; set; } = true;
        public virtual bool ShowAccuracy { get; set; } = false;
        public virtual bool PercentAcc { get; set; } = false;
        public virtual bool DetailedAcc { get; set; } = false;
    }
}
