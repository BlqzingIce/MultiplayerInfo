using SiraUtil.Affinity;
using Zenject;
using MultiplayerInfo.UI;

namespace MultiplayerInfo.Patches
{
    internal class MultiplayerLevelScenesTransitionPatch : IAffinity
    {
        [Inject] NicknameFlowCoordinator _nicknameFlowCoordinator;
        [Inject] ScoreFlowCoordinator _scoreFlowCoordinator;

        [AffinityPrefix]
        [AffinityPatch(typeof(MultiplayerLevelScenesTransitionSetupDataSO), nameof(MultiplayerLevelScenesTransitionSetupDataSO.Init))]
        private void Prefix()
        {
            _nicknameFlowCoordinator.Dismiss();
            _scoreFlowCoordinator.Dismiss();
        }
    }
}
