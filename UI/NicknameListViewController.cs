using HMUI;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using BeatSaberMarkupLanguage.Parser;
using System.Collections.Generic;
using MultiplayerInfo.Models;

//shamelessly stolen from jdfixer
namespace MultiplayerInfo.Settings
{
    [HotReload(RelativePathToLayout = @"nicknamelist.bsml")]
    [ViewDefinition("MultiplayerInfo.UI.nicknamelist.bsml")]
    public class NicknameListViewController : BSMLAutomaticViewController
    {
        [UIParams]
        private BSMLParserParams parserParams;

        [UIComponent("nick_list")]
        public CustomListTableData nickList;
        private Nicknames _selectedNick = null;

        [UIAction("select_nick")]
        private void Select_Nick(TableView tableView, int row)
        {
            _selectedNick = Configuration.PluginConfig.Instance.NicknamesList[row];
        }



        private List<Player> Players = new List<Player>();

        [UIComponent("player_list")]
        public CustomListTableData playerList;
        private Player _selectedPlayer = null;

        [UIAction("select_player")]
        private void Select_Player(TableView tableView, int row)
        {
            _selectedPlayer = Players[row];
        }

        [UIAction("add_pressed")]
        private void Add_Pressed()
        {
            Reload_Unnicked_Player_List();
            if (Players == null)
            {
                return;
            }
            parserParams.EmitEvent("open_modal");
        }

        [UIAction("add_player")]
        private void Add_Player()
        {
            if (_selectedPlayer == null)
            {
                return;
            }
            Configuration.PluginConfig.Instance.NicknamesList.Add(new Nicknames(_selectedPlayer.PlayerID, _selectedPlayer.PlayerName, _selectedPlayer.PlayerName));
            Reload_List_From_Config();
            parserParams.EmitEvent("closeModals");
        }



        [UIAction("edit_pressed")]
        private void edit_Pressed()
        {
            if (_selectedNick == null)
            {
                return;
            }
            nickname = _selectedNick.NickName;
            parserParams.EmitEvent("open_keyboard");
        }

        [UIValue("nick")]
        string nickname = null;

        [UIAction("keyboard_enter")]
        private void Keyboard_Enter(string text)
        {
            foreach (Nicknames nickname in Configuration.PluginConfig.Instance.NicknamesList)
            {
                if (nickname.PlayerID == _selectedNick.PlayerID)
                {
                    nickname.NickName = text;
                    break;
                }
            }
            Reload_List_From_Config();
        }



        [UIAction("remove_pressed")]
        private void Remove_Pressed()
        {
            if (_selectedNick == null)
            {
                return;
            }
            Configuration.PluginConfig.Instance.NicknamesList.Remove(_selectedNick);
            Reload_List_From_Config();
            Reload_Unnicked_Player_List();
        }



        private void Reload_List_From_Config()
        {
            nickList.data.Clear();

            if (Configuration.PluginConfig.Instance.NicknamesList == null)
            {
                return;
            }

            foreach (var nick in Configuration.PluginConfig.Instance.NicknamesList)
            {
                nickList.data.Add(new CustomListTableData.CustomCellInfo($"{nick.NickName} ({nick.PlayerName})"));
            }

            nickList.tableView.ReloadData();
            nickList.tableView.ClearSelection();
            _selectedNick = null;
        }

        private void Reload_Unnicked_Player_List()
        {
            Players.Clear();
            foreach (Player player in HarmonyPatches.ServerPlayerTableViewPatch.playerList)
            {
                Players.Add(player);
            }

            for (int i = 0; i < Players.Count; i++)
            {
                foreach (Nicknames nickname in Configuration.PluginConfig.Instance.NicknamesList)
                {
                    if (nickname.PlayerName == Players[i].PlayerName)
                    {
                        Players.RemoveAt(i);
                        i--;
                        break;
                    }
                }
            }

            playerList.data.Clear();
            if (Players != null && Players.Count != 0)
            {
                foreach (var player in Players)
                {
                    playerList.data.Add(new CustomListTableData.CustomCellInfo($"{player.PlayerName}"));
                }
            }

            playerList.tableView.ReloadData();
            playerList.tableView.ClearSelection();
            _selectedPlayer = null;
        }

        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            base.DidActivate(firstActivation, addedToHierarchy, screenSystemEnabling);

            Reload_List_From_Config();
            Reload_Unnicked_Player_List();
        }

        protected override void DidDeactivate(bool removedFromHierarchy, bool screenSystemDisabling)
        {
            base.DidDeactivate(removedFromHierarchy, screenSystemDisabling);
        }
    }
}