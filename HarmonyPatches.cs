using HarmonyLib;
using System.Collections.Generic;
using MultiplayerInfo.Models;
using System;

namespace MultiplayerInfo.HarmonyPatches
{
    [HarmonyPatch(typeof(BeatmapCallbacksController), MethodType.Constructor)]
    [HarmonyPatch(new Type[] { typeof(BeatmapCallbacksController.InitData) })]
    internal class BeatmapCallbacksPatch
    {
        public static int maxRawScore = -1;
        public static void Postfix(BeatmapCallbacksController.InitData initData)
        {
            maxRawScore = ScoreModel.ComputeMaxMultipliedScoreForBeatmap(initData.beatmapData);
        }
    }

    [HarmonyPatch(typeof(ResultsTableCell), "SetData")]
    internal class ResultsCellPatch
    {
        public static void Postfix(IConnectedPlayer connectedPlayer, LevelCompletionResults levelCompletionResults, TMPro.TextMeshProUGUI ____rankText, TMPro.TextMeshProUGUI ____scoreText, TMPro.TextMeshProUGUI ____nameText)
        {
            if (Configuration.PluginConfig.Instance.ResultsInfo)
            {
                if (____scoreText.richText == false)
                {
                    ____scoreText.enableWordWrapping = false;
                    ____scoreText.richText = true;
                    ____scoreText.transform.localPosition = ____scoreText.transform.localPosition + new UnityEngine.Vector3(7, 0, 0);
                }

                /*
                Plugin.Log.Info("player: " + connectedPlayer.userName + " (id: " + connectedPlayer.userId + ")");
                Plugin.Log.Info("modifiedScore: " + levelCompletionResults.modifiedScore.ToString());
                Plugin.Log.Info("rawScore: " + levelCompletionResults.multipliedScore.ToString());
                Plugin.Log.Info("good cuts: " + levelCompletionResults.goodCutsCount.ToString());
                Plugin.Log.Info("bombs hit: " + levelCompletionResults.notGoodCount.ToString());
                Plugin.Log.Info("acc: " + levelCompletionResults.averageCutScoreForNotesWithFullScoreScoringType.ToString());
                Plugin.Log.Info("max combo: " + levelCompletionResults.maxCombo.ToString());
                Plugin.Log.Info("endSongTime: " + levelCompletionResults.endSongTime.ToString());
                */

                bool passedLevel = levelCompletionResults.levelEndStateType == LevelCompletionResults.LevelEndStateType.Cleared ? true : false;
                bool isNoFail = levelCompletionResults.gameplayModifiers.noFailOn0Energy && levelCompletionResults.energy == 0;
                int totalMisses = levelCompletionResults.missedCount + levelCompletionResults.badCutsCount;
                string preText = !passedLevel ? "F    " : isNoFail ? "NF    " : "";
                string missText = levelCompletionResults.fullCombo ? "<color=yellow>FC</color>" : preText + "<color=red>X</color><size=65%> </size>" + totalMisses.ToString();

                string score = ____scoreText.text;

                string percentageText;
                if (BeatmapCallbacksPatch.maxRawScore == -1)
                    percentageText = "ERROR";
                else
                    percentageText = ((double)levelCompletionResults.multipliedScore / BeatmapCallbacksPatch.maxRawScore * 100).ToString("00.00") + "%";

                string accText = Configuration.PluginConfig.Instance.ShowAccuracy ? "<size=80%> (" + levelCompletionResults.averageCutScoreForNotesWithFullScoreScoringType.ToString("00.0") + " Avg Cut)</size>" : "            ";

                ____rankText.SetText("");
                ____scoreText.SetText(missText + "    " + score + "    " + percentageText + accText);
            }

            if (Configuration.PluginConfig.Instance.EnableNicknames)
            {
                for (int i = Configuration.PluginConfig.Instance.NicknamesList.Count - 1; i >= 0; i--)
                {
                    if (Configuration.PluginConfig.Instance.NicknamesList[i].PlayerID.Equals(connectedPlayer.userId))
                    {
                        ____nameText.text = Configuration.PluginConfig.Instance.NicknamesList[i].NickName;
                        break;
                    }
                }
            }
        }
    }

    [HarmonyPatch(typeof(MultiplayerScoreRingManager), "SpawnTexts")]
    internal class ScoreRingPatch
    {
        public static void Postfix(List<IConnectedPlayer> ____allActivePlayers, Dictionary<string, MultiplayerScoreRingItem> ____scoreRingItems)
        {
            if (Configuration.PluginConfig.Instance.EnableNicknames)
            {
                foreach (IConnectedPlayer allActivePlayer in ____allActivePlayers)
                {
                    for (int i = Configuration.PluginConfig.Instance.NicknamesList.Count - 1; i >= 0; i--)
                    {
                        if (Configuration.PluginConfig.Instance.NicknamesList[i].PlayerID.Equals(allActivePlayer.userId))
                        {
                            ____scoreRingItems[allActivePlayer.userId].SetName(Configuration.PluginConfig.Instance.NicknamesList[i].NickName);
                            break;
                        }
                    }
                }
            }
        }
    }

    [HarmonyPatch(typeof(ConnectedPlayerName), "Start")]
    internal class ConnectedPlayerNamePatch
    {
        public static void Postfix(IConnectedPlayer ____connectedPlayer, TMPro.TextMeshProUGUI ____nameText)
        {
            if (Configuration.PluginConfig.Instance.EnableNicknames)
            {
                for (int i = Configuration.PluginConfig.Instance.NicknamesList.Count - 1; i >= 0; i--)
                {
                    if (Configuration.PluginConfig.Instance.NicknamesList[i].PlayerID.Equals(____connectedPlayer.userId))
                    {
                        ____nameText.text = Configuration.PluginConfig.Instance.NicknamesList[i].NickName;
                        break;
                    }
                }
            }
        }
    }

    [HarmonyPatch(typeof(GameServerPlayerTableCell), "SetData")]
    internal class ServerPlayerTableCellPatch
    {
        public static void Postfix(IConnectedPlayer connectedPlayer, HMUI.CurvedTextMeshPro ____playerNameText)
        {
            for (int i = Configuration.PluginConfig.Instance.NicknamesList.Count - 1; i >= 0; i--)
            {
                if (Configuration.PluginConfig.Instance.NicknamesList[i].PlayerID.Equals(connectedPlayer.userId))
                {
                    Configuration.PluginConfig.Instance.NicknamesList[i].PlayerName = connectedPlayer.userName;

                    if (Configuration.PluginConfig.Instance.EnableNicknames)
                    {
                        ____playerNameText.text = Configuration.PluginConfig.Instance.NicknamesList[i].NickName;
                        break;
                    }
                }
            }
        }
    }

    [HarmonyPatch(typeof(GameServerPlayersTableView), "SetData")]
    internal class ServerPlayerTableViewPatch
    {
        public static List<Player> playerList = new List<Player>();
        public static void Postfix(List<IConnectedPlayer> sortedPlayers)
        {
            playerList.Clear();
            foreach (IConnectedPlayer connectedPlayer in sortedPlayers)
            {
                playerList.Add(new Player(connectedPlayer.userId, connectedPlayer.userName));
            }
        }
    }
}