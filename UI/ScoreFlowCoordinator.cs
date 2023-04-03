using BeatSaberMarkupLanguage;
using HMUI;
using SiraUtil.Logging;
using Zenject;

namespace MultiplayerInfo.UI
{
    public class ScoreFlowCoordinator : FlowCoordinator
    {
        bool visible = false;

        public FlowCoordinator _parentFlow;
        private ScoreSettingsViewController _scoreSettingsView;

        [Inject]
        public void Construct(ScoreSettingsViewController scoreSettingsViewController)
        {
            _scoreSettingsView = scoreSettingsViewController;
        }

        public void Dismiss()
        {
            if (visible)
            {
                _parentFlow?.DismissFlowCoordinator(this);
                visible = false;
            }
        }

        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            showBackButton = true;
            SetTitle("Score Settings");
            ProvideInitialViewControllers(_scoreSettingsView);
            visible = true;
        }

        protected override void BackButtonWasPressed(ViewController topViewController)
        {
            _parentFlow?.DismissFlowCoordinator(this);
            visible = false;
        }
    }
}