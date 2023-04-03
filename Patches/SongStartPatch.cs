using SiraUtil.Affinity;
using SiraUtil.Logging;
using Zenject;

namespace MultiplayerInfo.Patches
{
    internal class SongStartPatch : IAffinity
    {
        [Inject] private readonly SiraLog _log = null!;

        public static int maxScore = -1;

        [AffinityPostfix]
        [AffinityPatch(typeof(MultiplayerController), nameof(MultiplayerController.PerformSongStartSync))]
        private void Postfix()
        {
            if (BS_Utils.Plugin.LevelData.IsSet)
            {
                maxScore = ScoreModel.ComputeMaxMultipliedScoreForBeatmap(BS_Utils.Plugin.LevelData.GameplayCoreSceneSetupData.transformedBeatmapData);
            }
            else maxScore = -1;
            _log.Info("maxScore set to " + maxScore);
        }
    }
}