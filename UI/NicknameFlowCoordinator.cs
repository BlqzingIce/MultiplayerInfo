using BeatSaberMarkupLanguage;
using HMUI;
using SiraUtil.Logging;
using Zenject;

namespace MultiplayerInfo.UI
{
    public class NicknameFlowCoordinator : FlowCoordinator
    {
        bool visible = false;

        public FlowCoordinator _parentFlow;
        private NicknameListViewController _nickListView;

        [Inject]
        public void Construct(NicknameListViewController nicknameListViewController)
        {
            _nickListView = nicknameListViewController;
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
            SetTitle("Nickname Editor");
            ProvideInitialViewControllers(_nickListView);
            visible = true;
        }

        protected override void BackButtonWasPressed(ViewController topViewController)
        {
            _parentFlow?.DismissFlowCoordinator(this);
            visible = false;
        }
    }
}