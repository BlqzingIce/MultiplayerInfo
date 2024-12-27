using MultiplayerInfo.UI;
using SiraUtil.Affinity;
using Zenject;

namespace MultiplayerInfo.Patches
{
    internal class MultiplayerLevelScenesTransitionPatch : IAffinity
    {
        [Inject] private readonly NicknameFlowCoordinator _nicknameFlowCoordinator = null;
        [Inject] private readonly ScoreFlowCoordinator _scoreFlowCoordinator = null;
        [Inject] private readonly RankFlowCoordinator _rankFlowCoordinator = null;

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
