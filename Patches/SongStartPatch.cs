using SiraUtil.Affinity;

namespace MultiplayerInfo.Patches
{
    internal class SongStartPatch : IAffinity
    {
        public static int maxScore = -1;

        [AffinityPostfix]
        [AffinityPatch(typeof(MultiplayerController), "PerformSongStartSync")]
        private void Postfix()
        {
            if (BS_Utils.Plugin.LevelData.IsSet)
            {
                maxScore = ScoreModel.ComputeMaxMultipliedScoreForBeatmap(BS_Utils.Plugin.LevelData.GameplayCoreSceneSetupData.transformedBeatmapData);
            }
            else maxScore = -1;
        }
    }
}