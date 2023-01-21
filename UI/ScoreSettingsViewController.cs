using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;

namespace MultiplayerInfo.Settings
{
    [HotReload(RelativePathToLayout = @"scoresettings.bsml")]
    [ViewDefinition("MultiplayerInfo.UI.scoresettings.bsml")]
    public class ScoreSettingsViewController : BSMLAutomaticViewController
    {
        [UIValue("ShowRank")]
        public bool ShowRank
        {
            get => Configuration.PluginConfig.Instance.ShowRank;
            set => Configuration.PluginConfig.Instance.ShowRank = value;
        }

        [UIValue("ShowCombo")]
        public bool ShowCombo
        {
            get => Configuration.PluginConfig.Instance.ShowCombo;
            set => Configuration.PluginConfig.Instance.ShowCombo = value;
        }

        [UIValue("ShowMisses")]
        public bool ShowMisses
        {
            get => Configuration.PluginConfig.Instance.ShowMisses;
            set => Configuration.PluginConfig.Instance.ShowMisses = value;
        }

        [UIValue("ShowBombs")]
        public bool ShowBombs
        {
            get => Configuration.PluginConfig.Instance.ShowBombs;
            set => Configuration.PluginConfig.Instance.ShowBombs = value;
        }

        [UIValue("ShowScore")]
        public bool ShowScore
        {
            get => Configuration.PluginConfig.Instance.ShowScore;
            set => Configuration.PluginConfig.Instance.ShowScore = value;
        }

        [UIValue("ShowPercent")]
        public bool ShowPercent
        {
            get => Configuration.PluginConfig.Instance.ShowPercent;
            set => Configuration.PluginConfig.Instance.ShowPercent = value;
        }

        [UIValue("ShowAccuracy")]
        public bool ShowAccuracy
        {
            get => Configuration.PluginConfig.Instance.ShowAccuracy;
            set => Configuration.PluginConfig.Instance.ShowAccuracy = value;
        }

        [UIValue("PercentAcc")]
        public bool PercentAcc
        {
            get => Configuration.PluginConfig.Instance.PercentAcc;
            set => Configuration.PluginConfig.Instance.PercentAcc = value;
        }

        [UIValue("DetailedAcc")]
        public bool DetailedAcc
        {
            get => Configuration.PluginConfig.Instance.DetailedAcc;
            set => Configuration.PluginConfig.Instance.DetailedAcc = value;
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