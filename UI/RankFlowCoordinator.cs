using BeatSaberMarkupLanguage;
using HMUI;
using Zenject;

namespace MultiplayerInfo.UI
{
    public class RankFlowCoordinator : FlowCoordinator
    {
        bool visible = false;

        public FlowCoordinator _parentFlow = null!;
        private RankListViewController _rankListView = null!;

        [Inject]
        public void Construct(RankListViewController rankListViewController)
        {
            _rankListView = rankListViewController;
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
            SetTitle("Global Ranks");
            ProvideInitialViewControllers(_rankListView);
            visible = true;
        }

        protected override void BackButtonWasPressed(ViewController topViewController)
        {
            _parentFlow?.DismissFlowCoordinator(this);
            visible = false;
        }
    }
}