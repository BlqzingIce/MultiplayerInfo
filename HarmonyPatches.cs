using HarmonyLib;
using System.Collections.Generic;

namespace MultiplayerInfo.HarmonyPatches
{
    [HarmonyPatch(typeof(BeatmapCallbacksController), "Dispose")]
    internal class BeatmapCallbacksPatch
    {
        public static int maxRawScore = 1;
        public static void Prefix(IReadonlyBeatmapData ____beatmapData)
        {
            maxRawScore = ScoreModel.ComputeMaxMultipliedScoreForBeatmap(____beatmapData);
        }
    }

    [HarmonyPatch(typeof(ResultsTableCell), "SetData")]
    internal class ResultsCellPatch
    {
        public static void Postfix(int order, IConnectedPlayer connectedPlayer, LevelCompletionResults levelCompletionResults, TMPro.TextMeshProUGUI ____rankText, TMPro.TextMeshProUGUI ____scoreText, TMPro.TextMeshProUGUI ____nameText)
        {            
            if (____rankText.richText == false)
            {
                ____rankText.enableWordWrapping = false;
                ____rankText.richText = true;
                ____scoreText.enableWordWrapping = false;
                ____scoreText.richText = true;
                ____rankText.transform.localPosition = (____rankText.transform.localPosition + new UnityEngine.Vector3(-2, 0, 0));
                ____scoreText.transform.localPosition = (____scoreText.transform.localPosition + new UnityEngine.Vector3(-5, 0, 0));
            }

            Plugin.Log.Info("player: " + connectedPlayer.userName + "(id: " + connectedPlayer.userId + ")");
            Plugin.Log.Info("placement: " + order.ToString());
            Plugin.Log.Info("modifiedScore: " + levelCompletionResults.modifiedScore.ToString());
            Plugin.Log.Info("rawScore: " + levelCompletionResults.multipliedScore.ToString());
            Plugin.Log.Info("fullCombo: " + levelCompletionResults.fullCombo.ToString());
            Plugin.Log.Info("good cuts: " + levelCompletionResults.goodCutsCount.ToString());
            Plugin.Log.Info("bad cuts: " + levelCompletionResults.badCutsCount.ToString());
            Plugin.Log.Info("misses: " + levelCompletionResults.missedCount.ToString());
            Plugin.Log.Info("bombs hit: " + levelCompletionResults.notGoodCount.ToString());
            Plugin.Log.Info("acc: " + levelCompletionResults.averageCenterDistanceCutScoreForNotesWithFullScoreScoringType.ToString());
            Plugin.Log.Info("max combo: " + levelCompletionResults.maxCombo.ToString());
            Plugin.Log.Info("endSongTime: " + levelCompletionResults.endSongTime.ToString());

            bool passedLevel = levelCompletionResults.levelEndStateType == LevelCompletionResults.LevelEndStateType.Cleared ? true : false;
            bool isNoFail = levelCompletionResults.gameplayModifiers.noFailOn0Energy && levelCompletionResults.energy == 0;
            int totalMisses = levelCompletionResults.missedCount + levelCompletionResults.badCutsCount;
            string percentageText = ((double)levelCompletionResults.multipliedScore / BeatmapCallbacksPatch.maxRawScore * 100).ToString("0.00");
            string score = ____scoreText.text;
            string preText = !passedLevel ? "F    " : isNoFail ? "NF    " : "";
            string missText = levelCompletionResults.fullCombo ? "FC" : preText + "<color=red>X</color><size=65%> </size>" + totalMisses.ToString();
            ____rankText.SetText(percentageText + "<size=75%>%</size>");
            ____scoreText.SetText(missText + "    " + score);

            if (connectedPlayer.userId.Equals("QXzRo+cwkPArBeq+0i4yxw"))
            {
                ____nameText.SetText("Munchkin61");
            }
            if (connectedPlayer.userId.Equals("EgSFNQHl2WZK631rEyLIJx"))
            {
                ____nameText.SetText("professional modder");
            }
        }
    }

    [HarmonyPatch(typeof(MultiplayerScoreRingManager), "SpawnTexts")]
    internal class ScoreRingPatch
    {
        public static void Postfix(List<IConnectedPlayer> ____allActivePlayers, Dictionary<string, MultiplayerScoreRingItem> ____scoreRingItems)
        {
            foreach (IConnectedPlayer allActivePlayer in ____allActivePlayers)
            {
                if (allActivePlayer.userId.Equals("QXzRo+cwkPArBeq+0i4yxw"))
                {
                    ____scoreRingItems[allActivePlayer.userId].SetName("Munchkin61");
                }
            }
        }
    }

    [HarmonyPatch(typeof(ConnectedPlayerName), "Start")]
    internal class ConnectedPlayerNamePatch
    {
        public static void Postfix(IConnectedPlayer ____connectedPlayer, TMPro.TextMeshProUGUI ____nameText)
        {
            if (____connectedPlayer.userId.Equals("QXzRo+cwkPArBeq+0i4yxw"))
            {
                ____nameText.text = "Munchkin61";
            }
        }
    }

    [HarmonyPatch(typeof(GameServerPlayerTableCell), "SetData")]
    internal class ServerPlayerTableCellPatch
    {
        public static void Postfix(IConnectedPlayer connectedPlayer, HMUI.CurvedTextMeshPro ____playerNameText)
        {
            if (connectedPlayer.userId.Equals("QXzRo+cwkPArBeq+0i4yxw"))
            {
                ____playerNameText.text = "Munchkin61";
            }
        }
    }
}