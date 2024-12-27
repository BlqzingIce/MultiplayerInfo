using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.GameplaySetup;
using HMUI;
using System;
using Zenject;

namespace MultiplayerInfo.UI
{
    public class ModifierUI : IInitializable, IDisposable
	{
		private readonly MainFlowCoordinator _mainFlow;
        private readonly ScoreFlowCoordinator _scoreFlow;
        private readonly NicknameFlowCoordinator _nickFlow;
        private readonly RankFlowCoordinator _rankFlow;

        public void Initialize()
		{
			GameplaySetup.Instance.AddTab("MultiplayerInfo", "MultiplayerInfo.UI.modifierui.bsml", this, MenuType.Online);
		}

		public void Dispose()
		{
			if (GameplaySetup.Instance != null)
			{
				GameplaySetup.Instance.RemoveTab("MultiplayerInfo");
			}
		}

		public ModifierUI(MainFlowCoordinator mainFlowCoordinator, ScoreFlowCoordinator scoreFlowCoordinator, NicknameFlowCoordinator nicknameFlowCoordinator, RankFlowCoordinator rankFlowCoordinator)
		{
			_mainFlow = mainFlowCoordinator;
            _scoreFlow = scoreFlowCoordinator;
            _nickFlow = nicknameFlowCoordinator;
            _rankFlow = rankFlowCoordinator;
        }

        [UIValue("EnableScoreInfo")]
        public bool EnableScoreInfo
        {
            get => Plugin.Config.EnableScoreInfo;
            set => Plugin.Config.EnableScoreInfo = value;
        }

        [UIAction("score_button_clicked")]
        private void Score_Button_Clicked()
        {
            FlowCoordinator currentFlow = _mainFlow.YoungestChildFlowCoordinatorOrSelf();
            _scoreFlow._parentFlow = currentFlow;
            currentFlow.PresentFlowCoordinator(_scoreFlow);
        }

        [UIValue("EnableNicknames")]
        public bool EnableNicknames
        {
            get => Plugin.Config.EnableNicknames;
            set => Plugin.Config.EnableNicknames = value;
        }

        [UIAction("nick_button_clicked")]
        private void Nick_Button_Clicked()
        {
            FlowCoordinator currentFlow = _mainFlow.YoungestChildFlowCoordinatorOrSelf();
            _nickFlow._parentFlow = currentFlow;
            currentFlow.PresentFlowCoordinator(_nickFlow);
        }

        [UIValue("EnableRankInfo")]
        public bool EnableRankInfo
        {
            get => Plugin.Config.EnableRankInfo;
            set => Plugin.Config.EnableRankInfo = value;
        }

        [UIAction("rank_button_clicked")]
        private void Rank_Button_Clicked()
        {
            FlowCoordinator currentFlow = _mainFlow.YoungestChildFlowCoordinatorOrSelf();
            _rankFlow._parentFlow = currentFlow;
            currentFlow.PresentFlowCoordinator(_rankFlow);
        }

        [UIValue("sph_enabled")]
        public bool SPHEnabled
        {
            get => Plugin.SongPlayHistoryEnabled;
        }
    }
}