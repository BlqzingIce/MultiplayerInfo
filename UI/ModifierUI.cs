using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.GameplaySetup;
using HMUI;
using Zenject;
using System;

namespace MultiplayerInfo.Settings
{
    public class ModifierUI : IInitializable, IDisposable
	{
		private readonly MainFlowCoordinator _mainFlow;
		private readonly NicknameFlowCoordinator _prefFlow;

		public void Initialize()
		{
			GameplaySetup.instance.AddTab("MultiplayerInfo", "MultiplayerInfo.UI.modifierui.bsml", this, MenuType.Online);
		}

		public void Dispose()
		{
			if (GameplaySetup.instance != null)
			{
				GameplaySetup.instance.RemoveTab("MultiplayerInfo");
			}
		}

		public ModifierUI(MainFlowCoordinator mainFlowCoordinator, NicknameFlowCoordinator nicknameFlowCoordinator)
		{
			_mainFlow = mainFlowCoordinator;
			_prefFlow = nicknameFlowCoordinator;
		}

        [UIValue("EnableNicknames")]
        public bool EnableNicknames
        {
            get => Configuration.PluginConfig.Instance.EnableNicknames;
            set => Configuration.PluginConfig.Instance.EnableNicknames = value;
        }

        //shamelessly stolen from jdfixer
        [UIAction("nick_button_clicked")]
        private void Nick_Button_Clicked()
        {
            FlowCoordinator currentFlow = _mainFlow.YoungestChildFlowCoordinatorOrSelf();
            _prefFlow._parentFlow = currentFlow;
            currentFlow.PresentFlowCoordinator(_prefFlow);
        }

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
    }
}