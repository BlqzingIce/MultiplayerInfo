using BeatSaberMarkupLanguage;
using HMUI;
using Zenject;

//shamelessly stolen from jdfixer
namespace MultiplayerInfo.Settings
{
    public class NicknameFlowCoordinator : FlowCoordinator
    {
        public FlowCoordinator _parentFlow;
        private NicknameListViewController _nickListView;

        /* Since this is binded as a unity component, our "Constructor" is actually a method called Construct (with an inject attribute)
         * We would do the same for ViewControllers if we wanna ask for stuff from Zenject
         */
        [Inject]
        public void Construct(NicknameListViewController nicknameListViewController)
        {
            _nickListView = nicknameListViewController;
        }

        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            showBackButton = true;
            SetTitle("Nickname Editor");
            ProvideInitialViewControllers(_nickListView);
        }

        protected override void BackButtonWasPressed(ViewController topViewController)
        {
            _parentFlow?.DismissFlowCoordinator(this);
        }
    }
}