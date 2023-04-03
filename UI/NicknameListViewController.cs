using HMUI;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using BeatSaberMarkupLanguage.Parser;
using System.Collections.Generic;
using MultiplayerInfo.Models;

namespace MultiplayerInfo.UI
{
    [HotReload(RelativePathToLayout = @"nicknamelist.bsml")]
    [ViewDefinition("MultiplayerInfo.UI.nicknamelist.bsml")]
    public class NicknameListViewController : BSMLAutomaticViewController
    {
        [UIParams]
        private readonly BSMLParserParams parserParams = null!;

        [UIComponent("nick_list")]
        public CustomListTableData nickList;
        private Nickname _selectedNick = null;

        [UIAction("select_nick")]
        private void Select_Nick(TableView tableView, int row)
        {
            _selectedNick = Plugin.Config.Nicknames[row];
        }



        private List<BasicPlayer> Players = new List<BasicPlayer>();

        [UIComponent("player_list")]
        public CustomListTableData playerList;
        private BasicPlayer _selectedPlayer = null;

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
            Plugin.Config.Nicknames.Add(new Nickname(_selectedPlayer.Id, _selectedPlayer.Name, _selectedPlayer.Name));
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
            nickname = _selectedNick.Nick;
            parserParams.EmitEvent("open_keyboard");
        }

        [UIValue("nick")]
        string nickname = null;

        [UIAction("keyboard_enter")]
        private void Keyboard_Enter(string text)
        {
            foreach (Nickname nickname in Plugin.Config.Nicknames)
            {
                if (nickname.PlayerId == _selectedNick.PlayerId)
                {
                    nickname.Nick = text;
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
            Plugin.Config.Nicknames.Remove(_selectedNick);
            Reload_List_From_Config();
            Reload_Unnicked_Player_List();
        }



        private void Reload_List_From_Config()
        {
            nickList.data.Clear();

            if (Plugin.Config.Nicknames == null)
            {
                return;
            }

            foreach (var nick in Plugin.Config.Nicknames)
            {
                nickList.data.Add(new CustomListTableData.CustomCellInfo($"{nick.Nick} ({nick.Name})"));
            }

            nickList.tableView.ReloadData();
            nickList.tableView.ClearSelection();
            _selectedNick = null;
        }

        private void Reload_Unnicked_Player_List()
        {
            Players.Clear();
            foreach (BasicPlayer player in Patches.ServerPlayerTableViewPatch.playerList)
            {
                Players.Add(player);
            }

            for (int i = 0; i < Players.Count; i++)
            {
                foreach (Nickname nickname in Plugin.Config.Nicknames)
                {
                    if (nickname.Name == Players[i].Name)
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
                    playerList.data.Add(new CustomListTableData.CustomCellInfo($"{player.Name}"));
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