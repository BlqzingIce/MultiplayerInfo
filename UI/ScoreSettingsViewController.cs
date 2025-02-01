using System;
using System.Collections.Generic;
using System.Linq;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using MultiplayerInfo.Models;

namespace MultiplayerInfo.UI
{
    [ViewDefinition("MultiplayerInfo.UI.scoresettings.bsml")]
    public class ScoreSettingsViewController : BSMLAutomaticViewController
    {
        [UIValue("ShowOrder")]
        public bool ShowOrder
        {
            get => Plugin.Config.ShowOrder;
            set => Plugin.Config.ShowOrder = value;
        }

        [UIValue("ShowRank")]
        public bool ShowRank
        {
            get => Plugin.Config.ShowRank;
            set => Plugin.Config.ShowRank = value;
        }

        [UIValue("ShowCombo")]
        public bool ShowCombo
        {
            get => Plugin.Config.ShowCombo;
            set => Plugin.Config.ShowCombo = value;
        }

        [UIValue("ShowMisses")]
        public bool ShowMisses
        {
            get => Plugin.Config.ShowMisses;
            set => Plugin.Config.ShowMisses = value;
        }

        [UIValue("ShowBombs")]
        public bool ShowBombs
        {
            get => Plugin.Config.ShowBombs;
            set => Plugin.Config.ShowBombs = value;
        }

        [UIValue("ShowScore")]
        public bool ShowScore
        {
            get => Plugin.Config.ShowScore;
            set => Plugin.Config.ShowScore = value;
        }

        [UIValue("ShowPercent")]
        public bool ShowPercent
        {
            get => Plugin.Config.ShowPercent;
            set => Plugin.Config.ShowPercent = value;
        }
        
        [UIValue("ShowEstimatedPercent")]
        public bool ShowEstimatedPercent
        {
            get => Plugin.Config.ShowEstimatedPercent;
            set => Plugin.Config.ShowEstimatedPercent = value;
        }

        [UIValue("ShowAccuracy")]
        public bool ShowAccuracy
        {
            get => Plugin.Config.ShowAccuracy;
            set => Plugin.Config.ShowAccuracy = value;
        }

        [UIValue("AccDisplayList")]
        public List<object> AccDisplayList = Enum.GetValues(typeof(AccDisplay)).Cast<object>().ToList();
        [UIValue("AccDisplayValue")]
        public AccDisplay AccDisplayValue
        {
            get => Plugin.Config.AccDisplay;
            set => Plugin.Config.AccDisplay = value;
        }
    }
}