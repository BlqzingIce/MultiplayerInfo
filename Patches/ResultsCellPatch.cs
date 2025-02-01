using MultiplayerInfo.Models;
using SiraUtil.Affinity;
using System;
using Zenject;

namespace MultiplayerInfo.Patches
{
    internal class ResultsCellPatch : IAffinity
    {
        [Inject] private readonly PluginConfig _config = null;

        [AffinityPostfix]
        [AffinityPatch(typeof(ResultsTableCell), nameof(ResultsTableCell.SetData))]
        private void Postfix(IConnectedPlayer connectedPlayer, LevelCompletionResults levelCompletionResults, TMPro.TextMeshProUGUI ____orderText, TMPro.TextMeshProUGUI ____rankText, TMPro.TextMeshProUGUI ____scoreText, TMPro.TextMeshProUGUI ____nameText)
        {
            if (_config.EnableNicknames)
            {
                for (int i = _config.Nicknames.Count - 1; i >= 0; i--)
                {
                    if (!_config.Nicknames[i].PlayerId.Equals(connectedPlayer.userId)) continue;
                    ____nameText.text = _config.Nicknames[i].Nick;
                    break;
                }
            }

            if (!_config.EnableScoreInfo)
            {
                if (____scoreText.fontStyle == TMPro.FontStyles.Normal)
                {
                    ____scoreText.enableWordWrapping = true;
                    ____scoreText.richText = false;
                    ____scoreText.fontStyle = TMPro.FontStyles.Italic;
                    ____scoreText.transform.localPosition -= new UnityEngine.Vector3(6.5f, 0, 0);
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
                ____scoreText.transform.localPosition += new UnityEngine.Vector3(6.5f, 0, 0);
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
            
            if (levelCompletionResults.levelEndStateType == LevelCompletionResults.LevelEndStateType.Cleared)
            {
                if (levelCompletionResults.gameplayModifiers.noFailOn0Energy && levelCompletionResults.energy == 0)
                    ____scoreText.text += "NF";
                if (_config.ShowRank)
                    ____scoreText.text += "  " + rank;
            }
            else ____scoreText.text += "F";
            
            if (_config.ShowCombo)
            {
                if (levelCompletionResults.fullCombo)
                    ____scoreText.text += "  <color=yellow>FC</color>";
                ____scoreText.text += "  " + levelCompletionResults.maxCombo + "<size=80%>🔄</size>";
            }
            
            if (_config.ShowMisses)
            {
                if (!levelCompletionResults.fullCombo)
                    ____scoreText.text += "  " + (levelCompletionResults.missedCount + levelCompletionResults.badCutsCount) + "<size=30%> </size><color=red><size=80%>X</size></color>";
            }
            
            if (_config.ShowBombs)
            {
                if (!levelCompletionResults.fullCombo)
                    ____scoreText.text += "  " + levelCompletionResults.notGoodCount + "<size=30%> </size><size=65%>💣</size>";
            }
            
            if (_config.ShowScore)
            {
                ____scoreText.text += "  " + score;
            }
            
            if (_config.ShowPercent)
            {
                if (SongStartPatch.maxScore != -1)
                {
                    float percent = (float)levelCompletionResults.multipliedScore / SongStartPatch.maxScore * 100;
                    percent = Math.Abs(percent);
                    ____scoreText.text += "  " + percent.ToString("00.00") + "%";
                }
                else ____scoreText.text += "  ERROR%";
            }
            
            if (_config.ShowEstimatedPercent)
            {
                int noteCount = levelCompletionResults.goodCutsCount + levelCompletionResults.badCutsCount + levelCompletionResults.missedCount;
                float percent = (float)levelCompletionResults.multipliedScore / ComputeMaxMultipliedScore(noteCount) * 100;
                ____scoreText.text += "  ~" + percent.ToString("00.00") + "%";
            }
            
            if (_config.ShowAccuracy)
            {
                float acc = levelCompletionResults.averageCutScoreForNotesWithFullScoreScoringType;
                float cutAcc = levelCompletionResults.averageCenterDistanceCutScoreForNotesWithFullScoreScoringType;
                float swingAcc = acc - cutAcc;
                float percentAcc = acc / 1.15f;

                ____scoreText.text += "<size=80%> (";
                
                switch (_config.AccDisplay)
                {
                    case AccDisplay.Number:
                        ____scoreText.text += acc.ToString("00.0");
                        break;
                    case AccDisplay.Split:
                        ____scoreText.text += swingAcc.ToString("00.0") + "+" + cutAcc.ToString("0.0");
                        break;
                    case AccDisplay.Percent:
                        ____scoreText.text += percentAcc.ToString("00.00") + "%";
                        break;
                    case AccDisplay.PercentAndNumber:
                        ____scoreText.text += percentAcc.ToString("00.00") + "%";
                        ____scoreText.text += "|" + acc.ToString("00.0");
                        break;
                    case AccDisplay.PercentAndSplit:
                        ____scoreText.text += percentAcc.ToString("00.00") + "%";
                        ____scoreText.text += "|" + swingAcc.ToString("00.0") + "+" + cutAcc.ToString("0.0");
                        break;
                    case AccDisplay.SaberSurgeon:
                        ____scoreText.text += (cutAcc / 0.15f).ToString("00.00") + "%";
                        break;
                }
                
                ____scoreText.text += ")</size>";
            }
        }
        
        private static int ComputeMaxMultipliedScore(int noteCount)
        {
            if (noteCount == 0) return 0;
            if (noteCount == 1) return 115;
            if (noteCount <= 5) return 115 * 1 + 230 * (noteCount - 1);
            if (noteCount <= 13) return 115 * 1 + 230 * 4 + 460 * (noteCount - 5);
            return 115 * 1 + 230 * 4 + 460 * 8 + 920 * (noteCount - 13);
            // 1 = 1
            // 2, 3, 4, 5 = 2
            // 6, 7, 8, 9, 10, 11, 12, 13 = 4
            // 14+ = 8
        }
    }
}