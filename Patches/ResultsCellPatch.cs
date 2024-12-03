using SiraUtil.Affinity;
using System;
using Zenject;

namespace MultiplayerInfo.Patches
{
    internal class ResultsCellPatch : IAffinity
    {
        [Inject] private readonly PluginConfig _config = null!;

        [AffinityPostfix]
        [AffinityPatch(typeof(ResultsTableCell), nameof(ResultsTableCell.SetData))]
        private void Postfix(IConnectedPlayer connectedPlayer, LevelCompletionResults levelCompletionResults, TMPro.TextMeshProUGUI ____orderText, TMPro.TextMeshProUGUI ____rankText, TMPro.TextMeshProUGUI ____scoreText, TMPro.TextMeshProUGUI ____nameText)
        {
            if (_config.EnableNicknames)
            {
                for (int i = _config.Nicknames.Count - 1; i >= 0; i--)
                {
                    if (_config.Nicknames[i].PlayerId.Equals(connectedPlayer.userId))
                    {
                        ____nameText.text = _config.Nicknames[i].Nick;
                        break;
                    }
                }
            }

            if (!_config.EnableScoreInfo)
            {
                if (____scoreText.fontStyle == TMPro.FontStyles.Normal)
                {
                    ____scoreText.enableWordWrapping = true;
                    ____scoreText.richText = false;
                    ____scoreText.fontStyle = TMPro.FontStyles.Italic;
                    ____scoreText.transform.localPosition = ____scoreText.transform.localPosition - new UnityEngine.Vector3(6.5f, 0, 0);
                }
                if (____orderText.fontStyle == TMPro.FontStyles.Normal)
                {
                    ____orderText.fontStyle = TMPro.FontStyles.Italic;
                }
                if (____nameText.fontStyle == TMPro.FontStyles.Normal)
                {
                    ____nameText.fontStyle = TMPro.FontStyles.Italic;
                }
                return;
            }

            if (____scoreText.fontStyle != TMPro.FontStyles.Normal)
            {
                ____scoreText.enableWordWrapping = false;
                ____scoreText.richText = true;
                ____scoreText.fontStyle = TMPro.FontStyles.Normal;
                ____scoreText.transform.localPosition = ____scoreText.transform.localPosition + new UnityEngine.Vector3(6.5f, 0, 0);
            }
            if (____orderText.fontStyle != TMPro.FontStyles.Normal)
            {
                ____orderText.fontStyle = TMPro.FontStyles.Normal;
            }
            if (____nameText.fontStyle != TMPro.FontStyles.Normal)
            {
                ____nameText.fontStyle = TMPro.FontStyles.Normal;
            }

            string rank = ____rankText.text;
            ____rankText.text = "";

            string score = ____scoreText.text;
            ____scoreText.text = "";

            if (!_config.ShowOrder)
                ____orderText.text = "";

            //don't do anything if everything is disabled
            if (_config.ShowRank || _config.ShowCombo || _config.ShowMisses ||
                _config.ShowBombs || _config.ShowScore || _config.ShowPercent ||
                _config.ShowAccuracy)
            {
                //completion + rank
                if (levelCompletionResults.levelEndStateType == LevelCompletionResults.LevelEndStateType.Cleared)
                {
                    ____scoreText.text += levelCompletionResults.gameplayModifiers.noFailOn0Energy && levelCompletionResults.energy == 0 ? "NF    " : "";
                    ____scoreText.text += _config.ShowRank ? rank + "    " : "";
                }
                else ____scoreText.text += "F    ";

                //combo
                if (_config.ShowCombo)
                {
                    string combo = levelCompletionResults.maxCombo.ToString() + "<size=40%> </size><size=85%>🔄</size>";
                    ____scoreText.text += levelCompletionResults.fullCombo ? "<color=yellow>FC</color>  " + combo + "    " : combo + "    ";
                }

                //misses
                if (_config.ShowMisses)
                {
                    string misses = (levelCompletionResults.missedCount + levelCompletionResults.badCutsCount).ToString();
                    ____scoreText.text += !levelCompletionResults.fullCombo ? "<color=red>X</color><size=65%> </size>" + misses + "    " : "";
                }

                //bombs
                if (_config.ShowBombs)
                {
                    ____scoreText.text += !levelCompletionResults.fullCombo ? "<size=65%>💣 </size>" + levelCompletionResults.notGoodCount.ToString() + "    " : "";
                }

                //score
                if (_config.ShowScore)
                {
                    ____scoreText.text += score + "    ";
                }

                //percent
                if (_config.ShowPercent)
                {
                    if (SongStartPatch.maxScore != -1)
                    {
                        double percent = (double)levelCompletionResults.multipliedScore / SongStartPatch.maxScore * 100;
                        percent = Math.Abs(percent);
                        ____scoreText.text += percent.ToString("00.00") + "%";
                    }
                    else ____scoreText.text += "??.??%";
                }

                //acc
                if (_config.ShowAccuracy)
                {
                    ____scoreText.text += "<size=80%> (";
                    float averageFullScore = levelCompletionResults.averageCutScoreForNotesWithFullScoreScoringType;
                    if (!_config.PercentAcc)
                        ____scoreText.text += averageFullScore.ToString("00.0");
                    else
                    {
                        ____scoreText.text += (averageFullScore / 1.15).ToString("00.00") + "%";
                    }
                    if (_config.DetailedAcc)
                    {
                        float averageAccScore = levelCompletionResults.averageCenterDistanceCutScoreForNotesWithFullScoreScoringType;
                        float averageSwingScore = averageFullScore - averageAccScore;
                        ____scoreText.text += "|" + averageSwingScore.ToString("00.0") + "+" + averageAccScore.ToString("0.0") + ")</size>";
                    }
                    else if (!_config.PercentAcc)
                        ____scoreText.text += " <size=65%>Avg Cut</size>)</size>";
                    else
                        ____scoreText.text += ")</size>";
                }
            }
        }
    }
}