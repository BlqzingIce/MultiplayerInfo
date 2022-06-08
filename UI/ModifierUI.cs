using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Components;
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
			GameplaySetup.instance.AddTab("MultiplayerInfo", "MultiplayerInfo.UI.modifierui.bsml", this, BeatSaberMarkupLanguage.GameplaySetup.MenuType.Online);
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

		[UIValue("ResultsInfo")]
		public bool ResultsInfo
		{
			get => Configuration.PluginConfig.Instance.ResultsInfo;
			set => Configuration.PluginConfig.Instance.ResultsInfo = value;
		}

		[UIValue("ShowAccuracy")]
		public bool ShowAccuracy
		{
			get => Configuration.PluginConfig.Instance.ShowAccuracy;
			set => Configuration.PluginConfig.Instance.ShowAccuracy = value;
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
	}
}