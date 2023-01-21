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

            if (____scoreText.richText == false)
            {
                ____scoreText.enableWordWrapping = false;
                ____scoreText.richText = true;
                ____scoreText.transform.localPosition = ____scoreText.transform.localPosition + new UnityEngine.Vector3(6.5f, 0, 0);
            }

            string rank = ____rankText.text;
            ____rankText.text = "";

            string score = ____scoreText.text;
            ____scoreText.text = "";

            //don't do anything if everything is disabled
            if (Configuration.PluginConfig.Instance.ShowRank || Configuration.PluginConfig.Instance.ShowCombo || Configuration.PluginConfig.Instance.ShowMisses ||
                Configuration.PluginConfig.Instance.ShowBombs || Configuration.PluginConfig.Instance.ShowScore || Configuration.PluginConfig.Instance.ShowPercent ||
                Configuration.PluginConfig.Instance.ShowAccuracy)
            {
                //completion + rank
                if (levelCompletionResults.levelEndStateType == LevelCompletionResults.LevelEndStateType.Cleared)
                {
                    ____scoreText.text += levelCompletionResults.gameplayModifiers.noFailOn0Energy && levelCompletionResults.energy == 0 ? "NF    " : "";
                    ____scoreText.text += Configuration.PluginConfig.Instance.ShowRank ? rank + "    " : "";
                }
                else ____scoreText.text += "F    ";

                //combo
                if (Configuration.PluginConfig.Instance.ShowCombo)
                {
                    string combo = levelCompletionResults.maxCombo.ToString() + "<size=40%> </size><size=85%>🔄</size>";
                    ____scoreText.text += levelCompletionResults.fullCombo ? "<color=yellow>FC</color>  " + combo + "    " : combo + "    ";
                }

                //misses
                if (Configuration.PluginConfig.Instance.ShowMisses)
                {
                    string misses = (levelCompletionResults.missedCount + levelCompletionResults.badCutsCount).ToString();
                    ____scoreText.text += !levelCompletionResults.fullCombo ? "<color=red>X</color><size=65%> </size>" + misses + "    " : "";
                }

                //bombs
                if (Configuration.PluginConfig.Instance.ShowBombs)
                {
                    ____scoreText.text += !levelCompletionResults.fullCombo ? "<size=65%>💣 </size>" + levelCompletionResults.notGoodCount.ToString() + "    " : "";
                }

                //score
                if (Configuration.PluginConfig.Instance.ShowScore)
                {
                    ____scoreText.text += score + "    ";
                }

                //percent
                if (Configuration.PluginConfig.Instance.ShowPercent)
                {
                    double percent = (double)levelCompletionResults.multipliedScore / BeatmapCallbacksPatch.maxRawScore * 100;
                    if (percent < 0) percent *= -1;
                    ____scoreText.text += BeatmapCallbacksPatch.maxRawScore == -1 ? "??.??%" : percent.ToString("00.00") + "%";
                }

                //acc
                if (Configuration.PluginConfig.Instance.ShowAccuracy)
                {
                    ____scoreText.text += "<size=80%> (";
                    float averageFullScore = levelCompletionResults.averageCutScoreForNotesWithFullScoreScoringType;
                    if (!Configuration.PluginConfig.Instance.PercentAcc)
                        ____scoreText.text += averageFullScore.ToString("00.0");
                    else
                    {
                        ____scoreText.text += (averageFullScore/1.15).ToString("00.00") + "%";
                    }
                    if (Configuration.PluginConfig.Instance.DetailedAcc)
                    {
                        float averageAccScore = levelCompletionResults.averageCenterDistanceCutScoreForNotesWithFullScoreScoringType;
                        float averageSwingScore = averageFullScore - averageAccScore;
                        ____scoreText.text += "|" + averageSwingScore.ToString("00.0") + "+" + averageAccScore.ToString("0.0") + ")</size>";
                    }
                    else if (!Configuration.PluginConfig.Instance.PercentAcc)
                        ____scoreText.text += " <size=65%>Avg Cut</size>)</size>";
                    else
                        ____scoreText.text += ")</size>";
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