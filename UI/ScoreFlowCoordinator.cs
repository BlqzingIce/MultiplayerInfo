using BeatSaberMarkupLanguage;
using HMUI;
using Zenject;

namespace MultiplayerInfo.Settings
{
    public class ScoreFlowCoordinator : FlowCoordinator
    {
        public FlowCoordinator _parentFlow;
        private ScoreSettingsViewController _scoreSettingsView;

        [Inject]
        public void Construct(ScoreSettingsViewController scoreSettingsViewController)
        {
            _scoreSettingsView = scoreSettingsViewController;
        }

        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            showBackButton = true;
            SetTitle("Score Settings");
            ProvideInitialViewControllers(_scoreSettingsView);
        }

        protected override void BackButtonWasPressed(ViewController topViewController)
        {
            _parentFlow?.DismissFlowCoordinator(this);
        }
    }
}