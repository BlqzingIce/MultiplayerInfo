using MultiplayerInfo.UI;
using SiraUtil.Affinity;
using Zenject;

namespace MultiplayerInfo.Patches
{
    internal class MultiplayerLevelScenesTransitionPatch : IAffinity
    {
        [Inject] NicknameFlowCoordinator _nicknameFlowCoordinator = null;
        [Inject] ScoreFlowCoordinator _scoreFlowCoordinator = null;
        [Inject] RankFlowCoordinator _rankFlowCoordinator = null;

        [AffinityPrefix]
        [AffinityPatch(typeof(MultiplayerLevelScenesTransitionSetupDataSO), nameof(MultiplayerLevelScenesTransitionSetupDataSO.Init))]
        private void Prefix()
        {
            _nicknameFlowCoordinator.Dismiss();
            _scoreFlowCoordinator.Dismiss();
            _rankFlowCoordinator.Dismiss();
        }
    }
}
