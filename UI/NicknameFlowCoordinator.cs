﻿using BeatSaberMarkupLanguage;
using HMUI;
using Zenject;

namespace MultiplayerInfo.Settings
{
    public class NicknameFlowCoordinator : FlowCoordinator
    {
        public FlowCoordinator _parentFlow;
        private NicknameListViewController _nickListView;

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