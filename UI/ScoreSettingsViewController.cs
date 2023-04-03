using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;

namespace MultiplayerInfo.UI
{
    [HotReload(RelativePathToLayout = @"scoresettings.bsml")]
    [ViewDefinition("MultiplayerInfo.UI.scoresettings.bsml")]
    public class ScoreSettingsViewController : BSMLAutomaticViewController
    {
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

        [UIValue("ShowAccuracy")]
        public bool ShowAccuracy
        {
            get => Plugin.Config.ShowAccuracy;
            set => Plugin.Config.ShowAccuracy = value;
        }

        [UIValue("PercentAcc")]
        public bool PercentAcc
        {
            get => Plugin.Config.PercentAcc;
            set => Plugin.Config.PercentAcc = value;
        }

        [UIValue("DetailedAcc")]
        public bool DetailedAcc
        {
            get => Plugin.Config.DetailedAcc;
            set => Plugin.Config.DetailedAcc = value;
        }

        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            base.DidActivate(firstActivation, addedToHierarchy, screenSystemEnabling);
        }

        protected override void DidDeactivate(bool removedFromHierarchy, bool screenSystemDisabling)
        {
            base.DidDeactivate(removedFromHierarchy, screenSystemDisabling);
        }
    }
}