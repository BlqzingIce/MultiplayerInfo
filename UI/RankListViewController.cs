﻿using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using HMUI;
using MultiplayerInfo.Models;
using MultiplayerInfo.Rank;
using Zenject;

namespace MultiplayerInfo.UI
{
    [HotReload(RelativePathToLayout = @"ranklist.bsml")]
    [ViewDefinition("MultiplayerInfo.UI.ranklist.bsml")]
    public class RankListViewController : BSMLAutomaticViewController
    {
        [Inject] private readonly PluginConfig _config = null;

        [UIComponent("rank_list")]
        public CustomListTableData rankList;

        [UIAction("select_rank")]
        private void Select_Rank(TableView tableView, int row)
        {
            rankList.TableView.ClearSelection();
        }

        private void Reload_List()
        {
            rankList.Data.Clear();

            if (!_config.EnableRankInfo)
                rankList.Data.Add(new CustomListTableData.CustomCellInfo("Rank collection is disabled!"));

            if (PlayerHandler.currentPlayerList.Count < 1 || MpPlayerPatch.cachedPlayerList.Count < 1)
            {
                rankList.Data.Add(new CustomListTableData.CustomCellInfo("No ranked players to display!"));
            }
            else
            {
                foreach (BasicPlayer player in PlayerHandler.currentPlayerList)
                {
                    foreach (PlatformPlayer miPlayer in MpPlayerPatch.cachedPlayerList)
                    {
                        if (player.Id == miPlayer.Id)
                        {
                            if (miPlayer.SSRank > 0 && miPlayer.BLRank > 0)
                            {
                                string entry = miPlayer.Name + " - SS #" + miPlayer.SSRank + " | BL #" + miPlayer.BLRank;
                                rankList.Data.Add(new CustomListTableData.CustomCellInfo(entry));
                            }
                            else if (miPlayer.SSRank > 0 && miPlayer.BLRank <= 0)
                            {
                                string entry = miPlayer.Name + " - SS #" + miPlayer.SSRank;
                                rankList.Data.Add(new CustomListTableData.CustomCellInfo(entry));
                            }
                            else if (miPlayer.SSRank <= 0 && miPlayer.BLRank > 0)
                            {
                                string entry = miPlayer.Name + " - BL #" + miPlayer.BLRank;
                                rankList.Data.Add(new CustomListTableData.CustomCellInfo(entry));
                            }
                            else
                            {
                                string entry = miPlayer.Name + " - N/A";
                                rankList.Data.Add(new CustomListTableData.CustomCellInfo(entry));
                            }
                            break;
                        }
                    }
                }
            }

            if (rankList.Data.Count == 0)
            {
                rankList.Data.Add(new CustomListTableData.CustomCellInfo("No ranked players to display!"));
            }

            rankList.TableView.ReloadData();
        }

        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            base.DidActivate(firstActivation, addedToHierarchy, screenSystemEnabling);

            Reload_List();
        }

        protected override void DidDeactivate(bool removedFromHierarchy, bool screenSystemDisabling)
        {
            base.DidDeactivate(removedFromHierarchy, screenSystemDisabling);
        }
    }
}