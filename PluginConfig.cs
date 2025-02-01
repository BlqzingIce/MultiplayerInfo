using IPA.Config.Stores.Attributes;
using IPA.Config.Stores.Converters;
using MultiplayerInfo.Models;
using System.Collections.Generic;

namespace MultiplayerInfo
{
    public class PluginConfig
    {
        public virtual bool EnableScoreInfo { get; set; } = true;
        public virtual bool ShowOrder { get; set; } = true;
        public virtual bool ShowRank { get; set; } = true;
        public virtual bool ShowCombo { get; set; } = false;
        public virtual bool ShowMisses { get; set; } = true;
        public virtual bool ShowBombs { get; set; } = false;
        public virtual bool ShowScore { get; set; } = true;
        public virtual bool ShowPercent { get; set; } = true;
        public virtual bool ShowEstimatedPercent { get; set; } = false;
        public virtual bool ShowAccuracy { get; set; } = false;
        public virtual AccDisplay AccDisplay { get; set; } = AccDisplay.Number;

        public virtual bool EnableNicknames { get; set; } = true;
        [UseConverter(typeof(ListConverter<Nickname>))]
        public virtual List<Nickname> Nicknames { get; set; } = new List<Nickname>();

        public virtual bool EnableRankInfo { get; set; } = true;
    }
}
