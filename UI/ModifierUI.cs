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
        private readonly ScoreFlowCoordinator _scoreFlow;
        private readonly NicknameFlowCoordinator _nickFlow;

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

		public ModifierUI(MainFlowCoordinator mainFlowCoordinator, ScoreFlowCoordinator scoreFlowCoordinator, NicknameFlowCoordinator nicknameFlowCoordinator)
		{
			_mainFlow = mainFlowCoordinator;
            _scoreFlow = scoreFlowCoordinator;
            _nickFlow = nicknameFlowCoordinator;
		}

        [UIAction("score_button_clicked")]
        private void Score_Button_Clicked()
        {
            FlowCoordinator currentFlow = _mainFlow.YoungestChildFlowCoordinatorOrSelf();
            _scoreFlow._parentFlow = currentFlow;
            currentFlow.PresentFlowCoordinator(_scoreFlow);
        }

        [UIAction("nick_button_clicked")]
        private void Nick_Button_Clicked()
        {
            FlowCoordinator currentFlow = _mainFlow.YoungestChildFlowCoordinatorOrSelf();
            _nickFlow._parentFlow = currentFlow;
            currentFlow.PresentFlowCoordinator(_nickFlow);
        }

        [UIValue("EnableNicknames")]
        public bool EnableNicknames
        {
            get => Configuration.PluginConfig.Instance.EnableNicknames;
            set => Configuration.PluginConfig.Instance.EnableNicknames = value;
        }
    }
}